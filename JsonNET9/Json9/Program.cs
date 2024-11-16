using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

JsonElement left = JsonDocument.Parse("""{"a": 0.001, "b": "test"}""").RootElement;
JsonElement right = JsonDocument.Parse("""{"a": 1e-3, "b": "test"}""").RootElement;

Console.WriteLine(JsonElement.DeepEquals(left, right).ToString());