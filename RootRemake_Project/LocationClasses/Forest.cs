using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.LocationClasses
{
    // For the forests gets the location 
    public class Forest : Location
    {
        public Forest(int locationID, decimal[,] locationCoordinates, List<int> connectedLocations)
            : base(locationID, "Forest", locationCoordinates, connectedLocations)
        {
        }
    }
}
