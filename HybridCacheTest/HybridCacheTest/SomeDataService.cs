using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;

namespace HybridCacheTest;
public record SomeData(int Id, string Name);

public class SomeDataService
{
    public virtual async Task<SomeData[]> GetSomeData(int id, CancellationToken cancellationToken)
    {
       
            SomeData[] data1 = [
                new SomeData(1, "One"),
                new SomeData(2, "Two"),
                new SomeData(3, "Three")    
            ];
            
            await Task.Delay(1000);
            return data1;
        
    }
}
public class SomeDataIDsService(LocalCache cache) : SomeDataService
{
    public virtual async Task<SomeData[]> GetSomeData(int id, CancellationToken cancellationToken)
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
            
            cache.Set("SomeData", JsonSerializer.SerializeToUtf8Bytes(data1), new DistributedCacheEntryOptions());
            await Task.Delay(1000);
            return data1;
        }
        
    }
}

public class SomeHybridService(HybridCache cache): SomeDataService
{
    public override async Task<SomeData[]> GetSomeData(int id, CancellationToken cancellationToken)
    {
        
         var data = await cache.GetOrCreateAsync(
            $"SomeData/{id}",
            async cancellation =>
            {
                // valuetask注意
                await Task.Delay(2500);
                return new SomeData[]
                {
                    new(1, "One"),
                    new(2, "Two"),
                    new(3, "Three")
                };
            },
            cancellationToken: cancellationToken);

        return data;
    }
}