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
        private bool recruitDone;
        private bool moving = false; // used to check if the player is moving a warrior or not
        private int movingFromLocationID;
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
                if (parentWindow?.cardHand != null)
                {
                    parentWindow.cardHand.CardClicked += (cardId) => CardClicked(parentWindow.cardHand, cardId);
                }
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
                UpdateActionsInfo();
            }
            if (parentWindow?.cardHand != null)
            {
                parentWindow.cardHand.CardClicked += (cardId) => CardClicked(parentWindow.cardHand, cardId);
            }

        }

        private void CardClicked(object sender, string cardID)
        {
            Debug.WriteLine($"CardID: {cardID}");
            foreach (var card in marquis.Hand)
            {
                Debug.WriteLine($"Card in Hand: {card.CardKey}");
            }
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow == null) return;
            List<Card> cardCopy = new List<Card>(marquis.Hand);
            foreach (Card card in cardCopy)
            {
                if (card.CardKey == cardID)
                {
                    if (lastActionClicked == "Overwork")
                    {
                        parentWindow.Players[parentWindow.CurrentPlayerTurn].Hand.Remove(card);
                        // REFRESH HAND
                        parentWindow.UpdateHandDisplay();
                        parentWindow.endTurnBtn.IsEnabled = true;
                        marquis.AvailableWood += 1;
                    }
                    if (card.Suit == 1 && lastActionClicked == "GainAction")
                    {
                        parentWindow.Players[parentWindow.CurrentPlayerTurn].Hand.Remove(card);
                        // REFRESH HAND
                        parentWindow.UpdateHandDisplay();
                        parentWindow.endTurnBtn.IsEnabled = true;
                        marquis.DaylightActions += 1;
                        UpdateActionsInfo();
                    }
                    else if (lastActionClicked == "GainAction")
                    {
                        MessageBox.Show("You can only gain an action via a wild card");
                    }

                }
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
                        if (!moving)
                        {

                            movingFromLocationID = locationId;
                            List<int> Highlightables = new List<int>();
                           
                            Location location = parentWindow.Locations[locationId];

                            foreach (int loc in parentWindow.Locations[locationId].ConnectedLocations)
                            {
                                if (parentWindow.Locations[loc].LocationType != "Forest")
                                {
                                    Highlightables.Add(loc);
                                    
                                }
                            }
                            moving = true;
                            parentWindow.HighlightLocations(Highlightables);
                            
                        } else
                        {
                            Location location = parentWindow.Locations[locationId];
                            // Deletes the action from the decree
                            

                            parentWindow.AddWarriorToLocation(location.LocationID, 1, PlayerID);
                            parentWindow.DeductWarrior(movingFromLocationID, PlayerID, 1);
                            moving = false;
                            marquis.DaylightActions -= 1;
                        }
                            break;
                    case "Attack":
                        
                        Army myArmy = parentWindow.Locations[locationId].Armies.FirstOrDefault(a => a.PlayerID == PlayerID);

                        Army enemyArmy = parentWindow.Locations[locationId].Armies.FirstOrDefault(a => a.PlayerID != PlayerID);
                        if (myArmy != null && enemyArmy != null)
                        {
                            int[] damage = marquis.Battle();
                            MessageBox.Show(parentWindow.Players[enemyArmy.PlayerID].UserName + " took " + damage[1] + " damage and " + parentWindow.Players[PlayerID].UserName + " took " + damage[0] + " damage.");
                            parentWindow.DeductWarrior(locationId, enemyArmy.PlayerID, damage[1]);
                            parentWindow.DeductWarrior(locationId, PlayerID, damage[0]);
                        }
                        break;
                    case "BuildWorkshop":
                        parentWindow.AddBuildingToLocation(locationId, PlayerID, "Workshop");
                        marquis.AvailableWood -= marquis.BuildingCosts[6 - marquis.AvailableWorkshops];
                        marquis.AvailableWorkshops--;
                        marquis.DaylightActions -= 1;
                        marquis.VictoryPoints += marquis.WorkShopVP[6 - marquis.AvailableWorkshops];
                        break;
                    case "BuildSawmill":
                        parentWindow.AddBuildingToLocation(locationId, PlayerID, "Sawmill");
                        marquis.AvailableWood -= marquis.BuildingCosts[6 - marquis.AvailableSawmills];
                        marquis.AvailableSawmills--;
                        marquis.DaylightActions -= 1;
                        marquis.VictoryPoints += marquis.SawmillVP[6 - marquis.AvailableSawmills];
                        break;
                    case "BuildRecruiter":
                        parentWindow.AddBuildingToLocation(locationId, PlayerID, "Recruiter");
                        marquis.AvailableWood -= marquis.BuildingCosts[6 - marquis.AvailableRecruiters];
                        marquis.AvailableRecruiters--;
                        marquis.DaylightActions -= 1;
                        marquis.VictoryPoints += marquis.RecruiterVP[6 - marquis.AvailableRecruiters];
                        break;
                    default:
                        break;

                }
                if (!moving)
                {
                    parentWindow.HighlightLocations(new List<int>());

                }


            }
            UpdateActionsInfo();

        }
        /// <summary>
        /// Updates actions and info on the side panel according to the current player
        /// if actions remaining = 0 then end turn button is enabled
        /// Disabling all action buttons once the action counter has finished but keeping the gain action button enabled to gain another action
        /// </summary>
        private void UpdateActionsInfo()
        {
            
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
            recruitWoodLbl.Content = marquis.BuildingCosts[6-marquis.AvailableRecruiters];
            sawmillWoodLbl.Content = marquis.BuildingCosts[6 - marquis.AvailableSawmills];
            workshopWoodLbl.Content = marquis.BuildingCosts[6 - marquis.AvailableWorkshops];

            // Updates parent window with the current player
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.Players[parentWindow.CurrentPlayerTurn] = marquis;
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
            Debug.WriteLine($"Available Wood: {marquis.AvailableWood}");
            Debug.WriteLine($"Sawmill Cost: {marquis.BuildingCosts[6 - marquis.AvailableSawmills]}");

            // THIS NOT WORKING TODO
            if (recruitDone)
            {
                recruitBtn.IsEnabled = false;
            }
            else
            {
                recruitBtn.IsEnabled = true;
            }

            if (marquis.BuildingCosts[6-marquis.AvailableSawmills] > marquis.AvailableWood)
            {
                buildSawmillBtn.IsEnabled = false;
            }
            else
            {
                buildSawmillBtn.IsEnabled = true;
            }
            if (marquis.BuildingCosts[6-marquis.AvailableWorkshops] > marquis.AvailableWood)
            {
                buildWorkshopBtn.IsEnabled = false;
            }
            else
            {
                buildWorkshopBtn.IsEnabled = true;
            }
            if (marquis.BuildingCosts[6-marquis.AvailableRecruiters] > marquis.AvailableWood)
            {
                buildRecruiterBtn.IsEnabled = false;
            }
            else
            {
                buildRecruiterBtn.IsEnabled = true;
            }

            marchBtn.IsEnabled = true;
            attackBtn.IsEnabled = true;
            recruitBtn.IsEnabled = true;
            overworkBtn.IsEnabled = true;

        }

        #region Action Methods

        private void marchBtn_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;

            List<int> Highlightable = new List<int>();

            if (parentWindow != null)
            {
                foreach (Location location in parentWindow.Locations)
                {
                    // Check if roosts are in the location
                    if (location.Armies.Any(army => army.PlayerID == PlayerID))
                    {
                      
                        Highlightable.Add(location.LocationID);
 
                    }
                }
                parentWindow.HighlightLocations(Highlightable);
            }
            lastActionClicked = "March";
            UpdateActionsInfo();
        }

        private void attackBtn_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            List<int> fightableLocations = new List<int>();
            if (parentWindow != null)
            {
                foreach (Location location in parentWindow.Locations)
                {
                    if (location.Armies.Count > 1 && location.Armies.Any(c => c.PlayerID == PlayerID))
                    {
                        fightableLocations.Add(location.LocationID); 
                    }
                }
                
                parentWindow.HighlightLocations(fightableLocations);
            }
            lastActionClicked = "Attack";
            UpdateActionsInfo();
        }

        private void recruitBtn_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                foreach (Location location in parentWindow.Locations)
                {
                    foreach (Building building in location.Buildings)
                    {
                        if (building.BuildingType == "Recruiter")
                        {
                            parentWindow.AddWarriorToLocation(location.LocationID, 1, PlayerID);
                        }
                    }
                }
            }
            marquis.DaylightActions -= 1;
            UpdateActionsInfo();
            recruitBtn.IsEnabled = false;
        }

       
        private void gainActionBtn_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow.cardHand.Visibility != Visibility.Visible)
            {
                parentWindow.toggleHandBtn_Click(parentWindow, new RoutedEventArgs());
            }

            lastActionClicked = "GainAction";
            // since update actions removes 1 action we add 2
            
            UpdateActionsInfo();
            
        }

        private void overworkBtn_Click(object sender, RoutedEventArgs e)
        {
            lastActionClicked = "Overwork";
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow.cardHand.Visibility != Visibility.Visible)
            {
                parentWindow.toggleHandBtn_Click(parentWindow, new RoutedEventArgs());
            }

            marquis.DaylightActions++;
            UpdateActionsInfo();
            marquis.DaylightActions -= 1;
        }

        #endregion


        #region Building Methods
        private void buildWorkShopBtn_Click(object sender, RoutedEventArgs e)
        {
            lastActionClicked = "BuildWorkshop";
            

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
            recruitDone = true;
          
        }

        





        #endregion

       
    }
}
