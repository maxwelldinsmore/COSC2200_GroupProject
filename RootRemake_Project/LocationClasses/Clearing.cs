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




        public Clearing(int locationID, int locationFaction, double[][] locationCoordinates, int[] connectedLocations, Point warriorLocations, Point buildLocation, Point buildLocation2)
        {
            this.LocationID = locationID;
            this.LocationFaction = locationFaction;
            this.LocationType = "Clearing";
            this.LocationPolygon = locationCoordinates;
            this.ConnectedLocations = connectedLocations;
            this.Armies = new List<Army>();
            this.Buildings = new List<Building>();
            this.TotalBuildings = 2;
            this.WarriorLocation = warriorLocations;

            this.Building1Location = buildLocation;
            this.Building2Location = buildLocation2;
        }

        public Clearing(int locationID, int locationFaction, double[][] locationCoordinates, int[] connectedLocations, Point warriorLocations, Point buildLocation)
        {
            this.LocationFaction = locationFaction;
            this.LocationID = locationID;
            this.LocationType = "Clearing";
            this.LocationPolygon = locationCoordinates;
            this.ConnectedLocations = connectedLocations;
            this.Armies = new List<Army>();
            this.Buildings = new List<Building>();
            this.TotalBuildings = 1;
            this.Building1Location = buildLocation;
            this.WarriorLocation = warriorLocations;
            this.Building2Location = null;
        }
        override public bool CanBuild()
        {
            if (TotalBuildings == 1 && Buildings.Count == 1)
            {
                return false;
            }
            else if (TotalBuildings == 2 && Buildings.Count == 2)
            {
                return false;
            }
            else
            {
                return true;
            }

        }



        
    }
}
