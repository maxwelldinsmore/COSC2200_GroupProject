using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project
{
    internal class MarquisDeCat : Character
    {
        
        // properties
        public int noCats; // total available cats
        public int noWoods; // available wood for buildings
        public int keepToken; // represents the keep token location
        private Dictionary<int, string> buildings; // key: location, value: buildingType
        private List<int> locationSquares; // area locations

        public MarquisDeCat()
        {
            noCats = 25; // total cat count
            noWoods = 8; // total wood count
            keepToken = -1; // not placed yet
            buildings = new Dictionary<int, string>();
            //locationSquares = new List<int>() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
        // -----------------------------------------------
        // SET UP PHASE -- at the start of the game
        // -----------------------------------------------
        public void setUp()
        {
            placeCats();
            tokens();
            Console.WriteLine("Set up complete.");
        }

        // places a cat at each area on the game board
        private void placeCats()
        {
            foreach (int i in locationSquares)
            {
                noCats--;
            }
        }

        // places the keep token to the selected area
        private void tokens()
        {
            keepToken = locationSquares[0];
            Console.WriteLine($"Keep token placed at location {keepToken}");
        }

        public void placeBuilding(int locationId, string buildingType)
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
        /// 
        /// </summary>
        public void birdsong()
        {
            int sawmills = 0;
            foreach (var building in buildings.Values)
            {
                if (building == "Sawmill")
                    sawmills++;
            }
            noWoods += sawmills;
        }
        // -----------------------------------------------
        // DAYLIGHT
        // -----------------------------------------------
        public void daylight()
        {
            int totalActions = 3;
            while (totalActions > 0)
            {
                string action = Console.ReadLine();

                switch (action.ToUpper())
                {
                    case "B":
                        build();
                        break;
                    case "R":
                        recruit();
                        break;
                    case "M":
                        move();
                        break;
                    case "MA":
                        march();
                        break;
                    case "O":
                        overwork();
                        break;
                    case "BT":
                        battle();
                        break;
                    case "E":
                        extraAction();
                        break;
                    default:
                        Console.WriteLine("Invalid action.");
                        continue;
                }
                totalActions--;
            }
        }
        // action methods
        // build : let player build a building of choice by spending their wood
        public void build()
        {
            if (noWoods > 0)
            {
                Console.WriteLine("Enter location ID to build: ");
                int loc = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter building type (Sawmill/Workshop/Recruiter: ");
                string type = Console.ReadLine();
                placeBuilding(loc, type);
                noWoods--;
            }
        } 
        // recruit: lets player to span a cat to a recruiter building
        public void recruit()
        {
            if (noCats > 0)
            {
                Console.WriteLine("Enter location ID to recruit a cat: ");
                int loc = int.Parse(Console.ReadLine());
                noCats--;
            } else
            {
                Console.WriteLine("No more cats to recruit!");
            }
        }

        // move: moves a cat by 1 area
        private void move()
        {
            
        }

        // march: moves a cat by 2 areas
        private void march()
        {

        }

        // battle: battles againts another character
        private void battle()
        {

        }

        // overwork: spends a card to plcae one wood at a sawmill in a matching clearing
        private void overwork()
        {

        }

        // extra action: allows user to have 1 more action by using their wil card
        private void extraAction()
        {

        }

        // -----------------------------------------------
        // EVENING 
        // -----------------------------------------------
        public void evening()
        {
            drawCard();
        }

        private void drawCard()
        {

        }
    }
}
