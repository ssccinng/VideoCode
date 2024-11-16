// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;
using System.Text.Json.Serialization;
// 1.json文档
{

    
    JsonSerializerOptions options = JsonSerializerOptions.Default;
    JsonNode schema = options.GetJsonSchemaAsNode(typeof(Person));
    Console.WriteLine(schema.ToString());
}



{
    JsonSerializerOptions options = new(JsonSerializerOptions.Default)
    {
        PropertyNamingPolicy = JsonNamingPolicy.KebabCaseUpper,
        NumberHandling = JsonNumberHandling.WriteAsString,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
    };

    JsonNode schema = options.GetJsonSchemaAsNode(typeof(Person));
    Console.WriteLine(schema.ToString());
}

{
    var json =
        """
        null
        {
            "name": "John Doe"
        }
        [1,2,3]
        1
        """u8;
    JsonReaderOptions options = new() { AllowMultipleValues = true };
    Utf8JsonReader reader = new(json, options);
    reader.Read();
    Console.WriteLine(reader.TokenType); // Null

    reader.Read();
    Console.WriteLine(reader.TokenType); // StartObject
    reader.Skip();
    
    reader.Read();
    Console.WriteLine(reader.TokenType); // Number
    reader.Skip();

    reader.Read();
    Console.WriteLine(reader.TokenType); // StartArray
    Console.WriteLine(reader.Read()); // False
    
    
    ReadOnlySpan<byte> utf8Json = """[0] [0,1] [0,1,1] [0,1,1,2] [0,1,1,2,3]"""u8;
    ReadOnlySpan<byte> utf8Json1 = Encoding.UTF8.GetBytes( """[0] [0,1] [0,1,1] [0,1,1,2] [0,1,1,2,3]""");
    using var stream = new MemoryStream(utf8Json.ToArray());

    await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<JsonElement>(stream, topLevelValues: true))
    {
        Console.WriteLine(item);
    }
}

{
    var data = JsonSerializer.Serialize(MyEnum.Value1 | MyEnum.Value2); // "Value1, Custom enum value"

    Console.WriteLine(data);
    
    data = JsonSerializer.Serialize(MyEnum.Value2);
    
    Console.WriteLine(data);

}

{
    JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        IndentCharacter = ' ',
        IndentSize = 1,
    };

    Console.WriteLine(JsonSerializer.Serialize(new { Value = 42 }, options));
}

{
    JsonSerializerOptions options = JsonSerializerOptions.Web; // used instead of new(JsonSerializerDefaults.Web);
    JsonSerializer.Serialize(new { Value = 42 }, options); // {"value":42}
    
}

{
    var obj = JsonObject.Create(JsonDocument.Parse("{}").RootElement);
    obj.Insert(0, "value1", "dani");
    obj.RemoveAt(0);
    obj.Insert(0, "value2", "dani1");
    obj.IndexOf("value2");

    Console.WriteLine(obj);
}
[JsonDerivedType(typeof(Derived), "derived")]
record Base;
record Derived(string Name) : Base;

[Flags, JsonConverter(typeof(JsonStringEnumConverter))]
enum MyEnum
{
    Value1 = 1,
    [JsonStringEnumMemberName("Custom enum value")]
    Value2 = 2,
}

class MyPoco
{
    public int NumericValue { get; init; }
}

record Person (string Name, string? Email, int? Age);



