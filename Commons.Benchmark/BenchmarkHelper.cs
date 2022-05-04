using System.Collections.ObjectModel;
using System.Reflection;
using Xunit;

namespace Commons.Benchmark;

internal static class BenchmarkHelper
{
    public static List<object?[]> GetInlineData<TType>(string methodName)
    {
        var type = typeof(TType);
        var member = type.GetMethod(methodName);
        if (member == null) throw new Exception($"Could not find a method named '{methodName}' on type '{type}'.");
        return member.CustomAttributes.Where(a => a.AttributeType == typeof(InlineDataAttribute))
            .Select(a => (a.ConstructorArguments[0].Value as ReadOnlyCollection<CustomAttributeTypedArgument>)?.Select(v => v.Value).ToArray()).ToList()!;
    }
}