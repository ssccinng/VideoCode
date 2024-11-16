// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


public class TestSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this one
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // begin creating the source we'll inject into the users compilation
        var sourceBuilder = new StringBuilder(@"