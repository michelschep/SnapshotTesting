using System.Runtime.CompilerServices;

namespace SnapshotTesting;

[UsesVerify]
public class ExampleWithParameterizedTest // Add how to use parameterised tests!
{
    [Theory]
    [InlineData(6, 7)]
    [InlineData(9, 7)]
    public Task Test(int x, int y)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var actual = calculator.Multiply(x, y);
        
        // Assert
        var settings = new VerifySettings();
        settings.UseParameters(x, y);
        return Verify(actual, settings);
    }

    class Calculator
    {
        public CalculationResult Multiply(int x, int y)
        {
            return new CalculationResult(x, y, x * y);
        }
    }

    class CalculationResult
    {
        public int X { get; }
        public int Y { get; }
        public int Result { get; }

        public CalculationResult(int x, int y, int result)
        {
            X = x;
            Y = y;
            Result = result;
        }
    }
}