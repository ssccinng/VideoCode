// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
Test.TestParams(1, 2, 3);
object lockObjo = new object();
lock (lockObjo)
{
}

Lock lockObj = new Lock();
lock (lockObj)
{
}
return;
string song = """
              Here I am, staring at my own palms，
              I feel the warmth of your hand，
              I thought I’d be forever by your side, protecting you，
              Assuming that’s what you wanted to，
              I had no clue, that we had nothing between us，
              Perhaps I was so scared to know the truth，
              Now that you’re gone, and yet your love is still embracing me，
              I’m at a loss, I need you with me，
              It’s like I’m drowning, as if I’ve lost the way，
              But you always appear as if you guide my way，
              I thought I could do it all, I had my confidence，
              But now I’m left alone with no one by my end，
              I knew my faults, but I pretended I didn’t know，
              I fear I’m not the kind of man you know，
              I see you smile, pretending everything is alright，
              But that made me feel more in misery，
              I don’t know where to start, there is nothing left for me，
              I know I’ve lost it all except the memories，
              When I have my chin up high, I see a glimpse of light，
              Like how we find the brightest star up in the sky，
              I now have the courage to show you there is a way，
              You told me silently I know what you will say，
              And when I search for you, I see a guiding light，
              It’s not so clear to me that you’re still far away，
              A step away。
              """;

var ag = song
    .Split(' ', StringSplitOptions.RemoveEmptyEntries).CountBy(s => s)
    .OrderByDescending(s => s.Value);
// .AggregateBy( s=> s, new List<string>(), (total, item) => [.. total, item] );




foreach (var s in ag)
{
    Console.WriteLine(s);
}

(string, int)[] peoms =
[
    ("临流", 3),
    ("揽镜", 1),
    ("双魂", 2),
    ("落红", 3),
];
var data = peoms.AggregateBy(s => s.Item2, seed: new List<string>(),
    (seed, item) => [..seed, item.Item1]);

foreach (var s in data)
{
    Console.WriteLine($"{s.Key}: {string.Join(", ", s.Value)}");
}


EndIndex a = new ()
{
    Value =
    {
        [^1] = 0,
        [^2] = 1,
        [^3] = 2,
        [^4] = 3,
        [^5] = 4,
        [^6] = 5,
        [^7] = 6,
        [^8] = 7,
        [^9] = 8,
        [^10] = 9
    }
};

return;


BenchmarkRunner.Run<TestParams>();
return;

// int[] a = new int[] { 1, 2, 3, 1, 2, 3 , 1, 2, 3 };//, 46, 465, 484, 98 };
// var c = a.Aggregate((a, b) =>  a * b);
// var d = a.AggregateBy(s => s, seed:new List<int>(), (i, i1) => [..i, i1]);
// Console.WriteLine(c);
//
// foreach (var VARIABLE in d)
// {
//     Console.WriteLine(string.Join(", ", VARIABLE.Value));
// }

return;
BenchmarkRunner.Run<TestLoop>();
class Test
{
    [OverloadResolutionPriority(priority: 1)]
    public static void TestParams(params int[] args)
    {
        Console.WriteLine("params int[]");
    }
    // [OverloadResolutionPriority(priority: 1)]

    public static void TestParams(params ReadOnlySpan<int> args)
    {
        Console.WriteLine("params ReadOnlySpan<int>");
    }

}

[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(false)]
public class TestLinq
{
    private IEnumerable<int> _data =
        Enumerable.Range(0, 1000)
            // .Select(s => Random.Shared.Next())
            .ToList();

    [Benchmark]
    public bool Any() => _data.Any(s => s == 400);

    [Benchmark]
    public bool All() => _data.All(s => s >= 0);

    [Benchmark]
    public int Count() => _data.Count();

    [Benchmark]
    public int First() => _data.First();


    // private IEnumerable<int> _data = [];
    // Enumerable.Range(0, 10000).Select(s => Random.Shared.Next());

    // [Benchmark] public int Sum() => _data.Sum();
    // [Benchmark] public int Min() => _data.Min();
    // [Benchmark] public int Max() => _data.Max();
    //
    // [Benchmark]
    // public int First() => _data.First();
    //
    // [Benchmark] public int Last() => _data.Last();
    //
    // [Benchmark] public int Count() => _data.Count();
}

// [SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class TestLoop
{
    public int[] Data { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        Data = Enumerable.Range(0, 10000).ToArray();
    }

    [Benchmark]
    public int Sum()
    {
        int sum = 0;
        for (int i = 0; i < Data.Length; i++)
        {
            sum += Data[i];
        }

        return sum;
    }
}

[MemoryDiagnoser]
public class TestParams
{
    public int Sum(params int[] seq)
    {
        int sum = 0;
        for (int i = 0; i < seq.Length; i++) sum += seq[i];
        return sum;
    }

    public int SumSpan(params ReadOnlySpan<int> seq)
    {
        int sum = 0;
        for (int i = 0; i < seq.Length; i++) sum += seq[i];
        return sum;
    }

    [Benchmark]
    public int TestSum()
    {
        return Sum(1, 3, 4, 4, 5, 6, 7, 8, 9, 10, 11);
    }

    [Benchmark]
    public int TestSpan()
    {
        return SumSpan(1, 3, 4, 4, 5, 6, 7, 8, 9, 10, 11);
    }
}

public class EndIndex
{
    public int[] Value { get; set; }
} 