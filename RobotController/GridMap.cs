using System;
using System.Collections.Generic;

namespace RobotController
{

    public class GridMap
    {
        public int MaxRows { get; set; }
        public int MaxCols { get; set; }
        //Topography array stores obstructions at given grid location
        public Obstructions[,] Topography { get; set; }
        //for multiple robots in map
        public List<Robot> RobotsInGrid { get; set; }

        public void Display()
        {
            //Print the Grid with Obstructions
            Console.WriteLine("Grid size: " + MaxRows + " X " + MaxCols);
            Console.WriteLine("U-Unknown; P-Path; R-Rock; H-Hole; S-Spinner;");
            for (int i = MaxRows - 1; i >= 0; i--)
            {
                for (int j = 0; j < MaxCols; j++)
                {
                    Console.Write("(" + j + "," + i + ")" + Topography[j, i].Type + " ");
                }
                Console.WriteLine();
            }
        }
        public void CreateGridTopographyManually()
        {
            Topography = new Obstructions[MaxRows, MaxCols];
            for (var y = 0; y < MaxCols; y++)
            {
                for (var x = 0; x < MaxRows; x++)
                {
                    Topography[x, y] = new Path()
                    {
                        ObsPosition = new int[] { x, y }
                    };
                }
            }
            var rocks = new List<int[]>()
            {
                new int[]{2,2},
                new int[]{4,4},
                new int[]{6,6},
            };
            foreach (var t in rocks)
            {
                Topography[t[0], t[1]] = new Rock
                {
                    ObsPosition = t
                };
            }

            var holes = new List<int[]>()
            {
                new int[]{1,1,1,3},
                new int[]{4,1,5,1},
                new int[]{0,4,5,5},
            };
            foreach (var t in holes)
            {
                Topography[t[0], t[1]] = new Hole
                {
                    ObsPosition = t,
                    endPosition = new int[] { t[2], t[3] }
                };
            }

            var spinners = new List<int[]>()
            {
                new int[]{4,5,90},
                new int[]{3,9,180},
                new int[]{4,8,270},
            };
            foreach (var t in spinners)
            {
                Topography[t[0], t[1]] = new Spinner
                {
                    ObsPosition = t,
                    spinAngle = t[2]
                };
            }

            var unknowns = new List<int[]>()
            {
                new int[]{3,3},
                new int[]{7,7},
            };
            foreach (var t in unknowns)
            {
                Topography[t[0], t[1]] = new Unknown
                {
                    ObsPosition = t
                };
            }
        }
    }
}
