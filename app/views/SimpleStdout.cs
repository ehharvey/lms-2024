
using System.Reflection;

namespace Lms.Views {
    public class SimpleStdout : IView
    {
        // Stringify a single object, suitable for stdout
        public string Stringify(object o)
        {
            var objectName = o.GetType().Name;
            var properties = o.GetType().GetProperties().ToList().OrderBy((p) => p.Name);

            var maxPropPadding = properties.Select((p) => p.Name.Length).Max();
            
            // The wonky (lack of) tabbing is because we want to return a 
            // multiline string that has no extra spaces!
            //
            // Format: see tests
            return 
$@"#### {objectName} ####
{string.Join("\n", properties.Select(
    (p) => $"{p.Name.PadLeft(maxPropPadding)}: {p.GetValue(o)}"
))}
{"".PadLeft(objectName.Length + 10, '#')}";
}

        // Stringifys multiple objects in a table-like view
        // Possibly compliant with markdown :p
        public string Stringify<T>(IEnumerable<T> objs)
        {
            // Assert that all objects are of same type
            var numberOfTypes = objs.Select((o) => o?.GetType().Name ?? throw new ArgumentException("passed null in objs")).Distinct().ToArray().Length;
            var isAllSame = numberOfTypes == 1;

            if (!isAllSame)
            {
                throw new ArgumentException("multiple types of objs passed");
            }

            var properties = objs.First()?
                                 .GetType()
                                 .GetProperties()
                                 .OrderBy((p) => p.Name) ?? throw new ArgumentException("passed null in objs");

            var propertyPaddings = properties.ToDictionary(
                p => p.Name, 
                p => objs.Select(o =>
                {
                    return p.GetValue(o)?.ToString()?.Length ?? 0;
                })?.Append(p.Name.Length)?.Max() ?? 1
            );
            var header = $"| {string.Join(" | ", properties.Select((p) => p.Name.PadRight(propertyPaddings[p.Name])))} |";
            var headerSeperator = $"| {string.Join(" | ", properties.Select(_ => "-".PadRight(propertyPaddings[_.Name], '-')))} |";

            // The wonky (lack of) tabbing is because we want to return a 
            // multiline string that has no extra spaces!
            // Format: see tests
            return 
$@"{header}
{headerSeperator}
| {string.Join(" |\n| ", objs.Select(
    (o) => string.Join(" | ", properties.Select(
        (p) => p.GetValue(o)?.ToString()?.PadLeft(propertyPaddings[p.Name])
        ))
))} |";
        }
    }
}