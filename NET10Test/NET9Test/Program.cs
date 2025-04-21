Console.WriteLine("Hello C# 14");

var aa = nameof(List<>);
Console.WriteLine(aa);

MyClass my = null;

my?.A = 1;
Console.WriteLine(my?.GetValue());


// field, extension

public class MyClass
{
    public int A { get; set; }
    public int GetValue()
    {
        return 1;
    }
}