IEnumerable<int> ints = [ 1, 2, 3, 4, 5 ];

// .NET8
foreach (var (i, v) in ints.Select((v, i) => (i, v)))
{
    Console.WriteLine($"Index: {i}, Value: {v}");
}

// .NET9
foreach (var (i, v) in ints.Index())
{
    Console.WriteLine($"Index: {i}, Value: {v}");
}

// Output
// Index: 0, Value: 1
// Index: 1, Value: 2
// Index: 2, Value: 3
// Index: 3, Value: 4
// Index: 4, Value: 5