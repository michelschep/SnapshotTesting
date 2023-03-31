using FluentAssertions;

namespace SnapshotTesting;

public class UnitTest
{
    [Fact]
    public void Test()
    {
        // Arrange
        var expected = true;

        // Act
        var actual = false;

        // Assert
        actual.Should().Be(expected);
    }
}