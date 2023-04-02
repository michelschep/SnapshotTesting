using FluentAssertions;
using SnapshotTesting.Refactoring.FirstExampleTests;

namespace SnapshotTesting;

public class BasicExample
{
    [Fact]
    public void CreateReport_ShouldBeAsExpected()
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
            { "hamlet", new Play("Hamlet", "tragedy") },
            { "as-like", new Play("As You Like It", "comedy") },
            { "othello", new Play("Othello", "tragedy") }
        };

        // Act
        var actual = theater.Statement(invoice, plays);

        // Assert
        // Init
        var expected = @"???";

        actual.Should().Be(expected);
        // Demo
        // ....;
    }
}