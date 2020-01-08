
Instructions to Run the application:
- Clone this git repo and open it using visual studio.
- Build and Run 'RobotController' Project. 
- There are two projects:
1. RobotController:
   In main program(Program.cs), Grid of 10 rows and 10 columns is created and polpulated
   with list of obstructions to create the topography.
   Robot is created with id, starting coordinates and facing directions. 
   Robot is given set of commands/moves to navigate in the grid.
   The program outputs Robot location after each move.
   New Robot and obstructions can be added in the main program.
   Single or multiple robots can be added to the grid. In case of multiple robots, only one robot can move at a given time.
   
2. RobotController.UnitTests project:
   This project has unit tests for testing moves considering different obstructions scenarios.
   Also tests for multiple robots scenario.


Assumptions:
. Robot and grid coordinates are in the first quadrant.
. Location with Unknown object is set as obstruction with 'Unknown' type for simplicity.
. Robot does not change direction when it moves left/right.
. Spinner obstruction spinAngle/ Rotation always changes the Robot direction clockwise.
. With multiple robots on the grid, robots will move one by one ,
  and can not move to a location occupied by other robots.

Extra Credit:
. New types of Robots can be added using IRobot interface.
. New types of obstructions and their behaviours can be added by inheriting from obstructions base class.
. Added code for multiple robots on the same grid
