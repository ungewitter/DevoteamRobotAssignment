using DevoteamRobotAssignment.Objects;

namespace DevoteamRobotAssignment.Services
{
    public class RoomSetupService : IRoomSetupService
    {
        public RoomSetupService() { }

        public Room SetRoomArea()
        {
            var digits = GetDigitsFromInput();

            if (digits.Length != 2)
            {
                throw new ArgumentException("Invalid input, please type only two digits with a space between.");
            }

            if (int.TryParse(digits[0], out var roomWidth) && int.TryParse(digits[1], out var roomDepth))
            {
                if (roomWidth >= 0 && roomDepth >= 0)
                {
                    return new Room
                    {
                        Width = roomWidth,
                        Depth = roomDepth,
                    };
                }
                else
                {
                    throw new ArgumentException("Digits must have value 0 or more.");
                }
            }
            else
            {
                throw new ArgumentException("Invalid input, type only digits.");
            }
        }

        public RobotPosition SetRobotPosition(Room roomDetails)
        {
            var digits = GetDigitsFromInput();

            if (digits.Length != 3)
            {
                throw new ArgumentException("Invalid input, please type only two digits with a space between.");
            }

            if (int.TryParse(digits[0], out var positionWidth)
                && int.TryParse(digits[1], out var positionDepth)
                && Enum.TryParse<Orientation>(digits[2], out var direction))
            {
                if (positionWidth < 0 || positionDepth < 0)
                {
                    throw new ArgumentException("Digits must have value 0 or more.");
                }
                else if (positionWidth > roomDetails.Width || positionDepth > roomDetails.Depth)
                {
                    throw new ArgumentException("Position is out of bounds. Robot needs to be in room area.");
                }
                else
                {
                    return new RobotPosition
                    {
                        Width = positionWidth,
                        Depth = positionDepth,
                        Direction = direction
                    };
                }

            }
            else
            {
                throw new ArgumentException("Invalid input, type two numbers and a valid direction.");
            }
        }

        private string[] GetDigitsFromInput()
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input is empty. Try again.");
            }

            return input.Trim().Split(' ');
        }
    }
}
