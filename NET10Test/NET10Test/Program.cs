Console.WriteLine("Hello C# 14");

var aa = nameof(List<>);
Console.WriteLine(aa);

MyClass my = new();

my?.A = my.GetValue();
//Console.WriteLine(my?.GetValue());


// field, extension

Console.WriteLine(my.GetArray());
Console.WriteLine(my.IsOk);

public static class Extensions
{
    extension(MyClass my)
    {
        public IEnumerable<int> GetArray() => [1, 2, 3];
        public bool IsOk => my.A == 1;
    }
}

public partial class MyClass
{
    public partial MyClass();
    public int field;
    public int A
    {
        get
        {
            return @field;
        }
        set => @field = value;
    }

    private int myVar;

    public int MyProperty
    {
        get { return myVar; }
        set { myVar = value; }
    }

    public int GetValue()
    {
        Console.WriteLine("11");
        return 1;
    }
}

public partial class MyClass
{
    public partial MyClass()
    {
        
    }
}