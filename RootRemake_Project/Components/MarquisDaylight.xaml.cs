using RootRemake_Project.CharacterClasses;
using RootRemake_Project.ObjectClasses;
using RootRemake_Project.LocationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace RootRemake_Project.Components
{
    /// <summary>
    /// Interaction logic for Marquis.xaml
    /// </summary>
    public partial class MarquisDaylight : UserControl
    {
        MarquisDeCat marquis { get; set; }
        public event EventHandler SidePanelLoaded;
        private int lastLocationClicked;
        private int PlayerID;
        private string lastActionClicked;

        public MarquisDaylight()
        {
            InitializeComponent();
            this.Loaded += UserControl_Loaded;
            this.Unloaded += UserControl_Unloaded;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.LocationClicked -= ParentWindow_LocationClicked;

                // returns the marquis de cat object to the parent window
                parentWindow.Players[parentWindow.CurrentPlayerTurn] = marquis;

                PlayerID = parentWindow.CurrentPlayerTurn;
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SidePanelLoaded?.Invoke(this, EventArgs.Empty);

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {


                parentWindow.LocationClicked += ParentWindow_LocationClicked;

                // Gets a copy of the marquis de cat object for the current player
                // then returns the object once end actions are run out
                marquis = (MarquisDeCat)parentWindow.Players[parentWindow.CurrentPlayerTurn];
                marquis.Daylight();
                marquis.DaylightActions += 1;
                UpdateActionsInfo();
            }
            
        }
        private void ParentWindow_LocationClicked(object sender, int locationId)
        {
            lastLocationClicked = locationId;

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            { 
                switch(lastActionClicked)
                {
                    case "March":
                        // march action
                        break;
                    case "Attack":
                        // attack action
                        break;
                    case "BuildWorkshop":
                        parentWindow.AddBuildingToLocation(locationId, PlayerID, "Workshop");
                        break;
                    case "BuildSawmill":
                        parentWindow.AddBuildingToLocation(locationId, PlayerID, "Sawmill");
                        break;
                    case "BuildRecruiter":
                        parentWindow.AddBuildingToLocation(locationId, PlayerID, "Recruiter");
                        break;
                    default:
                        break;

                }
                parentWindow.HighlightLocations(new List<int>());


            }

        }
        /// <summary>
        /// Updates actions and info on the side panel according to the current player
        /// if actions remaining = 0 then end turn button is enabled
        /// Disabling all action buttons once the action counter has finished but keeping the gain action button enabled to gain another action
        /// </summary>
        private void UpdateActionsInfo()
        {
            marquis.DaylightActions -= 1;
            actionsRemainingLabel.Content = "Actions Remaining: " + marquis.DaylightActions.ToString();
            woodCountLabel.Content = "Wood: " + marquis.AvailableWood.ToString();

            // Disables the action buttons if action count is = 0

            if (marquis.DaylightActions <= 0)
            {
                DisableActionButtons();
            }
            else 
            { 
                EnableActionButtons();
            }
            
        }

        // Disables the action buttons expect the gain action button
        private void DisableActionButtons()
        {
            marchBtn.IsEnabled = false;
            attackBtn.IsEnabled = false;
            recruitBtn.IsEnabled = false;
            overworkBtn.IsEnabled = false;
            buildRecruiterBtn.IsEnabled = false;
            buildSawmillBtn.IsEnabled = false;
            buildWorkshopBtn.IsEnabled = false;
            
            gainActionBtn.IsEnabled = true; 
        }

        // enables the action buttons
        private void EnableActionButtons()
        {
            marchBtn.IsEnabled = true;
            attackBtn.IsEnabled = true;
            recruitBtn.IsEnabled = true;
            overworkBtn.IsEnabled = true;
            buildRecruiterBtn.IsEnabled = true;
            buildSawmillBtn.IsEnabled = true;
            buildWorkshopBtn.IsEnabled = true;

        }

        #region Action Methods

        private void marchBtn_Click(object sender, RoutedEventArgs e)
        {
            lastActionClicked = "March";
            UpdateActionsInfo();
        }

        private void attackBtn_Click(object sender, RoutedEventArgs e)
        {
            lastActionClicked = "Attack";
            UpdateActionsInfo();
        }

        private void recruitBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateActionsInfo();
            recruitBtn.IsEnabled = false;
        }

        //TODO: add a check to spend a wildcard first
        private void gainActionBtn_Click(object sender, RoutedEventArgs e)
        {
            lastActionClicked = "GainAction";
            // since update actions removes 1 action we add 2
            marquis.DaylightActions += 2;
            UpdateActionsInfo();
            // Calls EnableActionButtons to enable the action buttons duhhhhhhhh
            EnableActionButtons();
        }

        private void overworkBtn_Click(object sender, RoutedEventArgs e)
        {
            lastActionClicked = "Overwork";



            UpdateActionsInfo();
        }

        #endregion


        #region Building Methods
        private void buildWorkShopBtn_Click(object sender, RoutedEventArgs e)
        {
            lastActionClicked = "BuildWorkshop";
            AttemptBuild("Workshop");

            var parentWindow = Window.GetWindow(this) as GameScreen;
            //(MarquisDeCat)parentWindow.Players[PlayerID].TotalBuildings();
            List<int> buildableLocations = new List<int>();
            foreach (Location location in parentWindow.Locations)
            {
                if (location.CanBuild() && location.ruledByPlayer(PlayerID))
                {
                    buildableLocations.Add(location.LocationID);
                }
                Debug.WriteLine($"Location {location.LocationID} can build: {location.CanBuild()}");
                Debug.WriteLine(location.ruledByPlayer(PlayerID));
            }
            foreach (int ints in buildableLocations)
            {
                Debug.WriteLine(ints);

            }
            parentWindow.HighlightLocations(buildableLocations);

            UpdateActionsInfo();
        }

        private void buildSawmillBtn_Click(object sender, RoutedEventArgs e)
        {
            lastActionClicked = "BuildSawmill";

            var parentWindow = Window.GetWindow(this) as GameScreen;
            //(MarquisDeCat)parentWindow.Players[PlayerID].TotalBuildings();
            List<int> buildableLocations = new List<int>();
            foreach (Location location in parentWindow.Locations)
            {
                if (location.CanBuild() && location.ruledByPlayer(PlayerID))
                {
                    buildableLocations.Add(location.LocationID);
                }
                Debug.WriteLine($"Location {location.LocationID} can build: {location.CanBuild()}");
                Debug.WriteLine(location.ruledByPlayer(PlayerID));
            }
            foreach (int ints in buildableLocations)
            {
                Debug.WriteLine(ints);

            }
            parentWindow.HighlightLocations(buildableLocations);

            AttemptBuild("Sawmill");
            UpdateActionsInfo();
        }

        private void buildRecruiterBtn_Click(object sender, RoutedEventArgs e)
        {
            lastActionClicked = "BuildRecruiter";


            var parentWindow = Window.GetWindow(this) as GameScreen;
            //(MarquisDeCat)parentWindow.Players[PlayerID].TotalBuildings();
            List<int> buildableLocations = new List<int>();
            foreach (Location location in parentWindow.Locations)
            {
                if (location.CanBuild() && location.ruledByPlayer(PlayerID))
                {
                    buildableLocations.Add(location.LocationID);
                }
                Debug.WriteLine($"Location {location.LocationID} can build: {location.CanBuild()}");
                Debug.WriteLine(location.ruledByPlayer(PlayerID));
            }
            foreach (int ints in buildableLocations)
            {
                Debug.WriteLine(ints);

            }
            parentWindow.HighlightLocations(buildableLocations);

            UpdateActionsInfo();
        }

        private void AttemptBuild(string buildingType)
        {
            int builtCount = 0;
            int remaining = 0;
            Uri imageSource = null;
            int cost = 0;
            string buildingName = "";
            
           

            switch (buildingType)
            {
                case "Sawmill":
                    builtCount = 6 - marquis.AvailableSawmills;
                    remaining = marquis.AvailableSawmills;
                    imageSource = marquis.SawmillArt;
                    buildingName = "Sawmill";
                    break;
                case "Workshop":
                    builtCount = 6 - marquis.AvailableWorkshops;
                    remaining = marquis.AvailableWorkshops;
                    imageSource = marquis.WorkshopArt;
                    buildingName = "Workshop";
                    break;
                case "Recruiter":
                    builtCount = 6 - marquis.AvailableRecruiters;
                    remaining = marquis.AvailableRecruiters;
                    imageSource = marquis.RecruiterArt;
                    buildingName = "Recruiter";
                    break;
            }

            if (remaining <= 0)
            {
                MessageBox.Show($"Not enough wood to build a {buildingType}. You need {cost} wood.");
                return;
            }

            // deduct wood
            marquis.AvailableWood -= cost;

            // update remaining count
            switch (buildingType)
            {
                case "Sawmill":
                    marquis.AvailableSawmills--;
                    break;
                case "Workshop":
                    marquis.AvailableWorkshops--;
                    break;
                case "Recruiter":
                    marquis.AvailableRecruiters--;
                    break;
            }

            // render building on board -- update later to let user choose location
            //PlaceBuildingOnBoard(imageSource, buildingName);
        }






        #endregion

       
    }
}
