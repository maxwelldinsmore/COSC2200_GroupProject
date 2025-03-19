using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.LocationClasses
{
    // For the Clearing gets the location
    public class Clearing : Location
    {
        public Clearing(int locationID, double[,] locationCoordinates, List<int> connectedLocations)
            : base(locationID, "Clearing", locationCoordinates, connectedLocations)
        {
        }


        override public Boolean CanBuild()
        {
            return true; // TODO: add logic for if character rules the location and if there is space to build
        }
    }
}
