using System;
namespace RobotController
{
    public class Robot
    {
        //robot number
        public int Rid { get; set; }
        //Initial Direction
        public char Direction { get; set; }
        //Starting Location 
        public int LocX { get; set; }
        public int LocY { get; set; }
        //Grid to navigate
        public GridMap Grid { get; set; }
    }

    public class ARobot : Robot
    {
        public void Display()
        {
            Console.WriteLine("Robot " + Rid + " Start At: [{0},{1}] Facing: ({2})", LocX, LocY, Direction);
        }
        public void Navigate(string command)
        {
            Console.WriteLine("Command: " + command);
            Console.WriteLine("Robot " + Rid + " Path: ");
            foreach (char i in command)
            {
                Move(char.ToUpper(i));
                Console.WriteLine("Command {0}: Location: [{1},{2}] Facing: ({3})"
                    , i, LocX, LocY, Direction);
            }

        }
        public void Move(char instruction)
        {
            Obstructions targetObs;
            int newX = LocX;
            int newY = LocY;
            if ((instruction == 'F' && Direction == 'E') ||
                (instruction == 'L' && Direction == 'S') ||
                (instruction == 'R' && Direction == 'N'))
            {
                newX += 1;
            }
            if ((instruction == 'F' && Direction == 'N') ||
                (instruction == 'L' && Direction == 'E') ||
                (instruction == 'R' && Direction == 'W'))
            {
                newY += 1;
            }
            if ((instruction == 'F' && Direction == 'W') ||
                (instruction == 'L' && Direction == 'N') ||
                (instruction == 'R' && Direction == 'S'))
            {
                newX -= 1;
            }
            if ((instruction == 'F' && Direction == 'S') ||
                (instruction == 'L' && Direction == 'W') ||
                (instruction == 'R' && Direction == 'E'))
            {
                newY -= 1;
            }
            //check for boundary conditions
            if (newX >= Grid.MaxCols || newY >= Grid.MaxRows || newX < 0 || newY < 0)
            {
                return;
            }

            //check for other robots
            if (Grid.RobotsInGrid.Count > 1)
            {
                foreach (var t in Grid.RobotsInGrid)
                {
                    //skip current robot
                    if (t.Rid == Rid) continue;
                    //if another robot present at Next location , Do not move
                    if (t.LocX == newX && t.LocY == newY)
                    {
                        return;
                    }
                }
            }
            //Check for obstructions
            targetObs = Grid.Topography[newX, newY];
            switch (targetObs.Type)
            {
                case 'U':
                    break;
                case 'P':
                    LocX = newX;
                    LocY = newY;
                    break;
                case 'R':
                    break;
                case 'H':
                    var hole = (Hole)targetObs;
                    LocX = hole.endPosition[0];
                    LocY = hole.endPosition[1];
                    break;
                case 'S':
                    var spinner = (Spinner)targetObs;
                    LocX = newX;
                    LocY = newY;
                    if ((spinner.spinAngle == 90 && Direction == 'N') ||
                        (spinner.spinAngle == 180 && Direction == 'W') ||
                        (spinner.spinAngle == 0 && Direction == 'E') ||
                        (spinner.spinAngle == 270 && Direction == 'S'))
                    {
                        Direction = 'E';
                    }
                    else if ((spinner.spinAngle == 90 && Direction == 'S') ||
                        (spinner.spinAngle == 180 && Direction == 'E') ||
                        (spinner.spinAngle == 0 && Direction == 'W') ||
                        (spinner.spinAngle == 270 && Direction == 'N'))
                    {
                        Direction = 'W';
                    }
                    else if ((spinner.spinAngle == 90 && Direction == 'E') ||
                        (spinner.spinAngle == 180 && Direction == 'N') ||
                        (spinner.spinAngle == 0 && Direction == 'S') ||
                        (spinner.spinAngle == 270 && Direction == 'W'))
                    {
                        Direction = 'S';
                    }
                    else if ((spinner.spinAngle == 90 && Direction == 'W') ||
                        (spinner.spinAngle == 180 && Direction == 'S') ||
                        (spinner.spinAngle == 0 && Direction == 'N') ||
                        (spinner.spinAngle == 270 && Direction == 'E'))
                    {
                        Direction = 'N';
                    }
                    break;

            }
        }
    }
    //Other type of Robot with different implementation for obstruction types
    public class BRobot : Robot
    {
        //robot move
        public void Move(char instruction)
        {

        }
    }
}