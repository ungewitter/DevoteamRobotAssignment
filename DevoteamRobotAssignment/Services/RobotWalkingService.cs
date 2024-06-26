using DevoteamRobotAssignment.Objects;

namespace DevoteamRobotAssignment.Services
{
    public class RobotWalkingService : IRobotWalkingService
    {
        public RobotWalkingService() { }

        public RobotPosition UseWalkingRobot(Room roomDetails, RobotPosition robotPosition)
        {
            var commands = GetCommandsFromInput();

            foreach (var command in commands)
            {
                if (Enum.TryParse<WalkingCommands>(command.ToString(), out var commandResult))
                {
                    if (commandResult == WalkingCommands.F)
                    {
                        robotPosition = WalkForward(robotPosition);
                    }
                    else if (commandResult == WalkingCommands.L || commandResult == WalkingCommands.R)
                    {
                        robotPosition.Direction = ChangeOrientation(robotPosition, commandResult);
                    }
                }
                else
                {
                    throw new ArgumentException("Type only commands F, L or R.");
                }

                if (!CheckValidPosition(roomDetails, robotPosition))
                {
                    throw new ArgumentException($"ERROR: Out of bounds at {robotPosition.Width} {robotPosition.Depth}", "outOfBounds");
                };
            }

            return robotPosition;
        }

        private char[] GetCommandsFromInput()
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input is empty. Try again.");
            }

            return input.Where(command => !char.IsWhiteSpace(command)).ToArray();
        }

        private RobotPosition WalkForward(RobotPosition robotPosition)
        {
            switch (robotPosition.Direction)
            {
                case Orientation.N:
                    robotPosition.Depth++;
                    break;
                case Orientation.E:
                    robotPosition.Width++;
                    break;
                case Orientation.S:
                    robotPosition.Depth--;
                    break;
                case Orientation.W:
                    robotPosition.Width--;
                    break;
            }

            return robotPosition;
        }

        private Orientation ChangeOrientation(RobotPosition position, WalkingCommands command)
        {
            if (command == WalkingCommands.L)
            {
                if (position.Direction == Orientation.N)
                {
                    return Orientation.W;
                }

                return position.Direction - 1;
            }
            else if (command == WalkingCommands.R)
            {
                if (position.Direction == Orientation.W)
                {
                    return Orientation.N;
                }

                return position.Direction + 1;
            }
            else
            {
                return position.Direction;
            }
        }

        private bool CheckValidPosition(Room roomDetails, RobotPosition position)
        {
            if (position.Width > roomDetails.Width || position.Depth > roomDetails.Depth)
            {
                return false;
            }
            else if (position.Width < 0 || position.Depth < 0)
            {
                return false;
            }
            else { return true; }
        }
    }
}
