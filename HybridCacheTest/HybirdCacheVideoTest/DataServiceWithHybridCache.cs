using Microsoft.Extensions.Caching.Hybrid;

namespace HybirdCacheVideoTest;

public class DataServiceWithHybridCache(HybridCache cache) : DataService
{
    public override async Task<SomeData[]> GetSomeData(int id)
    {
        var data = await cache.GetOrCreateAsync($"SomeData/{id}",
            async token =>
            {
                await Task.Delay(2500);
                return new SomeData[]
                    {
                        new SomeData(1, $"One"),
                        new SomeData(2, $"Two"),
                        new SomeData(3, $"Three"),

                    }
                    ;
            } 
            
        );
        return data;
    }
}