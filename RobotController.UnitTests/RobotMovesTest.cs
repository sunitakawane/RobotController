using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotController.Robots;

namespace RobotController.UnitTests
{
    [TestClass]
    public class RobotMovesTest
    {
        GridMap grid;
        SimpleRobot rob, rob1, rob2;
        public RobotMovesTest()
        {
            grid = new GridMap() { MaxRows = 20, MaxCols = 20 };
            Assert.IsNotNull(grid);
            rob = new SimpleRobot(1, 9, 8, 'N', grid);
            Assert.IsNotNull(rob);
            grid.RobotsInGrid = new List<SimpleRobot> { rob };
            grid.CreateGridTopographyManually();
        }
        [TestMethod]
        public void RobotDoNotMove_InvalidInstruction()
        {
            //robot at (9,8) 'N'
            rob = new SimpleRobot(1, 9, 8, 'N', grid);
            rob.Move('s');
            var newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 9);
            Assert.AreEqual(newPoint[1], 8);
            Assert.AreEqual(rob.GetDirection(), 'N');
        }
        [TestMethod]
        public void RobotDoNotMove_RockOrUnkownOrBorder()
        {
            //Test rock in Front
            //robot at (9,8) 'N'
            rob = new SimpleRobot(1, 9, 8, 'N', grid);
            //rock at (9,9) , Do not move Front
            grid.Topography[9, 9] = new Rock()
            {
                ObsPosition = new int[] { 9, 9 }
            };
            rob.Move('F');
            var newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 9);
            Assert.AreEqual(newPoint[1], 8);
            Assert.AreEqual(rob.GetDirection(), 'N');

            //Test Unknown on Left
            //robot at (9,8) 'N'
            rob = new SimpleRobot(1, 9, 8, 'N', grid);
            //Unknown at (8,8) , Do not move to Left
            grid.Topography[8, 8] = new Unknown()
            {
                ObsPosition = new int[] { 8, 8 }
            };
            rob.Move('L');
            newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 9);
            Assert.AreEqual(newPoint[1], 8);
            Assert.AreEqual(rob.GetDirection(), 'N');


            //Test On the border
            //robot at (19,18) 'N'
            //Out of bouds on Move to Right : Do Not Move
            rob = new SimpleRobot(1, 19, 18, 'N', grid);
            rob.Move('R');
            newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 19);
            Assert.AreEqual(newPoint[1], 18);
            Assert.AreEqual(rob.GetDirection(), 'N');

            //robot at (19,19) 'N'
            //Move Front / Left / Right Do not move
            rob = new SimpleRobot(1, 19, 19, 'N', grid);
            rob.Move('F');
            newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 19);
            Assert.AreEqual(newPoint[1], 19);
            Assert.AreEqual(rob.GetDirection(), 'N');
        }

        [TestMethod]
        public void RobotMoveTo_Path()
        {
            //Test Path in Front
            //robot at (9,8) 'N'
            rob = new SimpleRobot(1, 9, 8, 'N', grid);
            //Path at (9,9) , Move Front to (9,9)
            grid.Topography[9, 9] = new Path()
            {
                ObsPosition = new int[] { 9, 9 }
            };
            rob.Move('F');
            var newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 9);
            Assert.AreEqual(newPoint[1], 9);
            Assert.AreEqual(rob.GetDirection(), 'N');
        }

        [TestMethod]
        public void RobotMoveTo_Hole()
        {
            //robot at (9,8) 'N'
            rob = new SimpleRobot(1, 9, 8, 'N', grid);
            //hole at (9,9) connected to (7,7)
            grid.Topography[9, 9] = new Hole()
            {
                ObsPosition = new int[] { 9, 9 },
                endPosition = new int[] { 7, 7 }
            };
            rob.Move('F');
            var newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 7);
            Assert.AreEqual(newPoint[1], 7);
            Assert.AreEqual(rob.GetDirection(), 'N');
        }
        [TestMethod]
        public void RobotMoveTo_Spinner()
        {
            //robot at (9,8) 'N'
            rob = new SimpleRobot(1, 9, 8, 'N', grid);
            //move to spinner at (9,9) with spinAngle 90 , changes Direction to 'E'
            grid.Topography[9, 9] = new Spinner()
            {
                ObsPosition = new int[] { 9, 9 },
                spinAngle = 90
            };
            rob.Move('F');
            var newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 9);
            Assert.AreEqual(newPoint[1], 9);
            Assert.AreEqual(rob.GetDirection(), 'E');
            //move Right to spinner at (9,8) with spinAngle 90 , changes Direction From 'E' to 'S'
            grid.Topography[9, 8] = new Spinner()
            {
                ObsPosition = new int[] { 9, 8 },
                spinAngle = 90
            };
            rob.Move('R');
            newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 9);
            Assert.AreEqual(newPoint[1], 8);
            Assert.AreEqual(rob.GetDirection(), 'S');
            //move Right to spinner at (8,8) with spinAngle 90 , changes Direction From 'S' to 'W'
            grid.Topography[8, 8] = new Spinner()
            {
                ObsPosition = new int[] { 8, 8 },
                spinAngle = 90
            };
            rob.Move('R');
            newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 8);
            Assert.AreEqual(newPoint[1], 8);
            Assert.AreEqual(rob.GetDirection(), 'W');
            //move Right to spinner at (8,9) with spinAngle 90 , changes Direction From 'W' to 'N'
            grid.Topography[8, 9] = new Spinner()
            {
                ObsPosition = new int[] { 8, 9 },
                spinAngle = 90
            };
            rob.Move('R');
            newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 8);
            Assert.AreEqual(newPoint[1], 9);
            Assert.AreEqual(rob.GetDirection(), 'N');
            //move Left to spinner at (7,9) with spinAngle 270 , changes Direction From 'N' to 'W'
            grid.Topography[7, 9] = new Spinner()
            {
                ObsPosition = new int[] { 7, 9 },
                spinAngle = 270
            };
            rob.Move('L');
            newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 7);
            Assert.AreEqual(newPoint[1], 9);
            Assert.AreEqual(rob.GetDirection(), 'W');
        }
        [TestMethod]
        public void RobotDoNotMove_AnotherRobotPosition()
        {
            //robot1 at (9,8) 'N'
            //Robot1 can not move Right since Robot2 is present there
            rob1 = new SimpleRobot(1, 9, 8, 'N', grid);
            rob2 = new SimpleRobot(2, 9, 9, 'N', grid);
            grid.RobotsInGrid = new List<SimpleRobot>
            {
                rob1,
                rob2
            };

            rob1.Move('F');
            var newPoint = rob.GetLocation();
            Assert.AreEqual(newPoint[0], 9);
            Assert.AreEqual(newPoint[1], 8);
            Assert.AreEqual(rob.GetDirection(), 'N');
        }
    }
}


