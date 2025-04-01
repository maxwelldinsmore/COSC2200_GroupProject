using System.Windows;

namespace RootRemake_Project.LocationClasses
{
    public static class LocationInfo
    {
        public static int[][] ar =
        {
                new int[] {0, 1, 2, 3, 13, 14},
                new int[] {0, 2, 4, 5, 12, 14, 15},
                new int[] {2, 3, 5, 6, 7, 12, 13, 17, 18},
                new int[] {4, 5, 8 , 13, 16},
                new int[] {5, 8, 9, 10, 15, 17},
                new int[] {5, 6, 10, 14, 16, 18},
                new int[] {6, 7, 11, 14, 17}
            };

        //TODO: Add in the correct values for the locations
        public static Location[] MapLocations =
        {
                // location 0
                new Clearing(
                    // location ID
                    0,
                    // location coordinates for clicking on the area
                    new double[][] { new double[] {37.6000, 37.6000}, new double[] {153.6000, 36.8000}, new double[] {156.8000, 162.4000}, new double[] {43.2000, 157.6000} },
                    // all location id's that are 1 move away from this location
                    new int[] { 1, 2, 4, 12, 13 },
                    // string of location highlight image i.e. Assets/Areas/0.png
                    "0",
                    // Point for warrior location
                    new Point(90, 112),
                    // Point for building location
                    new Point(0, 0)
                ),
                new Clearing(
                    1,
                    new double[][] { new double[] {402.4000, 1.6000}, new double[] {524.0000, 4.0000}, new double[] {511.2000, 108.8000}, new double[] {444.8000, 124.8000}, new double[] {398.4000, 95.2000} },
                    new int[] {0, 3, 12 },
                    "1",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    2,
                    new double[][] { new double[] {289.6000, 176.0000}, new double[] {362.4000, 145.6000}, new double[] {419.2000, 168.8000}, new double[] {416.8000, 271.2000}, new double[] {305.6000, 275.2000} },
                    new int[] {0, 3, 5, 12, 13, 14 },
                    "2",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    3,
                    new double[][] { new double[] {684.0000, 104.0000}, new double[] {797.6000, 98.4000}, new double[] {796.0000, 223.2000}, new double[] {687.2000, 220.8000}},
                    new int[] {1, 2, 7, 12, 14 },
                    "3",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    4,
                    new double[][] {
                        new double[] {16.0000, 248.8000},
                        new double[] {93.6000, 240.0000},
                        new double[] {141.6000, 302.4000},
                        new double[] {138.4000, 390.4000},
                        new double[] {28.0000, 392.0000} },
                    new int[] {0, 5, 8, 13, 15 },
                    "4",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    5,
                    new double[][] {
                        new double[] {196.8000, 396.0000},
                        new double[] {300.8000, 345.6000},
                        new double[] {320.8000, 495.2000},
                        new double[] {204.0000, 495.2000}
                    },
                    new int[] {2, 4, 6 , 8, 10, 13, 14, 15, 16, 17 },
                    "5",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    6,
                    new double[][] {
                        new double[] {504.0000, 318.4000},
                        new double[] {604.0000, 321.6000},
                        new double[] {598.4000, 432.8000},
                        new double[] {489.6000, 434.4000}
                    },
                    new int[] {5, 7, 11, 14, 17, 18},
                    "6",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    7,
                    new double[][] {
                        new double[] {692.80, 392.80},
                        new double[] {760.80, 344.00},
                        new double[] {823.20, 380.00},
                        new double[] {816.80, 462.40},
                        new double[] {718.40, 464.00}
                    },
                    new int[] {3, 6, 11, 14, 18 },
                    "7",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    8,
                    new double[][] {
                        new double[] {44.00, 601.60},
                        new double[] {104.00, 565.60},
                        new double[] {156.00, 604.80},
                        new double[] {155.20, 675.20},
                        new double[] {41.60, 686.40}
                    },
                    new int[] {4, 5, 9, 15, 16},
                    "8",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    9,
                    new double[][] {
                        new double[] {251.20, 737.60},
                        new double[] {240.00, 637.60},
                        new double[] {339.20, 622.40},
                        new double[] {371.20, 664.00},
                        new double[] {357.60, 739.20}
                    },
                    new int[] {8, 10, 16},
                    "9",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    10,
                    new double[][] {
                        new double[] {429.60, 645.60},
                        new double[] {428.80, 552.00},
                        new double[] {497.60, 539.20},
                        new double[] {543.20, 568.80},
                        new double[] {548.80, 659.20}
                    },
                    new int[] {5, 9, 11, 16, 17 },
                    "10",
                    new Point(0, 0),
                    new Point(0, 0)
                ),
                new Clearing(
                    11,
                    new double[][] {
                        new double[] {648.00, 657.60},
                        new double[] {711.20, 598.40},
                        new double[] {749.60, 601.60},
                        new double[] {764.00, 706.40},
                        new double[] {641.60, 717.60}
                    },
                    new int[] {7, 10, 17, 18 },
                    "11",
                    new Point(0, 0),
                    new Point(0, 0)
                )
                //new Forest(
                //    12,
                //    new double[][] {
                //        [],
                //        [],
                //        [],
                //        []
                //    },
                //    new int[] {0, 3, 12 }
                //    )
            };



    } 
}


// Ray Casting Method (Draw a line through the point and see if it intersects with the polygon)
// https://rosettacode.org/wiki/Ray-casting_algorithm
// Add in gamescreen referencing the Location Class / location Array


//    /// <summary>
///// Polygons of the locations for the game board
///// as percentage values (12% -> 0.12) going top left most point 
///// then clockwise around for later highlighting / hovering
///// </summary>

//public static double[][][] locationSqaures =
//{
//        new []{ // Location 0
//            new []{ 4.58, 4.14 }, // [0][0]
//            new []{ 19.63, 4.89 }, // [0][1]
//            new []{ 19.63, 19.63 }, // [0][2]
//            new []{ 4.58, 19.63 }   // [0][3]
//        },
//        new []{ // Location 1
//            new []{ 46.21, 0.6 },
//            new []{ 60.01, 0.6 },
//            new []{ 60.01, 14.17 },
//            new []{ 46.21, 14.17 }
//        },

//    };
//}