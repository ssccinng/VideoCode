using Microsoft.Extensions.Caching.Memory;

namespace HybirdCacheVideoTest;

public class DataServiceWithMemoryCache(IMemoryCache cache) : DataService
{
    public override async Task<SomeData[]> GetSomeData(int id)
    {
        SomeData[] someDatas = [
            new SomeData(1, $"One"),
            new SomeData(2, $"Two"),
            new SomeData(3, $"Three"),

        ];
        if (!(cache.TryGetValue($"SomeData/{id}", out var data) 
              && data is SomeData[] someData))
        {
            await Task.Delay(2500);
            someData = someDatas;
            cache.Set($"SomeData/{id}", someData, 
                TimeSpan.FromSeconds(10));
        }
        return someData;
    }
}