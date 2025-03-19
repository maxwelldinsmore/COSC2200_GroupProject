using System.Drawing;

namespace RootRemake_Project.LocationClasses
{
    public class LocationInfo
    {
        public int[][] ar =
        {
            [ 1, 2, 4, 12, 13],
            [ 1, 2, 4, 12, 13],
            [0, 3, 12],
            [0, 3, 5, 12, 13, 14],
            [1, 2, 7, 12, 14],
            [0, 5, 8, 13, 15],
            [2, 4, 6 , 8, 10, 13, 14, 15, 16, 17],
            [5, 7, 11, 14, 17, 18],
            [3, 6, 11, 14, 18],
            [4, 5, 9, 15, 16],
            [8, 10, 16],
            [5, 9, 11, 16, 17],
            [7, 10, 17, 18],
            [0, 1, 2, 3, 13, 14],
            [0, 2, 4, 5, 12, 14, 15],
            [2, 3, 5, 6, 7, 12, 13, 17, 18],
            [4, 5, 8 , 13, 16],
            [5, 8, 9, 10, 15, 17],
            [5, 6, 10, 14, 16, 18],
            [6, 7, 11, 14, 17]

        };

        /// <summary>
        /// Polygons of the locations for the game board
        /// as percentage values (12% -> 0.12) going top left most point 
        /// then clockwise around for later highlighting / hovering
        /// </summary>

        public double[][][] locationSqaures =
        {
            new []{ // Location 0
                new []{ 4.58, 4.14 }, // [0][0]
                new []{ 19.63, 4.89 }, // [0][1]
                new []{ 19.63, 19.63 }, // [0][2]
                new []{ 4.58, 19.63 }   // [0][3]
            },
            new []{ // Location 1
                new []{ 46.21, 0.6 },
                new []{ 60.01, 0.6 },
                new []{ 60.01, 14.17 },
                new []{ 46.21, 14.17 }
            },

        };

        /// <summary>
        /// Sets the locations boundaries from percentage
        /// to real values for the game board
        /// </summary>
        public void onresize()
        {

        }

        public bool InsideLocation(Point mouse, double[][] locationPolygon)
        {
            // First gets inner polygon line used in point in polygon method
            double x1 = 0;
            double x2 = 0;
            double y1 = 0;
            double y2 = 99999999;
            foreach (double[] location in locationPolygon)
            {
                if (location[1] < y1)
                {
                    y1 = location[1];
                    x1 = location[0];
                }
                if (location[1] > y2)
                {
                    y2 = location[1];
                    x2 = location[0];
                }
            }

            // Ray Casting method 
            int intersectionCount = 0;
            // IDK IF THIS WORKS TOTALLY NOT AI
            for (int i = 0; i < locationPolygon.Length; i++)
            {
                double x1p = locationPolygon[i][0];
                double y1p = locationPolygon[i][1];
                double x2p = locationPolygon[(i + 1) % locationPolygon.Length][0];
                double y2p = locationPolygon[(i + 1) % locationPolygon.Length][1];
                if (y1p == y2p)
                {
                    continue;
                }
                if (mouse.Y < y1p && mouse.Y < y2p)
                {
                    continue;
                }
                if (mouse.Y >= y1p && mouse.Y >= y2p)
                {
                    continue;
                }
                double x = (mouse.Y - y1p) * (x2p - x1p) / (y2p - y1p) + x1p;
                if (x > mouse.X)
                {
                    intersectionCount++;
                }
            }
            // If even return false, if odd return true
            return intersectionCount % 2 == 1;

        }

    }

    
}


// TODO: Add to gamescreen for collision detection of polygon locations
// Ray Casting Method (Draw a line through the point and see if it intersects with the polygon)
// https://rosettacode.org/wiki/Ray-casting_algorithm
// Add in gamescreen referencing the Location Class / location Array


