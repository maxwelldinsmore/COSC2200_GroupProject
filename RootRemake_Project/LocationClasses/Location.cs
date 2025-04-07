using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows;
/// <summary>
//location class that is inherited by Forest and Clearing
//Where location class
//has 2d array of decimals for [x, y] points describing location
//has array showing all connected points (see locationinfo.cs)
//location ID
//location type that is either forest or clearing
//armiesOccupying which is a 2d array returning [Character Character, int amountOfUnits)
//CanBuild abstract function
//buildableLocation
//Forest has no more other features other than returing location type and false for canBuild and 0 buildable locations
//Clearing has a function called isRuled which counts amount of units of each Character in region and returns Character that has highest or false.
//setter and getters for adding units in location or adding building to location.
///</summary>

namespace RootRemake_Project.LocationClasses
{
    public abstract class Location
    {
        public int LocationID { get; set; } // An set and get for the location

        public string LocationType { get; set; } // If its a forest or clearing 

        /// <summary>
        /// The 2d array of doubles for [x, y] points describing location,
        /// </summary>
        public double[][] LocationPolygon { get; set; }

        public int[] ConnectedLocations { get; set; } // For listing the locations that connect

        /// <summary>
        /// List of factions that contain units in this location
        /// </summary>
        public List<Army> Armies { get; set; }

        public int TotalBuildings { get; set; } // The total amount of buildings in the location

        public List<Building> Buildings { get; set; } // The list of buildings in the location

        public bool ContainsRuin { get; set; } // If the location contains a ruin

        public string LocationHighlight { get; set; } // The location of the highlight image for the location

        public Point WarriorLocation { get; set; } // The location of the warriors

        public Point? Building1Location { get; set; }
        public Point? Building2Location { get; set; }
        public Location(int locationID, string locationType, double[][] locationCoordinates, int[] connectedLocations, string locationHighlight)
        {
            LocationID = locationID;
            LocationType = locationType;
            LocationPolygon = locationCoordinates;
            ConnectedLocations = connectedLocations;
            Armies = new List<Army>();
            Buildings = new List<Building>();
            LocationHighlight = locationHighlight;
        }
        public Location()
        {
            LocationType = string.Empty;
            LocationPolygon = Array.Empty<double[]>();
            ConnectedLocations = Array.Empty<int>();
            Armies = new List<Army>();
            Buildings = new List<Building>();
            LocationHighlight = string.Empty;
        }

        public void AddArmy(Army army)
        {
            Armies.Add(army);
        }

        public void RemoveArmy(Army army)
        {
            Armies.Remove(army);
        }

        public bool ContainsArmy(int PlayerID)
        {
            return Armies.Any(a => a.PlayerID == PlayerID);
        }


        public abstract bool CanBuild(); // Abstract function for building
    }
}
