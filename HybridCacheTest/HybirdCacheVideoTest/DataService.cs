namespace HybirdCacheVideoTest;

public record SomeData(int Id, string Name);
public class DataService
{
    public virtual async Task<SomeData[]> GetSomeData(int id)
    {
        await Task.Delay(2500);
        return
        [
            new SomeData(1, $"One"),
            new SomeData(2, $"Two"),
            new SomeData(3, $"Three"),

        ];
    }
}