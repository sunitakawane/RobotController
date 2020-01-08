using System;
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

            ARobot rob1 = new ARobot { Rid = 1, Direction = 'N', LocX = 0, LocY = 0, Grid = grid };
            ARobot rob2 = new ARobot { Rid = 2, Direction = 'N', LocX = 2, LocY = 3, Grid = grid };
            grid.RobotsInGrid = new List<Robot> { rob1, rob2 };
            rob1.Display();
            // Series of Input commands 
            string command = "LFFRRRFFLR";
            rob1.Navigate(command);

            rob2.Display();
            rob2.Navigate(command);
        }
    }
}
