using FluentAssertions;
using SnapshotTesting.Refactoring.FirstExampleTests;

namespace SnapshotTesting;

[UsesVerify]
public class UnitTest
{
    [Fact]
    public void Multiply_MultipleNumbers_ReturnsMultiplication()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var actual = calculator.Multiply(6, 7);

        // Assert
        actual.Should().Be(42);
    }
    
    [Fact]
    public Task CreateReport_ShouldBeAsExpected()
    {
        // Arrange
        var theater = new Theater();

        var performances = new[]
        {
            new Performance("hamlet", 55),
            new Performance("as-like", 35),
            new Performance("othello", 40),
        };
        var invoice = new Invoice("BigCo", performances);
        var plays = new Dictionary<string, Play>
        {
            {"hamlet", new Play("Hamlet", "tragedy")},
            {"as-like", new Play("As You Like It", "comedy")},
            {"othello", new Play("Othello", "tragedy")}
        };
        
        // Act
        var statement = theater.Statement(invoice, plays);

        // Assert
//        statement.Should().Be("???");
        return Verifier.Verify(statement);
    }
    
    
}