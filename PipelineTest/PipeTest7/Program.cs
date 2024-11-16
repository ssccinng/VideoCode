using System;
using System.Collections.Concurrent;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;
public class SingleThreadPipeSchedulerW : PipeScheduler
{
    private readonly Thread _thread;
    private readonly BlockingCollection<Action> _actions = new BlockingCollection<Action>();

    public SingleThreadPipeSchedulerW()
    {
        _thread = new Thread(ThreadProc) { IsBackground = true };
        _thread.Start();
    }

    private void ThreadProc()
    {
        foreach (var action in _actions.GetConsumingEnumerable())
        {
            action();
        }
    }

    public override void Schedule(Action<object?> action, object? state)
    {
        _actions.Add(() =>
        {
            Console.WriteLine($"Scheduled on ThreadW: {Thread.CurrentThread.ManagedThreadId}");
            action(state);
        });
    }
}

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
        var writerScheduler = new SingleThreadPipeScheduler();
        var readerScheduler = new SingleThreadPipeScheduler();

        var pipe = new Pipe(new PipeOptions(readerScheduler: readerScheduler, writerScheduler: writerScheduler));
        int[] a = [1, 2, 3];

        a.Index();
        // 启动写入任务
        var writeTask = Task.Run(async () =>
        {
            var writer = pipe.Writer;

            // 模拟写入数据
            await writer.WriteAsync(new byte[] { 1, 2, 3 });
            Console.WriteLine($"WriteAsync scheduled on Thread: {Thread.CurrentThread.ManagedThreadId}");

            // 刷新数据以确保它被写入
            await writer.FlushAsync();
            Console.WriteLine($"FlushAsync scheduled on Thread: {Thread.CurrentThread.ManagedThreadId}");

            // 完成写入
            await writer.CompleteAsync();
        });

        // 启动读取任务
        var readTask = Task.Run(async () =>
        {
            var reader = pipe.Reader;

            // 读取数据
            var result = await reader.ReadAsync();
            var buffer = result.Buffer;

            foreach (var segment in buffer)
            {
                Console.WriteLine($"Read byte: {segment.Span[0]}");
            }

            reader.AdvanceTo(buffer.End);
            await reader.CompleteAsync();
        });

        await Task.WhenAll(writeTask, readTask);
    }
}
