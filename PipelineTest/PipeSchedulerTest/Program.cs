// See https://aka.ms/new-console-template for more information
using System.IO.Pipelines;

Console.WriteLine("Hello, World!");
Pipe pipe = new Pipe(new PipeOptions());

PipeReader pipeReader = PipeReader.Create(null, new StreamPipeReaderOptions ())