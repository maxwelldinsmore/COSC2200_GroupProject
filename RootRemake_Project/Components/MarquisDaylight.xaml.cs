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

               

            }

        }
        /// <summary>
        /// Updates actions and info on the side panel according to the current player
        /// if actions remaining = 0 then end turn button is enabled
        /// </summary>
        private void UpdateActionsInfo()
        {
            marquis.DaylightActions -= 1;
            actionsRemainingLabel.Content = "Actions Remaining: " + marquis.DaylightActions.ToString();
            woodCountLabel.Content = "Wood: " + marquis.AvailableWood.ToString();
        }

        #region Action Methods

        private void marchBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateActionsInfo();
        }

        private void attackBtn_Click(object sender, RoutedEventArgs e)
        {
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
            // since update actions removes 1 action we add 2
            marquis.DaylightActions += 2;
            UpdateActionsInfo();
        }

        private void overworkBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateActionsInfo();
        }

        #endregion


        #region Building Methods
        private void buildWorkShopBtn_Click(object sender, RoutedEventArgs e)
        {
            
            AttemptBuild("Workshop");

            UpdateActionsInfo();
        }

        private void buildSawmillBtn_Click(object sender, RoutedEventArgs e)
        {
            AttemptBuild("Sawmill");
            UpdateActionsInfo();
        }

        private void buildRecruiterBtn_Click(object sender, RoutedEventArgs e)
        {
            AttemptBuild("Recruiter");
            var parentWindow = Window.GetWindow(this) as GameScreen;
            //(MarquisDeCat)parentWindow.Players[PlayerID].TotalBuildings();
            List<int> buildableLocations = new List<int>();
            foreach (Location location in parentWindow.Locations)
            {
                if (location.CanBuild() && location.ruledByPlayer(PlayerID))
                {
                    buildableLocations.Append(location.LocationID);
                }
            }
            parentWindow.HighlightLocations(buildableLocations);
            //marquis.lastAction = "BuildRecruiter";
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
            PlaceBuildingOnBoard(imageSource, buildingName);
        }

        private void PlaceBuildingOnBoard(Uri buildingUri, string buildingName)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;

            Building building = new Building(PlayerID, buildingName);

            Image buildingImage = new Image
            {
                Source = new BitmapImage(buildingUri),
                Width = 25,
                Height = 25,
                Name = building.BuildingKey,
                IsHitTestVisible = false
            };

            // update it later with location methods
            Point buildingPoint = new Point(100, 100); // example

            Canvas.SetLeft(buildingImage, buildingPoint.X);
            Canvas.SetTop(buildingImage, buildingPoint.Y);
            if (parentWindow != null)
            {
                parentWindow.canvasGameBoard.Children.Add(buildingImage);

            }
        }




        #endregion

       
    }
}
