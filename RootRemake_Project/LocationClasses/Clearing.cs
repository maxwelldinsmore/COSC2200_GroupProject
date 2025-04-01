using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace RootRemake_Project.LocationClasses
{
    // For the Clearing gets the location
    public class Clearing : Location
    {




        public Clearing(int locationID, double[][] locationCoordinates, int[] connectedLocations, string locationHighlight, Point warriorLocations, Point buildLocation, Point buildLocation2)
        {
            this.LocationID = locationID;
            this.LocationType = "Forest";
            this.LocationPolygon = locationCoordinates;
            this.ConnectedLocations = connectedLocations;
            this.Armies = new List<Army>();
            this.Buildings = new List<Building>();
            this.totalBuildings = 2;
            LocationHighlight = locationHighlight;
            this.WarriorLocation = warriorLocations;

            this.Building1Location = buildLocation;
            this.Building2Location = buildLocation2;
        }

        public Clearing(int locationID, double[][] locationCoordinates, int[] connectedLocations, string locationHighlight, Point warriorLocations, Point buildLocation)
        {
            this.LocationID = locationID;
            this.LocationType = "Forest";
            this.LocationPolygon = locationCoordinates;
            this.ConnectedLocations = connectedLocations;
            this.Armies = new List<Army>();
            this.Buildings = new List<Building>();
            this.totalBuildings = 1;
            this.Building1Location = buildLocation;
            this.WarriorLocation = warriorLocations;
            this.Building2Location = null;
            LocationHighlight = locationHighlight;
        }
        override public bool CanBuild()
        {
            // Forests can't build
            return false;
        }



        public bool RulesClearing(int playerID)
        {
            // TODO: add logic
            return false;
        }
    }
}
