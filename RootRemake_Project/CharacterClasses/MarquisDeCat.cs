using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RootRemake_Project.CharacterClasses
{
    public class MarquisDeCat : Player
    {


        // properties
        public int TotalCats; // total available cats
        public int AvailableWood; // available wood for buildings
        public int AvailableWorkshops = 6; // available workshops
        public int[] WorkShopVP = { 0, 2, 2, 3, 3, 4 }; // workshop victory points     

        public int AvailableSawmills = 6; // available sawmills
        public int[] SawmillVP = { 0, 1, 2, 3, 4, 5 }; // sawmill victory points

        public int AvailableRecruiters = 6; // available recruiters
        public int[] RecruiterVP = { 0, 1, 2, 3, 3, 4 }; // recruiter victory points

        public int[] buildingCosts = { 0, 1, 2, 3, 3, 4 }; // building costs

        public Uri RecruiterArt { get; set; }

        public Uri WorkshopArt { get; set; }

        public Uri SawmillArt { get; set; } 

        public MarquisDeCat(string PlayerName)
        {
            UserName = PlayerName;
            Hand = new List<Card>();
            VictoryPoints = 0;

            TotalCats = 25; // total cat count
            AvailableWood = 1; // starting wood count
            //locationSquares = new List<int>() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            SawmillArt = new Uri("pack://application:,,,/Assets/Marquis/Sawmill.png", UriKind.RelativeOrAbsolute);
            WorkshopArt = new Uri("pack://application:,,,/Assets/Marquis/Workshop.png", UriKind.RelativeOrAbsolute);
            RecruiterArt = new Uri("pack://application:,,,/Assets/Marquis/Recruiter.png", UriKind.RelativeOrAbsolute);
        }
        // -----------------------------------------------
        // SET UP PHASE -- at the start of the game
        // -----------------------------------------------
        override public void CharacterSetup()
        {
            // Requests user to place keep token
            // Place keep token in a corner clearing
            // then auto spawn cats and auto place buildings around

            PlaceCats();
            Tokens();
        }

        // places a cat at each area on the game board
        private void PlaceCats()
        {
        }

        // places the keep token to the selected area
        private void Tokens()
        {
            
        }

        public void PlaceBuilding(int locationId, string buildingType)
        {
            //if (!buildings.ContainsKey(locationId) && (buildingType == "Sawmill" || buildingType == "Workshop" || buildingType == "Recruiter"))
            //{
            //    buildings[locationId] = buildingType;
            //    Console.WriteLine($"Places {buildingType} at location {locationId}");
            //} else
            //{
            //    Console.WriteLine("Invalid building placement.");
            //}

            
        }
        // -----------------------------------------------
        // BIRDSONG
        // -----------------------------------------------
        /// <summary>
        /// Generates a wood for each sawmill at start of turn
        /// </summary>
        override public void BirdSong()
        {
            AvailableWood += 6 - AvailableSawmills;
            // EVENT TO NOTIFY MAIN SCREEN WOOD ADDED
            
        }
        // -----------------------------------------------
        // DAYLIGHT
        // -----------------------------------------------
        override public void Daylight()
        {
            int totalActions = 3;
            // Event to update Marquis daylight user control

        }
        // action methods
        // build : let player build a building of choice by spending their wood
        public void Build()
        {

        } 
        // recruit: lets player to span a cat to a recruiter building
        public void Recruit()
        {
            // done on main screen, this just minuses the cat count
        }

        // move: moves a cat by 1 area
        override public void Move()
        {
            
        }

        // march: moves a cat by 2 areas
        public void March()
        {

        }

        // battle: battles againts another Character
        private void Battle()
        {

        }

        // overwork: spends a card to plcae one wood at a sawmill in a matching clearing
        private void Overwork()
        {

        }

        // extra action: allows user to have 1 more action by using their wil card
        private void ExtraAction()
        {

        }

        // -----------------------------------------------
        // EVENING 
        // -----------------------------------------------
        override public void Evening()
        {
            DrawCard();
        }

        // TODO: complete this
        private int DrawCard()
        {
            return 0;
        }

        override public string CharacterName()
        {
            return "Marquis";
        }

    }
}
