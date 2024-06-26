using DevoteamRobotAssignment.Objects;
using DevoteamRobotAssignment.Services;

namespace DevoteamRobotAssignment.Tests.Services;

public class RoomSetupServiceTest
{
    [Fact]
    public void SetRoomArea_WithTwoDigitsWithSpace_ShouldReturnRoomWithCorrectValues()
    {
        // Arrange
        Console.SetIn(new StringReader("1 1"));

        var roomSetupService = new RoomSetupService();

        var expectedResult = new Room
        {
            Width = 1,
            Depth = 1,
        };

        // Act
        var result = roomSetupService.SetRoomArea();

        // Assert
        Assert.Equal(expectedResult.Width, result.Width);
        Assert.Equal(expectedResult.Depth, result.Depth);
    }

    [Fact]
    public void SetRoomArea_WithIncorrectAmountOfDigits_ShouldReturnArgumentException()
    {
        // Arrange
        Console.SetIn(new StringReader("1"));

        var roomSetupService = new RoomSetupService();

        var expectedErrorMessage = "Invalid input, please type only two digits with a space between.";

        // Act
        var exception = Assert.Throws<ArgumentException>(roomSetupService.SetRoomArea);

        // Assert
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void SetRoomArea_WithLetters_ShouldReturnArgumentException()
    {
        // Arrange
        Console.SetIn(new StringReader("A B"));

        var roomSetupService = new RoomSetupService();

        var expectedErrorMessage = "Invalid input, type only digits.";

        // Act
        var exception = Assert.Throws<ArgumentException>(roomSetupService.SetRoomArea);

        // Assert
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void SetRoomArea_WithNegativeDigits_ShouldReturnArgumentException()
    {
        // Arrange
        Console.SetIn(new StringReader("-3 1"));

        var roomSetupService = new RoomSetupService();

        var expectedErrorMessage = "Digits must have value 0 or more.";

        // Act
        var exception = Assert.Throws<ArgumentException>(roomSetupService.SetRoomArea);

        // Assert
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void SetRobotPosition_WithTwoDigitsAndDirection_ShouldReturnRobtPositionWithCorrectValues()
    {
        // Arrange
        Console.SetIn(new StringReader("1 1 N"));

        var roomSetupService = new RoomSetupService();

        var room = new Room
        {
            Width = 3,
            Depth = 3,
        };

        var expectedRobotPosition = new RobotPosition
        {
            Width = 1,
            Depth = 1,
            Direction = Orientation.N
        };

        // Act
        var result = roomSetupService.SetRobotPosition(room);

        // Assert
        Assert.Equal(expectedRobotPosition.Width, result.Width);
        Assert.Equal(expectedRobotPosition.Depth, result.Depth);
        Assert.Equal(expectedRobotPosition.Direction, result.Direction);
    }

    [Fact]
    public void SetRobotPosition_WithUnexistingDirection_ShouldReturnArgumentException()
    {
        // Arrange
        Console.SetIn(new StringReader("1 1 T"));

        var roomSetupService = new RoomSetupService();

        var room = new Room
        {
            Width = 3,
            Depth = 3,
        };

        var expectedErrorMessage = "Invalid input, type two numbers and a valid direction.";

        // Act
        var exception = Assert.Throws<ArgumentException>(() => roomSetupService.SetRobotPosition(room));

        // Assert
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void SetRobotPosition_WithOutOfBoundPosition_ShouldReturnArgumentException()
    {
        // Arrange
        Console.SetIn(new StringReader("3 3 S"));

        var roomSetupService = new RoomSetupService();

        var room = new Room
        {
            Width = 1,
            Depth = 1,
        };

        var expectedErrorMessage = "Position is out of bounds. Robot needs to be in room area.";

        // Act
        var exception = Assert.Throws<ArgumentException>(() => roomSetupService.SetRobotPosition(room));

        // Assert
        Assert.Equal(expectedErrorMessage, exception.Message);
    }
}