// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var test = new TestPartial();
System.Console.WriteLine(test.Name);

string testStr = "1465qwe";

var match = test.partialRegex().Match(testStr);

var match2 = test.PropPartialRegex.Match(testStr);

System.Console.WriteLine(match.Value);
System.Console.WriteLine(match2.Value);


public partial class TestPartial
{
    [GeneratedRegex(@"\d+")]
    public partial Regex partialRegex();

    [GeneratedRegex(@"\d+")]
    public partial Regex PropPartialRegex { get; }

    public partial string Name { get; set; }

    public partial string Name { get => "Test"; set{} }
    
}