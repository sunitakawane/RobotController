using RobotController.Robots;
using System.Collections.Generic;

namespace RobotController
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new GridMap { MaxRows = 10, MaxCols = 10 };
            //Create Obstructions map
            grid.CreateGridTopographyManually();
            grid.Display();

            IRobot rob1 = new SimpleRobot(1, 0, 0, 'N', grid);
            IRobot rob2 = new SimpleRobot(2, 2, 3, 'N', grid);

            grid.RobotsInGrid = new List<SimpleRobot> { (SimpleRobot)rob1, (SimpleRobot)rob2 };
            rob1.Display();
            // Series of Input commands 
            string command = "LFFRRRFFLR";
            rob1.Navigate(command);

            rob2.Display();
            rob2.Navigate(command);
        }
    }
}
