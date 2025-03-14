
namespace RootRemake_Project
{
    internal class LocationInfo
    {
        int[][] ar =
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
        decimal[][][] locationPolygon =
        {
            new []{ new decimal[] {2.2m, 2.2m }, new decimal[] { }, new decimal[] { }, new decimal[] { }, new decimal[] { } },
            new []{ new decimal[] { }, new decimal[] { }, new decimal[] { }, new decimal[] { }, new decimal[] { } },
            new []{ new decimal[] { }, new decimal[] { }, new decimal[] { }, new decimal[] { }, new decimal[] { } },
        };
    }
}
