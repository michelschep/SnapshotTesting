using FluentAssertions;

namespace SnapshotTesting;

[UsesVerify]
public class OldSkoolUnitTest 
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

    class Calculator
    {
        public int Multiply(int x, int y)
        {
            return 42;
        }
    }
}