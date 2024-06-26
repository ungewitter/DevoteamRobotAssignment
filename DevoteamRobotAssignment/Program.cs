using DevoteamRobotAssignment.Objects;
using DevoteamRobotAssignment.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, welcome to the robot simulation. Please input two numbers that will decide the room area.\n");
        Console.WriteLine("The first digit will decide the width, and the second digit decides the depth. Negative digits and 0 is not allowed.\n");
        Console.WriteLine("Please write the two digits with a space between.");

        var roomSetupService = new RoomSetupService();
        Room roomDetails;
        RobotPosition robotPosition;

        while (true)
        {
            try
            {
                roomDetails = roomSetupService.SetRoomArea();
                break;
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine("Now decide the robot position in the room and it's orientation. \nWrite two digits to decide field and a cardinal direction (N, W, S or E).");
        while (true)
        {
            try
            {
                robotPosition = roomSetupService.SetRobotPosition(roomDetails);
                break;
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        var robotWalkingService = new RobotWalkingService();
        Console.WriteLine("Now type navigation commands for the robot. F is Forward, L is left and R is right.");
        while (true)
        {
            try
            {
                robotPosition = robotWalkingService.UseWalkingRobot(roomDetails, robotPosition);
                Console.WriteLine($"Report: {robotPosition.Width} {robotPosition.Depth} {robotPosition.Direction}");
                break;
            }
            catch (ArgumentException argEx) when (argEx.ParamName == "outOfBounds")
            {
                Console.WriteLine(argEx.Message);
                break;
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}