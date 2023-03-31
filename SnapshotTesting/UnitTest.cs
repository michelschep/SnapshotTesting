using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Serilog;
using SnapshotTesting.Refactoring.FirstExampleTests;
using VerifyTests.Serilog;

namespace SnapshotTesting;


[UsesVerify]
public class UnitTest
{
    private Calculator _calculator;

    [ModuleInitializer]
    public static void Initialize() =>
        VerifySerilog.Initialize();

    public UnitTest()
    {
        var loggerFactory = new LoggerFactory()
            .AddSerilog(Log.Logger);

        _calculator = new Calculator(loggerFactory.CreateLogger("Logger"));
    }
    
    [Fact]
    public void Multiply_MultipleNumbers_ReturnsMultiplication()
    {
        // Arrange

        // Act
        var actual = _calculator.Multiply(6, 7);

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
        var settings = new VerifySettings();
        settings.ScrubLinesWithReplace(s =>
        {
            var pattern = @"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}";
            var regex = new Regex(pattern);
            var r = regex.Replace(s, "DATETIME");
            return r;
        });
        
        return Verify(statement, settings);
    }

    [Fact]
    public Task SnaphotTestingAndLogging()
    {
        // Arrange
        RecordingLogger.Start();

        // Act
        var actual = _calculator.Multiply(6, 7);
        
        // Assert
        return Verify(actual);
    }
}