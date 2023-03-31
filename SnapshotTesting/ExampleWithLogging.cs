using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Serilog;
using VerifyTests.Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace SnapshotTesting;

[UsesVerify]
public class ExampleWithLogging
{
    private readonly Calculator _calculator;

    [ModuleInitializer]
    public static void Initialize()
    {
        DerivePathInfo(
            (sourceFile, projectDirectory, type, method) =>
            {
                return new(
                    directory: Path.Combine(projectDirectory, "VerifiedSnapshots"),
                    typeName: type.Name,
                    methodName: method.Name);
            });
        
        VerifySerilog.Initialize();
    }

    public ExampleWithLogging()
    {
        var loggerFactory = new LoggerFactory()
            .AddSerilog(Log.Logger);

        _calculator = new Calculator(loggerFactory.CreateLogger("Logger"));
    }

    [Fact]
    public Task SnapshotTestingAndLogging()
    {
        // Arrange
        RecordingLogger.Start();

        // Act
        var actual = _calculator.Multiply(6, 7);

        // Assert
        return Verify(actual);
    }

    class Calculator
    {
        private readonly ILogger _logger;

        public Calculator(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public int Multiply(int x, int y)
        {
            _logger.LogInformation($"Multiply {x} and {y}");
            var result = 42;
            _logger.LogInformation($"Multiplication of {x} and {y} is {result}");
            return result;
        }
    }
}