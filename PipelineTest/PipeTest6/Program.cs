// See https://aka.ms/new-console-template for more information
using System.IO.Pipelines;
using System.Text;

Console.WriteLine("Hello, World!");
//ThreadPoolScheduler scheduler = new();
InlineScheduler scheduler = new();
InlineScheduler1 scheduler1 = new();
Pipe pipe = new Pipe(new PipeOptions(readerScheduler: scheduler, writerScheduler: scheduler1));


_ = Task.Run(async () =>
{




    while (true)
    {
        var res = await pipe.Reader.ReadAsync();
        var buffer = res.Buffer;

        foreach (var item in buffer)
        {
            Console.WriteLine("Recv:" +  Encoding.UTF8.GetString(item.Span));
        }

        pipe.Reader.AdvanceTo(buffer.End);
        await Task.Delay(100);
    }
});

while (true)
{
    var m = pipe.Writer.GetSpan(512);
    var aa = "asdasd"u8.ToArray();
    aa.CopyTo(m);
    pipe.Writer.Advance(aa.Length);
    Console.WriteLine($"Send: Writing: {string.Join(", ", aa)}");

    await pipe.Writer.FlushAsync();

    await Task.Delay(500);

}






internal sealed class ThreadPoolScheduler : PipeScheduler
{
    public override void Schedule(Action<object?> action, object? state)
    {
        Console.WriteLine("test ");
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
internal sealed class InlineScheduler1 : PipeScheduler
{
    public override void Schedule(Action<object?> action, object? state)
    {
        Console.WriteLine("test1 ");

        action(state);
    }


}