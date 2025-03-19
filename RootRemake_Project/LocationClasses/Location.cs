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
    internal abstract class Location 
    {
        public int LocationID { get; set; } // An set and get for the location

        public string LocationType { get; set; } // If its a forest or clearing (for clear skin)

        public decimal[,] LocationPolygon { get; set; } // The 2d array of decimals for [x, y] points describing location

        public List<int> ConnectedLocations { get; set; } // For listing the locations that connect

        public Location(int locationID, string locationType, decimal[,] locationCoordinates)
        { 
            LocationID = locationID;
            LocationType = locationType;
            LocationPolygon = locationCoordinates;
            ConnectedLocations = new List<int>();

        }

        // For the forests gets the location 
        public class Forest : Location
        {
            public Forest(int locationID, decimal[,] locationCoordinates)
                : base(locationID, "Forest", locationCoordinates)
            {
            }
        }

        // For the Clearing gets the location
        public class Clearing : Location
        {
            public Clearing(int locationID, decimal[,] locationCoordinates)
                : base(locationID, "Clearing", locationCoordinates)
            {
            }
        }

    }
}
