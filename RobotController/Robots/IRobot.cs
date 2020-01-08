
namespace RobotController.Robots
{
    public interface IRobot
    {
        int[] GetLocation();
        char GetDirection();
        void Display();
        void Navigate(string command);
    }
}