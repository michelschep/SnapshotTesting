using SnapshotTesting.Refactoring.FirstExampleTests;

namespace SnapshotTesting;

[UsesVerify]
public class BasicExample
{
    [Fact]
    public async Task CreateReport_ShouldBeAsExpected()
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
        await Verify(actual);
    }
}