namespace RobotController
{
    public class Obstructions
    {
        //Obstruction type
        public virtual char Type { get; set; }
        //coordinate of obstruction
        public int[] ObsPosition { get; set; }
    }
    //Create specific type of  obstructions, inherited from obstructions
    public class Path : Obstructions
    {
        public override char Type
        {
            get { return 'P'; }
        }
    }
    public class Rock : Obstructions
    {
        public override char Type
        {
            get { return 'R'; }
        }
    }
    public class Hole : Obstructions
    {
        public override char Type
        {
            get { return 'H'; }
        }
        public int[] endPosition { get; set; }
    }
    public class Spinner : Obstructions
    {
        public override char Type
        {
            get { return 'S'; }
        }
        public int spinAngle { get; set; }
    }
    public class Unknown : Obstructions
    {
        public override char Type
        {
            get { return 'U'; }
        }
    }
}
