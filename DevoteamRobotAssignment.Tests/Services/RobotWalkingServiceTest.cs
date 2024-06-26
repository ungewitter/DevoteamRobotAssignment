using DevoteamRobotAssignment.Objects;
using DevoteamRobotAssignment.Services;

namespace DevoteamRobotAssignment.Tests.Services;

public class RobotWalkingServiceTest
{
    [Fact]
    public void UseWalkingRobot_WithCommandsInBounds_ShouldGiveNewRobotPosition()
    {
        // Arrange
        Console.SetIn(new StringReader("FRF"));

        var robotWalkingService = new RobotWalkingService();

        var room = new Room
        {
            Width = 4,
            Depth = 4,
        };

        var originalRobotPosition = new RobotPosition
        {
            Depth = 2, 
            Width = 2, 
            Direction = Orientation.N
        };

        var expectedNewRobotPosition = new RobotPosition
        {
            Width = 3,
            Depth = 3,
            Direction = Orientation.E
        };

        // Act
        var result = robotWalkingService.UseWalkingRobot(room, originalRobotPosition);

        // Assert
        Assert.Equal(expectedNewRobotPosition.Width, result.Width);
        Assert.Equal(expectedNewRobotPosition.Depth, result.Depth);
        Assert.Equal(expectedNewRobotPosition.Direction, result.Direction);
    }

    [Fact]
    public void UseWalkingRobot_WithWrongLetters_ShouldReturnArgumentException()
    {
        // Arrange
        Console.SetIn(new StringReader("TEST"));

        var robotWalkingService = new RobotWalkingService();

        var room = new Room
        {
            Width = 4,
            Depth = 4,
        };

        var originalRobotPosition = new RobotPosition
        {
            Depth = 2,
            Width = 2,
            Direction = Orientation.N
        };

        var expectedErrorMessage = "Type only commands F, L or R.";

        // Act
        var exception = Assert.Throws<ArgumentException>(() => robotWalkingService.UseWalkingRobot(room, originalRobotPosition));

        // Assert
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void UseWalkingRobot_WalksOutOfBounds_ShouldReturnArgumentException()
    {
        // Arrange
        Console.SetIn(new StringReader("LFFF"));

        var robotWalkingService = new RobotWalkingService();

        var room = new Room
        {
            Width = 4,
            Depth = 4,
        };

        var originalRobotPosition = new RobotPosition
        {
            Depth = 2,
            Width = 2,
            Direction = Orientation.N
        };

        var expectedErrorMessage = $"ERROR: Out of bounds at -1 2 (Parameter 'outOfBounds')";

        // Act
        var exception = Assert.Throws<ArgumentException>(() => robotWalkingService.UseWalkingRobot(room, originalRobotPosition));

        // Assert
        Assert.Equal(expectedErrorMessage, exception.Message);
    }
}
