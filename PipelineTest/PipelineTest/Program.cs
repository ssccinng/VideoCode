// See https://aka.ms/new-console-template for more information
using System.Buffers;
using System.IO.Pipelines;
byte[] buffer = ArrayPool<byte>.Shared.Rent(1000);
ArrayPool<byte>.Shared.Return(buffer);
byte[] buffer1 = ArrayPool<byte>.Shared.Rent(1000);

buffer1[1] = 2;

Console.WriteLine(buffer[1]);

Console.WriteLine(buffer.Length);