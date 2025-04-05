using System.Windows;

namespace RootRemake_Project.LocationClasses
{
    public static class LocationInfo
    {


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
             
                    // Point for warrior location
                    new Point(164, 90),
                    // Point for building location
                    new Point(90, 112)
                ),
                new Clearing(
                    1,
                    new double[][] { new double[] {402.4000, 1.6000}, new double[] {524.0000, 4.0000}, new double[] {511.2000, 108.8000}, new double[] {444.8000, 124.8000}, new double[] {398.4000, 95.2000} },
                    new int[] {0, 3, 12 },
                    
                    // Point for warrior location
                    new Point(0, 0),
                    // Point for building location
                    new Point(412, 72),
                    new Point(453, 63)

                ),
                new Clearing(
                    2,
                    new double[][] { new double[] {289.6000, 176.0000}, new double[] {362.4000, 145.6000}, new double[] {419.2000, 168.8000}, new double[] {416.8000, 271.2000}, new double[] {305.6000, 275.2000} },
                    new int[] {0, 3, 5, 12, 13, 14 },
                    // Point for warrior location
                    new Point(0, 0),
                    // Point for building location
                    new Point(337, 225),
                    new Point(368, 184)
                ),
                new Clearing(
                    3,
                    new double[][] { new double[] {684.0000, 104.0000}, new double[] {797.6000, 98.4000}, new double[] {796.0000, 223.2000}, new double[] {687.2000, 220.8000}},
                    new int[] {1, 2, 7, 12, 14 },
                   // Point for warrior location
                    new Point(0, 0),
                    // Point for building location
                    new Point(701, 184),
                    new Point(765, 152)
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
                    // Point for warrior location
                    new Point(0, 0),
                    // Point for building location
                    new Point(44, 252),
                    new Point(66, 308)
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
                    // Point for warrior location
                    new Point(0, 0),
                    // Point for building location
                    new Point(213, 393),
                    new Point(270, 364)

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
                    // Point for warrior location
                    new Point(0, 0),
                    // Point for building location
                    new Point(511, 377),
                    new Point(576, 331)
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
                   // Point for warrior location
                    new Point(0, 0),
                    // Point for building location
                    new Point(784, 407)

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
                    // Point for warrior location
                    new Point(0, 0),
                    // Point for building location
                    new Point(72, 605)

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
                  
                    // Point for building location
                    new Point(247, 674),
                    new Point(319, 640)
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
                    // Point for building location
                    new Point(436, 558),
                    new Point(501, 600)
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
                    // Point for warrior location
                    new Point(0, 0),
                    // Point for building location
                    new Point(716, 611)
                 ),
                
                new Forest(
                    12,
                    new double[][] {
                        new double[] {178.40, 92.80},
                        new double[] {379.20, 88.80},
                        new double[] {434.40, 132.00},
                        new double[] {528.00, 114.40},
                        new double[] { 655.20, 167.20 },
                        new double[] { 445.60, 204.80 },
                        new double[] { 428.00, 161.60 },
                        new double[] { 362.40, 141.60},
                        new double[] { 288.00, 167.20},
                        new double[] { 164.80, 116.80 }
                    },
                    new int[] {0, 1, 2, 3, 13, 14},
                    new Point(0,0)
                 ),

                new Forest(
                    13,
                    new double[][] {
                        new double[] {108.00, 170.40},
                        new double[] {162.40, 174.40},
                        new double[] {280.80, 205.60},
                        new double[] {307.20, 316.80},
                        new double[] {199.20, 381.60},
                        new double[] {146.40, 356.80},
                        new double[] {152.80, 296.00},
                        new double[] {99.20, 232.80},
                    },
                    new int[] {0, 2, 4, 5, 12, 14, 15},
                    new Point(0,0)
                             ),

                new Forest(
                    14,
                    new double[][] {
                        new double[] {310.40, 368.00},
                        new double[] {364.00, 282.40},
                        new double[] {423.20, 279.20},
                        new double[] {428.80, 231.20},
                        new double[] { 668.80, 214.40 },
                        new double[] { 736.00, 234.40 },
                        new double[] {734.40, 349.60 },
                        new double[] { 605.60, 373.60 },
                        new double[] { 608.80, 316.80 },
                        new double[] { 499.20, 311.20 },
                        new double[] { 467.20, 377.60 },
                        new double[] {  320.00, 412.80},



                    },
                    new int[] {2, 3, 5, 6, 7, 12, 13, 17, 18},
                    new Point(0,0)
                ),

                new Forest(
                    15,
                    new double[][] {
                        new double[] {87.20, 397.60},
                        new double[] {152.80, 392.00},
                        new double[] {196.00, 490.40},
                        new double[] {130.40, 576.80},
                        new double[] {101.60, 556.00}
                    },
                    new int[] {4, 5, 8 , 13, 16},
                    new Point(0,0)
                ),

                new Forest(
                    16,
                    new double[][] {
                        new double[] {160.00, 576.80},
                        new double[] {229.60, 500.00},
                        new double[] {331.20, 504.00},
                        new double[] {423.20, 572.80},
                        new double[] {421.60, 622.40 },
                        new double[] { 348.80, 614.40 },
                        new double[] { 234.40, 628.80},
                        new double[] { 165.60, 651.20},
                    },
                    new int[] {5, 8, 9, 10, 15, 17},
                    new Point(0,0)
                ),

                new Forest(
                    17,
                    new double[][] {
                        new double[] {325.60, 441.60},
                        new double[] {473.60, 400.80},
                        new double[] {480.00, 434.40},
                        new double[] {548.80, 442.40},
                        new double[] { 648.80, 625.60 },
                        new double[] { 548.00, 564.80},
                        new double[] { 500.00, 535.20 },
                        new double[] { 426.40, 537.60 },
                    },
                    new int[] {5, 6, 10, 14, 16, 18},
                    new Point(0,0)
                ),

                new Forest(
                    18,
                    new double[][] {
                        new double[] {594.40, 456.80},
                        new double[] {608.80, 399.20},
                        new double[] {688.00, 416.80},
                        new double[] {725.60, 496.80},
                        new double[] { 681.60, 612.80 }
                    },
                    new int[] {6, 7, 11, 14, 17},
                    new Point(0,0)
                )


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