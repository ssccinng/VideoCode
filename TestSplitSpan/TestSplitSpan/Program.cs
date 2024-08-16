// See https://aka.ms/new-console-template for more information
using System.Text;
using System;

static string PrettifyArguments(ReadOnlySpan<char> arguments)
{
    var sb = new StringBuilder();

    foreach (var argumentIterator in arguments.Split(','))
    {
        // We split by ',' but usually args have spaces after the ',', like 'int, object'.
        var argument = arguments.Slice(argumentIterator).Trim(' ');

        // We don't have names here, so the argument can just have 'ByRef' or something similar
        int sectionNumber = 0;
        ReadOnlySpan<char> typeName = argument;
        string modifier = string.Empty;
        foreach (var sectionIterator in argument.Split(' '))
        {
            if (sectionNumber == 0)
            {
                typeName = argument.Slice(sectionIterator);
            }
            // Can't directly compare span<char> with a string literal.
            else if (argument.Slice(sectionIterator).EqualsInvariant("ByRef"))
            {
                // TODO: are there any other modifiers?
                modifier = "ref";
            }

            sectionNumber++;
        }

        typeName = PrettifyTypeName(typeName);

        if (sb.Length > 0)
        {
            sb.Append(", ");
        }

        if (!string.IsNullOrEmpty(modifier))
        {
            sb.Append($"{modifier} ");
        }

        sb.Append(typeName);
    }

    return sb.ToString();
}