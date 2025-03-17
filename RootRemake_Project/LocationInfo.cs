
namespace RootRemake_Project
{
    public class LocationInfo
    {
        public int[][] ar =
        {
            new []{ 1, 2, 4, 12, 13},
            new []{ 0, 3, 12},
            new []{ 0, 3, 5, 12, 13, 14},
            new []{ 1, 2, 7, 12, 14},
            new []{ 0, 5, 8, 13, 15},
            new []{ 2, 4, 6 , 8, 10, 13, 14, 15, 16, 17},
            new []{ 5, 7, 11, 14, 17, 18},
            new []{ 3, 6, 11, 14, 18},
            new []{ 4, 5, 9, 15, 16},
            new []{ 8, 10, 16},
            new []{ 5, 9, 11, 16, 17},
            new []{ 7, 10, 17, 18},
            new []{ 0, 1, 2, 3, 13, 14},
            new []{ 0, 2, 4, 5, 12, 14, 15},
            new []{ 2, 3, 5, 6, 7, 12, 13, 17, 18},
            new []{ 4, 5, 8 , 13, 16},
            new []{ 5, 8, 9, 10, 15, 17},
            new []{ 5, 6, 10, 14, 16, 18},
            new []{ 6, 7, 11, 14, 17}

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

        
    }
}
