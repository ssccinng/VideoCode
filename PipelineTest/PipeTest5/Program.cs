// See https://aka.ms/new-console-template for more information
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.IO.Pipelines;
using System.Reflection.PortableExecutable;

//var scheduler = new SingleThreadPipeScheduler();
var scheduler = new ThreadPoolScheduler() {  };
var scheduler1 = new ThreadPoolScheduler1();
var pipe = new Pipe(new PipeOptions(readerScheduler: scheduler, writerScheduler: scheduler1, resumeWriterThreshold: 1000, pauseWriterThreshold: 1500));

ReadResult result = await pipe.Reader.ReadAsync();
while (true)
{
    var op = Console.ReadLine();

    if (op == "1")
    {
        //pipe.Reader.AdvanceTo();
    }
    else
    {

        pipe.Writer.Advance(1000);
        await pipe.Writer.FlushAsync();
    }
}




var taskb = Task.Run(async () =>
{
    for (int i = 0; i < 1000; i++)
    {
        ReadResult result = await pipe.Reader.ReadAsync();
        ReadOnlySequence<byte> buffer = result.Buffer;

        foreach (var segment in buffer)
        {
            Console.WriteLine($"Read byte: {string.Join(", ", segment.ToArray())}");

            //Console.WriteLine();
        }

        // 标记数据已经被消费
        pipe.Reader.AdvanceTo(buffer.End);

        if (result.IsCompleted)
        {
            break;
        }

        Task.Delay(10000).Wait();

    }
});


//var taska = Task.Run(async () =>
//{
for (int i = 0; i < 100; i++)
    {
        var memory = pipe.Writer.GetSpan(5100);

        var bytes = new byte[] { (byte)i, (byte)(i + 1), (byte)(i + 2), (byte)(i + 3) };
        bytes.CopyTo(memory);

        //Console.WriteLine($"Writing: {string.Join(", ", bytes)}");

        pipe.Writer.Advance(5100);
        Console.WriteLine("ThreadId be:" + Thread.CurrentThread.ManagedThreadId);
        // 写入数据并刷新
        await pipe.Writer.FlushAsync();
        Console.WriteLine("ThreadId af:" + Thread.CurrentThread.ManagedThreadId);

        // 模拟写入的间隔
         Task.Delay(100).Wait();
    }
//});
//var taskc = Task.Run(async () =>
//{
//    for (int i = 0; i < 100; i++)
//    {
//        var memory = pipe.Writer.GetSpan(5100000);

//        var bytes = new byte[] { (byte)i, (byte)(i + 1), (byte)(i + 2), (byte)(i + 3) };
//        bytes.CopyTo(memory);

//        //Console.WriteLine($"Writing: {string.Join(", ", bytes)}");

//        pipe.Writer.Advance(5100000);

//        // 写入数据并刷新
//        await pipe.Writer.FlushAsync();

//        // 模拟写入的间隔
//        await Task.Delay(10);
//    }
//});


Console.ReadLine();



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

internal sealed class ThreadPoolScheduler : PipeScheduler
{
    public override void Schedule(Action<object?> action, object? state)
    {
        Console.WriteLine("test ");
        System.Threading.ThreadPool.QueueUserWorkItem(action, state, preferLocal: false);
    }

}

internal sealed class ThreadPoolScheduler1 : PipeScheduler
{
    public override void Schedule(Action<object?> action, object? state)
    {
        Console.WriteLine("test 1");
        System.Threading.ThreadPool.QueueUserWorkItem(action, state, preferLocal: false);
    }

}
internal sealed class InlineScheduler : PipeScheduler
{
    public override void Schedule(Action<object?> action, object? state)
    {
        Console.WriteLine("test ");

        action(state);
    }


}
