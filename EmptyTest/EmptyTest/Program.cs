using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TestEmpty>();

[MemoryDiagnoser]
[SimpleJob]
public class TestEmpty
{
    [Benchmark]
    public byte[] GetBytes() => [];
    [Benchmark]
    public byte[] GetBytes1() => Array.Empty<byte>();
    [Benchmark]
    public byte[] GetBytes2() => new byte[0];

}