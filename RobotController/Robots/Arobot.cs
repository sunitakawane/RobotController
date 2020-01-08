using System;

namespace RobotController.Robots
{
    public abstract class ARobot
    {
        //robot number
        public int _Rid { get; set; }
        /// current location of the robot
        protected int _LocX { get; set; }
        protected int _LocY { get; set; }
        protected char _Direction { get; set; }
        //G_Rid to navigate
        protected GridMap _grid { get; set; }
        
        /// Builds a robot
        public ARobot(int rid, int x, int y, char startingDirection, GridMap grid)
        {
            _Rid = rid;
            _LocX = x;
            _LocY = y;
            _Direction = startingDirection;
            _grid = grid;
        }
        public int[] GetLocation()
        {
            return new int[] { _LocX, _LocY};
        }
        public char GetDirection()
        {
            return _Direction;
        }

        public void Display()
        {
            Console.WriteLine("Robot " + _Rid + " Start At: [{0},{1}] Facing: ({2})", _LocX, _LocY, _Direction);
        }
        
    }
}