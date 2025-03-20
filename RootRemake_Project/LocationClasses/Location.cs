using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

/// <summary>
//location class that is inherited by Forest and Clearing
//Where location class
//has 2d array of decimals for [x, y] points describing location
//has array showing all connected points (see locationinfo.cs)
//location ID
//location type that is either forest or clearing
//armiesOccupying which is a 2d array returning [Character character, int amountOfUnits)
//CanBuild abstract function
//buildableLocation
//Forest has no more other features other than returing location type and false for canBuild and 0 buildable locations
//Clearing has a function called isRuled which counts amount of units of each character in region and returns character that has highest or false.
//setter and getters for adding units in location or adding building to location.
///</summary>

namespace RootRemake_Project.LocationClasses
{
    public abstract class Location
    {
        public int LocationID { get; set; } // An set and get for the location

        public string LocationType { get; set; } // If its a forest or clearing 

        /// <summary>
        /// The 2d array of doubles for [x, y] points describing location in percentage of map/image
        /// </summary>
        public double[,] LocationPolygonPercents { get; set; }

        /// <summary>
        /// The 2d array of doubles for [x, y] points describing location,
        /// updated when map is loaded and on every resize
        /// </summary>
        public double[,] LocationPolygon { get; set; } 

        public List<int> ConnectedLocations { get; set; } // For listing the locations that connect

        /// <summary>
        /// List of factions that contain units in this location
        /// </summary>
        public List<Army> Armies { get; set; }

        public int totalBuildings { get; set; } // The total amount of buildings in the location

        public List<Building> Buildings { get; set; } // The list of buildings in the location

        public Location(int locationID, string locationType, double[,] locationCoordinates, List<int> connectedLocations)
        {
            LocationID = locationID;
            LocationType = locationType;
            LocationPolygonPercents = locationCoordinates;
            ConnectedLocations = connectedLocations;
            Armies = new List<Army>();
            Buildings = new List<Building>();
            LocationPolygon = new double[locationCoordinates.GetLength(0), locationCoordinates.GetLength(1)];
        }

        public abstract bool CanBuild(); // Abstract function for building




    }
}
