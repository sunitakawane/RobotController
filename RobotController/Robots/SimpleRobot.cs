using System;

namespace RobotController.Robots
{
    public class SimpleRobot : ARobot, IRobot
    {
        public SimpleRobot(int Rid ,int x, int y, char startingDirection, GridMap grid) : base(Rid, x, y, startingDirection, grid) { }

        public void Navigate(string command)
        {
            Console.WriteLine("Command: " + command);
            Console.WriteLine("Robot " + _Rid + " Path: ");
            foreach (char i in command)
            {
                Move(char.ToUpper(i));
                Console.WriteLine("Command {0}: Location: [{1},{2}] Facing: ({3})"
                    , i, _LocX, _LocY, _Direction);
            }

        }
        public void Move(char instruction)
        {
            Obstructions targetObs;
            int newX = _LocX;
            int newY = _LocY;
            if ((instruction == 'F' && _Direction == 'E') ||
                (instruction == 'L' && _Direction == 'S') ||
                (instruction == 'R' && _Direction == 'N'))
            {
                newX += 1;
            }
            if ((instruction == 'F' && _Direction == 'N') ||
                (instruction == 'L' && _Direction == 'E') ||
                (instruction == 'R' && _Direction == 'W'))
            {
                newY += 1;
            }
            if ((instruction == 'F' && _Direction == 'W') ||
                (instruction == 'L' && _Direction == 'N') ||
                (instruction == 'R' && _Direction == 'S'))
            {
                newX -= 1;
            }
            if ((instruction == 'F' && _Direction == 'S') ||
                (instruction == 'L' && _Direction == 'W') ||
                (instruction == 'R' && _Direction == 'E'))
            {
                newY -= 1;
            }
            //check for boundary conditions
            if (newX >= _grid.MaxCols || newY >= _grid.MaxRows || newX < 0 || newY < 0)
            {
                return;
            }

            //check for other robots
            if (_grid.RobotsInGrid.Count > 1)
            {
                foreach (var t in _grid.RobotsInGrid)
                {
                    //skip current robot
                    if (t._Rid == _Rid) continue;
                    //if another robot present at Next location , Do not move
                    if (t._LocX == newX && t._LocY == newY)
                    {
                        return;
                    }
                }
            }
            //Check for obstructions
            targetObs = _grid.Topography[newX, newY];
            switch (targetObs.Type)
            {
                case 'U':
                    break;
                case 'P':
                    _LocX = newX;
                    _LocY = newY;
                    break;
                case 'R':
                    break;
                case 'H':
                    var hole = (Hole)targetObs;
                    _LocX = hole.endPosition[0];
                    _LocY = hole.endPosition[1];
                    break;
                case 'S':
                    var spinner = (Spinner)targetObs;
                    _LocX = newX;
                    _LocY = newY;
                    if ((spinner.spinAngle == 90 && _Direction == 'N') ||
                        (spinner.spinAngle == 180 && _Direction == 'W') ||
                        (spinner.spinAngle == 0 && _Direction == 'E') ||
                        (spinner.spinAngle == 270 && _Direction == 'S'))
                    {
                        _Direction = 'E';
                    }
                    else if ((spinner.spinAngle == 90 && _Direction == 'S') ||
                        (spinner.spinAngle == 180 && _Direction == 'E') ||
                        (spinner.spinAngle == 0 && _Direction == 'W') ||
                        (spinner.spinAngle == 270 && _Direction == 'N'))
                    {
                        _Direction = 'W';
                    }
                    else if ((spinner.spinAngle == 90 && _Direction == 'E') ||
                        (spinner.spinAngle == 180 && _Direction == 'N') ||
                        (spinner.spinAngle == 0 && _Direction == 'S') ||
                        (spinner.spinAngle == 270 && _Direction == 'W'))
                    {
                        _Direction = 'S';
                    }
                    else if ((spinner.spinAngle == 90 && _Direction == 'W') ||
                        (spinner.spinAngle == 180 && _Direction == 'S') ||
                        (spinner.spinAngle == 0 && _Direction == 'N') ||
                        (spinner.spinAngle == 270 && _Direction == 'E'))
                    {
                        _Direction = 'N';
                    }
                    break;

            }
        }

        
    }
}