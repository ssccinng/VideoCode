using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

public class SingleThreadPipeScheduler : PipeScheduler
{
    private readonly Thread _thread;
    private readonly BlockingCollection<Action> _actions = new BlockingCollection<Action>();

    public SingleThreadPipeScheduler()
    {
        _thread = new Thread(ThreadProc) { IsBackground = true };
        _thread.Start();
    }

    private void ThreadProc()
    {
        foreach (var action in _actions.GetConsumingEnumerable())
        {
            Console.WriteLine($"Action executing on Thread: {_thread.ManagedThreadId}");
            action();
        }
    }

    public override void Schedule(Action<object?> action, object? state)
    {
        _actions.Add(() =>
        {
            Console.WriteLine($"Scheduled on Thread: {Thread.CurrentThread.ManagedThreadId}");
            action(state);
        });
    }
}

public class Program
{
    public static async Task Main()
    {
        var scheduler = new SingleThreadPipeScheduler();
        var pipe = new Pipe(new PipeOptions(readerScheduler: scheduler, writerScheduler: scheduler));

        // 启动写入任务
        var writing = FillPipeAsync(pipe.Writer);

        // 启动读取任务
        var reading = ReadPipeAsync(pipe.Reader);

        await Task.WhenAll(reading, writing);
    }

    private static async Task FillPipeAsync(PipeWriter writer)
    {
        for (int i = 0; i < 5; i++)
        {
            var memory = writer.GetMemory(4);
            var bytes = new byte[] { (byte)i, (byte)(i + 1), (byte)(i + 2), (byte)(i + 3) };
            bytes.CopyTo(memory);

            Console.WriteLine($"Writing: {string.Join(", ", bytes)}");

            writer.Advance(bytes.Length);

            // 写入数据并刷新
            await writer.FlushAsync();

            // 模拟写入的间隔
            await Task.Delay(500);
        }

        // 完成写入
        await writer.CompleteAsync();
    }

    private static async Task ReadPipeAsync(PipeReader reader)
    {
        while (true)
        {
            ReadResult result = await reader.ReadAsync();
            ReadOnlySequence<byte> buffer = result.Buffer;

            foreach (var segment in buffer)
            {
                Console.WriteLine($"Read byte: {string.Join(", ", segment.ToArray())}");
            }

            // 标记数据已经被消费
            reader.AdvanceTo(buffer.End);

            if (result.IsCompleted)
            {
                break;
            }
        }

        await reader.CompleteAsync();
    }
}