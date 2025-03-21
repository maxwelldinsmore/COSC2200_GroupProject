﻿using System.Drawing;

namespace RootRemake_Project.LocationClasses
{
    public static class LocationInfo
    {
        public static int[][] ar =
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

        //TODO: Add in the correct values for the locations
        public static Location[] MapLocations =
        {
            // THESE ARE WRONG VALUES JUST THE RIGHT IDEA
            new Clearing(
                0, 
                new double[][] { [ 4.58, 4.14 ], [19.63, 4.89 ], [19.63, 19.63 ], [4.58, 19.63 ] },
                new int[] { 1, 2, 4, 12, 13 } 
                ),
            new Clearing(
                1,
                new double[][] { [ 46.1, 0 ], [60.01, 0 ], [60.01, 15 ], [46.1, 15] },
                new int[] {0, 3, 12 }
                )

        };

        /// <summary>
        /// Polygons of the locations for the game board
        /// as percentage values (12% -> 0.12) going top left most point 
        /// then clockwise around for later highlighting / hovering
        /// </summary>

        public static double[][][] locationSqaures =
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


        

        

    }

    
}


// TODO: Add to gamescreen for collision detection of polygon locations
// Ray Casting Method (Draw a line through the point and see if it intersects with the polygon)
// https://rosettacode.org/wiki/Ray-casting_algorithm
// Add in gamescreen referencing the Location Class / location Array


