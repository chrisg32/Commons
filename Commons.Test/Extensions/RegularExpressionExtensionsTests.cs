using System.Text.RegularExpressions;
using CG.Commons.Extensions;
using FluentAssertions;
using Xunit;

namespace CG.Commons.Test.Extensions;

public class RegularExpressionExtensionsTests
{
    [Fact]
    public void TestReplaceGroups_Array()
    {
        // Arrange
        var s = "Hello Jon, please give me 72 tacos!";
        var regex = new Regex(@"Hello (.+), please give me (\d+) tacos!");
        
        // Act
        var result = regex.ReplaceGroups(s, "John", "42");
        
        // Assert
        result.Should().Be("Hello John, please give me 42 tacos!");
    }
    
    [Fact]
    public void TestReplaceGroups_Func()
    {
        // Arrange
        var s = "Hello Jon, please give me 72 tacos!";
        var regex = new Regex(@"Hello (.+), please give me (\d+) tacos!");
        
        // Act
        var result = regex.ReplaceGroups(s, i => $"{{{i}}}");
        
        // Assert
        result.Should().Be("Hello {0}, please give me {1} tacos!");
    }
}