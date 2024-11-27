using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace HybirdCacheVideoTest;

public class DataServiceWithDistributedCache(IDistributedCache cache):DataService
{
    public override async Task<SomeData[]> GetSomeData(int id)
    {
        if (cache.Get("SomeData") is byte[] data)
        {
            return JsonSerializer.Deserialize<SomeData[]>(data);
        }
        else
        {
            SomeData[] data1 = [
                new SomeData(1, "One"),
                new SomeData(2, "Two"),
                new SomeData(3, "Three")    
            ];
            
            cache.Set("SomeData", JsonSerializer.SerializeToUtf8Bytes(data1), 
                new DistributedCacheEntryOptions());
            await Task.Delay(1000);
            return data1;
        }
    }
}