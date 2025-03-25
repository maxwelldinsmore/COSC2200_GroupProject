using RootRemake_Project.ObjectClasses;
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
        public Forest(int locationID, double[][] locationCoordinates, int[] connectedLocations)
        {
            this.LocationID = locationID;
            this.LocationType = "Forest";
            this.LocationPolygonPercents = locationCoordinates;
            this.ConnectedLocations = connectedLocations;
            this.Armies = new List<Army>();
            this.Buildings = new List<Building>();
            LocationPolygon = new double[locationCoordinates.Length][];

            for (int i = 0; i < locationCoordinates.Length; i++)
            {
                LocationPolygon[i] = new double[locationCoordinates[i].Length];
            }
        }
        override public bool CanBuild()
        {
            // Forests can't build
            return false;
        }
    }
}
