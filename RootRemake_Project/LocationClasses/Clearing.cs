using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.LocationClasses
{
    // For the Clearing gets the location
    public class Clearing : Location
    {
        public Point Building1LocationPercents { get; set; }
        public Point? Building2LocationPercents { get; set; }
        public Point Building1Location { get; set; }
        public Point? Building2Location { get; set; }


        public Clearing(int locationID, double[][] locationCoordinates, int[] connectedLocations, Point buildLocation, Point buildLocation2)
        {
            this.LocationID = locationID;
            this.LocationType = "Forest";
            this.LocationPolygon = locationCoordinates;
            this.ConnectedLocations = connectedLocations;
            this.Armies = new List<Army>();
            this.Buildings = new List<Building>();
            this.totalBuildings = 2;

            this.Building1LocationPercents = buildLocation;
            this.Building2LocationPercents = buildLocation2;
        }

        public Clearing(int locationID, double[][] locationCoordinates, int[] connectedLocations, Point buildLocation)
        {
            this.LocationID = locationID;
            this.LocationType = "Forest";
            this.LocationPolygon = locationCoordinates;
            this.ConnectedLocations = connectedLocations;
            this.Armies = new List<Army>();
            this.Buildings = new List<Building>();
            this.totalBuildings = 1;
            this.Building1LocationPercents = buildLocation;
            this.Building2LocationPercents = null;
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
