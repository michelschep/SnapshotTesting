using Microsoft.Extensions.Logging;

namespace SnapshotTesting;

public class Calculator
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