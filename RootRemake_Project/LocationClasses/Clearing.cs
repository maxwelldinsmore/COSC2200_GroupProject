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
        public Clearing(int locationID, decimal[,] locationCoordinates, List<int> connectedLocations)
            : base(locationID, "Clearing", locationCoordinates, connectedLocations)
        {
        }
    }
}
