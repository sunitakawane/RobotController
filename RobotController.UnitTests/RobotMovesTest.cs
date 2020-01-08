using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotController.UnitTests
{
    [TestClass]
    public class RobotMovesTest
    {
        GridMap grid;
        ARobot rob, rob1, rob2;
        public RobotMovesTest()
        {
            grid = new GridMap() { MaxRows = 20, MaxCols = 20 };
            rob = new ARobot() { Rid = 1, Direction = 'N', LocX = 9, LocY = 8, Grid = grid };
            grid.RobotsInGrid = new List<Robot> { rob };
            grid.CreateGridTopographyManually();
        }
        [TestMethod]
        public void RobotDoNotMove_InvalidInstruction()
        {
            //robot at (9,8) 'N'
            rob = new ARobot() { Rid = 1, Direction = 'N', LocX = 9, LocY = 8, Grid = grid };
            rob.Move('s');
            Assert.AreEqual(rob.Direction, 'N');
            Assert.AreEqual(rob.LocX, 9);
            Assert.AreEqual(rob.LocY, 8);
        }
        [TestMethod]
        public void RobotDoNotMove_RockOrUnkownOrBorder()
        {
            //Test rock in Front
            //robot at (9,8) 'N'
            rob = new ARobot() { Rid = 1, Direction = 'N', LocX = 9, LocY = 8, Grid = grid };
            //rock at (9,9) , Do not move Front
            grid.Topography[9, 9] = new Rock()
            {
                ObsPosition = new int[] { 9, 9 }
            };
            rob.Move('F');
            Assert.AreEqual(rob.Direction, 'N');
            Assert.AreEqual(rob.LocX, 9);
            Assert.AreEqual(rob.LocY, 8);

            //Test Unknown on Left
            //robot at (9,8) 'N'
            rob = new ARobot() { Rid = 1, Direction = 'N', LocX = 9, LocY = 8, Grid = grid };
            //Unknown at (8,8) , Do not move to Left
            grid.Topography[8, 8] = new Unknown()
            {
                ObsPosition = new int[] { 8, 8 }
            };
            rob.Move('L');
            Assert.AreEqual(rob.Direction, 'N');
            Assert.AreEqual(rob.LocX, 9);
            Assert.AreEqual(rob.LocY, 8);

            //Test On the border
            //robot at (19,18) 'N'
            //Out of bouds on Move to Right : Do Not Move
            rob = new ARobot() { Rid = 1, Direction = 'N', LocX = 19, LocY = 18, Grid = grid };
            rob.Move('R');
            Assert.AreEqual(rob.Direction, 'N');
            Assert.AreEqual(rob.LocX, 19);
            Assert.AreEqual(rob.LocY, 18);

            //robot at (19,19) 'N'
            //Move Front / Left / Right Do not move
            rob = new ARobot() { Rid = 1, Direction = 'N', LocX = 19, LocY = 19, Grid = grid };
            rob.Move('F');
            Assert.AreEqual(rob.Direction, 'N');
            Assert.AreEqual(rob.LocX, 19);
            Assert.AreEqual(rob.LocY, 19);
        }

        [TestMethod]
        public void RobotMoveTo_Path()
        {
            //Test Path in Front
            //robot at (9,8) 'N'
            rob = new ARobot() { Rid = 1, Direction = 'N', LocX = 9, LocY = 8, Grid = grid };
            //Path at (9,9) , Move Front to (9,9)
            grid.Topography[9, 9] = new Path()
            {
                ObsPosition = new int[] { 9, 9 }
            };
            rob.Move('F');
            Assert.AreEqual(rob.Direction, 'N');
            Assert.AreEqual(rob.LocX, 9);
            Assert.AreEqual(rob.LocY, 9);
        }
        [TestMethod]
        public void RobotMoveTo_Hole()
        {
            //robot at (9,8) 'N'
            rob = new ARobot() { Rid = 1, Direction = 'N', LocX = 9, LocY = 8, Grid = grid };
            //hole at (9,9) connected to (7,7)
            grid.Topography[9, 9] = new Hole()
            {
                ObsPosition = new int[] { 9, 9 },
                endPosition = new int[] { 7, 7 }
            };
            rob.Move('F');
            Assert.AreEqual(rob.Direction, 'N');
            Assert.AreEqual(rob.LocX, 7);
            Assert.AreEqual(rob.LocY, 7);
        }
        [TestMethod]
        public void RobotMoveTo_Spinner()
        {
            //robot at (9,8) 'N'
            rob = new ARobot() { Rid = 1, Direction = 'N', LocX = 9, LocY = 8, Grid = grid };
            //move to spinner at (9,9) with spinAngle 90 , changes Direction to 'E'
            grid.Topography[9, 9] = new Spinner()
            {
                ObsPosition = new int[] { 9, 9 },
                spinAngle = 90
            };
            rob.Move('F');
            Assert.AreEqual(rob.Direction, 'E');
            Assert.AreEqual(rob.LocX, 9);
            Assert.AreEqual(rob.LocY, 9);
            //move Right to spinner at (9,8) with spinAngle 90 , changes Direction From 'E' to 'S'
            grid.Topography[9, 8] = new Spinner()
            {
                ObsPosition = new int[] { 9, 8 },
                spinAngle = 90
            };
            rob.Move('R');
            Assert.AreEqual(rob.Direction, 'S');
            Assert.AreEqual(rob.LocX, 9);
            Assert.AreEqual(rob.LocY, 8);
            //move Right to spinner at (8,8) with spinAngle 90 , changes Direction From 'S' to 'W'
            grid.Topography[8, 8] = new Spinner()
            {
                ObsPosition = new int[] { 8, 8 },
                spinAngle = 90
            };
            rob.Move('R');
            Assert.AreEqual(rob.Direction, 'W');
            Assert.AreEqual(rob.LocX, 8);
            Assert.AreEqual(rob.LocY, 8);
            //move Right to spinner at (8,9) with spinAngle 90 , changes Direction From 'W' to 'N'
            grid.Topography[8, 9] = new Spinner()
            {
                ObsPosition = new int[] { 8, 9 },
                spinAngle = 90
            };
            rob.Move('R');
            Assert.AreEqual(rob.Direction, 'N');
            Assert.AreEqual(rob.LocX, 8);
            Assert.AreEqual(rob.LocY, 9);
            //move Left to spinner at (7,9) with spinAngle 270 , changes Direction From 'N' to 'W'
            grid.Topography[7, 9] = new Spinner()
            {
                ObsPosition = new int[] { 7, 9 },
                spinAngle = 270
            };
            rob.Move('L');
            Assert.AreEqual(rob.Direction, 'W');
            Assert.AreEqual(rob.LocX, 7);
            Assert.AreEqual(rob.LocY, 9);
        }
        [TestMethod]
        public void RobotDoNotMove_AnotherRobotPosition()
        {
            //robot1 at (9,8) 'N'
            //Robot1 can not move Right since Robot2 is present there
            rob1 = new ARobot { Rid = 1, Direction = 'N', LocX = 9, LocY = 8, Grid = grid };
            rob2 = new ARobot { Rid = 2, Direction = 'N', LocX = 9, LocY = 9, Grid = grid };
            grid.RobotsInGrid = new List<Robot>
            {
                rob1,
                rob2
            };

            rob1.Move('F');
            Assert.AreEqual(rob1.Direction, 'N');
            Assert.AreEqual(rob1.LocX, 9);
            Assert.AreEqual(rob1.LocY, 8);
        }
    }
}


