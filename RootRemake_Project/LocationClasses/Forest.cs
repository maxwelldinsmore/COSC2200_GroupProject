using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RootRemake_Project.LocationClasses
{
    // For the forests gets the location 
    public class Forest : Location
    {
        public Forest(int locationID, double[][] locationCoordinates, int[] connectedLocations, Point warriorLocations)
        {
            this.LocationID = locationID;
            this.LocationType = "Forest";
            this.LocationPolygon = locationCoordinates;
            this.ConnectedLocations = connectedLocations;
            this.Armies = new List<Army>();
            this.Buildings = new List<Building>();
            this.ContainsRuin = false;
            this.TotalBuildings = 0;
            this.Building1Location = null;
            this.Building2Location = null;
            this.WarriorLocation = warriorLocations;
            LocationFaction = -1; // No faction
        }
        override public bool CanBuild()
        {
            // Forests can't build
            return false;
        }
    }
}
