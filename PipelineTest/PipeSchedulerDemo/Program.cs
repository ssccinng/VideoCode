// See https://aka.ms/new-console-template for more information
using System.Collections.Concurrent;
using System.IO.Pipelines;
using System.Text;

var scheduler = new ThreadPoolScheduler();

Pipe pipe = new(new PipeOptions(readerScheduler: PipeScheduler.ThreadPool, writerScheduler: scheduler, pauseWriterThreshold: 2000, resumeWriterThreshold: 1000));

var reader = pipe.Reader;
var writer = pipe.Writer;

_ = Task.Run(async () =>
{
	while (true)
	{
		var res = await reader.ReadAsync();
		var buffer = res.Buffer;

        foreach (var item in buffer)
		{
            Console.WriteLine(Encoding.UTF8.GetString(item.Span));
		}

        reader.AdvanceTo(buffer.End);

        if (res.IsCompleted)
		{
            break;
        }
        await Task.Delay(10000);
    }
    reader.Complete();

});

_ = Task.Run(async () =>
{
    while (true)
    {
        var span = writer.GetSpan(510);

        var aa = Encoding.UTF8.GetBytes("Hello, World!\n");

        aa.CopyTo(span);

        writer.Advance(510);

        await writer.FlushAsync();
        await Task.Delay(500);
        Console.WriteLine("tick");
    }
});


Console.ReadLine();

internal sealed class ThreadPoolScheduler : PipeScheduler
{
    public override void Schedule(Action<object?> action, object? state)
    {
        Console.WriteLine("Schedule");

        System.Threading.ThreadPool.QueueUserWorkItem(action, state, preferLocal: false);
    }


}