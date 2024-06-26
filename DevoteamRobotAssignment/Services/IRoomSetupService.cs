using DevoteamRobotAssignment.Objects;

namespace DevoteamRobotAssignment.Services
{
    public interface IRoomSetupService
    {
        Room SetRoomArea();

        RobotPosition SetRobotPosition(Room roomDetails);
    }
}
