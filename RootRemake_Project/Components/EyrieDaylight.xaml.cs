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
using ControlzEx.Standard;
using RootRemake_Project.CharacterClasses;
using RootRemake_Project.ObjectClasses;
using RootRemake_Project.LocationClasses;
using System.Diagnostics;
namespace RootRemake_Project.Components
{
    /// <summary>
    /// Interaction logic for EyrieDaylight.xaml
    /// </summary>
    public partial class EyrieDaylight : UserControl
    {

        public event EventHandler SetupLoaded;
        private int lastLocationClicked;
        private Eyrie eyrie;
        private int playerID;
        private bool turmoilOccured = false;
        private string currentAction = "None";
        private bool moving = false;

        private int movingFromLocationID = -1;

        public EyrieDaylight()
        {
            InitializeComponent();
            this.Loaded += EyrieDaylight_Loaded;
        }

        private void EyrieDaylight_Loaded(object sender, RoutedEventArgs e)
        {
            SetupLoaded?.Invoke(this, EventArgs.Empty);

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                //parentWindow.endTurnBtn.IsEnabled = false;
                playerID = parentWindow.CurrentPlayerTurn;
                parentWindow.LocationClicked += ParentWindow_LocationClicked;
                eyrie = (Eyrie)parentWindow.Players[playerID];
            }
            RefreshDecree();
            Turmoil();
            // Loads in icons for the eyrie Events
        }
        private void ParentWindow_LocationClicked(object sender, int locationId)
        {
            var ParentWindow = Window.GetWindow(this) as GameScreen;
            if (ParentWindow != null)
            {
                ParentWindow.HighlightLocations(new List<int>());
            }
            
            switch (currentAction)
            {
                case "Recruit":
                    FinalizeRecruit(locationId);
                    break;
                case "Move":
                    if (!moving)
                    {
                        FinalizeMove(locationId);
                        return;
                    } else
                    {
                        FinalizeSecondMove(locationId);
                        moving = false;
                    }
                    break;

                case "Attack":
                    FinalizeAttack(locationId);
                    break;
                case "Build":
                    FinalizeBuild(locationId);
                    break;
                default:
                    break;
            }
           
            RefreshDecree();
        }

        private void CheckDecree()
        {
            // Check if the decree is empty
            if (eyrie.attackDecree.Count == 0 && eyrie.moveDecree.Count == 0 && eyrie.recruitDecree.Count == 0 && eyrie.buildDecree.Count == 0)
            {
                // Disable the end turn button
                var parentWindow = Window.GetWindow(this) as GameScreen;
                if (parentWindow != null)
                {
                    parentWindow.endTurnBtn.IsEnabled = true;
                    return;
                }
            }

            // Checks what decrees are left to be made 

            if (eyrie.recruitDecree.Count > 0)
            {
                recruitLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0269C6"));
                moveLbl.Foreground = new SolidColorBrush(Colors.White);
                attackLbl.Foreground = new SolidColorBrush(Colors.White);
                buildLbl.Foreground = new SolidColorBrush(Colors.White);
                currentAction = "Recruit";

                Recruit();
                return;
            }

            if (eyrie.moveDecree.Count > 0)
            {
                moveLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0269C6"));
                recruitLbl.Foreground = new SolidColorBrush(Colors.White);
                attackLbl.Foreground = new SolidColorBrush(Colors.White);
                buildLbl.Foreground = new SolidColorBrush(Colors.White);
                currentAction = "Move";
                Move();
                return;
            }

            if (eyrie.attackDecree.Count > 0)
            {
                attackLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0269C6"));
                moveLbl.Foreground = new SolidColorBrush(Colors.White);
                recruitLbl.Foreground = new SolidColorBrush(Colors.White);
                buildLbl.Foreground = new SolidColorBrush(Colors.White);
                currentAction = "Attack";
                
                Attack();
                return;
            }

            if (eyrie.buildDecree.Count > 0)
            {
                buildLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0269C6"));
                attackLbl.Foreground = new SolidColorBrush(Colors.White);
                recruitLbl.Foreground = new SolidColorBrush(Colors.White);
                moveLbl.Foreground = new SolidColorBrush(Colors.White);
                currentAction = "Build";
                Build();
                return;
            }


        }

        #region Recruit Actions
        private void Recruit()
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;

            List<int> Highlightable = new List<int>();
            if (parentWindow != null)
            {
                foreach (Location location in parentWindow.Locations)
                {
                    // Check if roosts are in the location
                    if (location.Buildings.Any(building => building.BuildingType == "Roost"))
                    {
                        if (eyrie.recruitDecree.Contains(1))
                        {
                            Highlightable.Add(location.LocationID);
                        }
                        else if (eyrie.recruitDecree.Any(i => i == location.LocationFaction))
                        {
                            Highlightable.Add(location.LocationID);
                        }
                    }
                }
                parentWindow.HighlightLocations(Highlightable);

            }
        }
        
        

        private void FinalizeRecruit(int locationID)
        {
            var parentLocation = Window.GetWindow(this) as GameScreen;
            if (parentLocation == null) return;
            Location location = parentLocation.Locations[locationID];
            parentLocation.AddWarriorToLocation(location.LocationID, 1, playerID);
           
            // Deletes the action from the decree
            bool deleted = eyrie.recruitDecree.Remove(location.LocationFaction);
            if (!deleted)
            {
                eyrie.recruitDecree.Remove(1);
            }
        }

        #endregion

        #region Move Actions

        private void Move()
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;

            List<int> Highlightable = new List<int>();

            foreach (Location location in parentWindow.Locations)
            {
                // Check if roosts are in the location
                if (location.Armies.Any(army => army.PlayerID == playerID))
                {
                    if (eyrie.moveDecree.Contains(1))
                    {
                        Highlightable.Add(location.LocationID);
                    }
                    else if (eyrie.moveDecree.Any(i => i == location.LocationFaction))
                    {
                        Highlightable.Add(location.LocationID);
                    }
                }
                
                
            }
            parentWindow.HighlightLocations(Highlightable);
        }

        private void FinalizeMove(int locationID)
        {
            movingFromLocationID = locationID;
            List<int> Highlightables = new List<int>();
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                Location location = parentWindow.Locations[locationID];

                foreach (int loc in parentWindow.Locations[locationID].ConnectedLocations)
                {
                    if (parentWindow.Locations[loc].LocationType != "Forest")
                    {
                        Highlightables.Add(loc);
                    }
                }
                moving = true;
                parentWindow.HighlightLocations(Highlightables);
            }
            
        }
        private void FinalizeSecondMove(int locationID)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                Location location = parentWindow.Locations[locationID];
                // Deletes the action from the decree
                bool deleted = eyrie.moveDecree.Remove(parentWindow.Locations[movingFromLocationID].LocationFaction);
                if (!deleted)
                {
                    eyrie.moveDecree.Remove(1);
                }

                parentWindow.AddWarriorToLocation(location.LocationID, 1, playerID);
                parentWindow.DeductWarrior(movingFromLocationID, playerID, 1);
            }
            
            
        }
        
        #endregion

        #region Attack Actions

        private void Attack()
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            List<int> buildableLocations = new List<int>();
            if (parentWindow != null)
            {
                foreach (Location location in parentWindow.Locations)
                {
                    if (location.Armies.Count > 1 && location.Armies.Any(c => c.PlayerID == playerID))
                    {
                        if (eyrie.buildDecree.Contains(1))
                        {
                            buildableLocations.Add(location.LocationID);
                        }
                        else if (eyrie.buildDecree.Any(i => i == location.LocationFaction))
                        {
                            buildableLocations.Add(location.LocationID);
                        }
                    }
                }
                parentWindow.HighlightLocations(buildableLocations);
            }
        }

        private void FinalizeAttack(int locationID)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                Location location = parentWindow.Locations[locationID];
                // Deletes the action from the decree
                bool deleted = eyrie.attackDecree.Remove(parentWindow.Locations[movingFromLocationID].LocationFaction);
                if (!deleted)
                {
                    eyrie.attackDecree.Remove(1);
                }

                // Battle Actions

                Army myArmy = location.Armies.FirstOrDefault(a => a.PlayerID == playerID);

                Army enemyArmy = location.Armies.FirstOrDefault(a => a.PlayerID != playerID);
                if (myArmy != null && enemyArmy != null)
                {
                    int[] damage = eyrie.Battle();
                    MessageBox.Show(parentWindow.Players[enemyArmy.PlayerID].UserName + " took " + damage[1] + " damage and " + parentWindow.Players[playerID].UserName + " took " + damage[0] + " damage.");
                    parentWindow.DeductWarrior(locationID, enemyArmy.PlayerID, damage[1]);
                    parentWindow.DeductWarrior(locationID, playerID, damage[0]);
                }

            }

        }
        #endregion

        #region Build Actions
        private void Build()
        {
           
            var parentWindow = Window.GetWindow(this) as GameScreen;
            List<int> buildableLocations = new List<int>();
            if (parentWindow != null)
            {
                foreach (Location location in parentWindow.Locations)
                {
                    if (location.CanBuild() && location.ruledByPlayer(playerID) )
                    {
                        if (eyrie.buildDecree.Contains(1))
                        {
                            buildableLocations.Add(location.LocationID);
                           

                        }
                        else if (eyrie.buildDecree.Any(i => i == location.LocationFaction))
                        {
                            buildableLocations.Add(location.LocationID);
                            
                        }
                    }
                    Debug.WriteLine($"Location {location.LocationID} can build: {location.CanBuild()}");
                    Debug.WriteLine(location.ruledByPlayer(playerID));
                }
               
                parentWindow.HighlightLocations(buildableLocations);
            }
            
        }
        private void FinalizeBuild(int locationID)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null )
            {
                parentWindow.AddBuildingToLocation(locationID, playerID, "Roost");
            }

        }

        #endregion


        #region Turmoil Methods

        private void Turmoil()
        {
            foreach (UIElement content in mainGrid.Children)
            {
                if (content is FrameworkElement element && element.Name != "headerText")
                {
                    element.Visibility = Visibility.Hidden;
                }
            }

            headerText.Foreground = new SolidColorBrush(Colors.Red);
            headerText.Text = "Turmoil! Your decree has failed and you must start anew";
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.endTurnBtn.IsEnabled = true;
            }
        }

        #endregion

        private void RefreshDecree()
        {
            attackGrid.Children.Clear();
            moveGrid.Children.Clear();
            recruitGrid.Children.Clear();
            buildGrid.Children.Clear();
            int xShift = 0;
            foreach (int suit in eyrie.attackDecree)
            {

                Image icon = GenerateIcon(suit);
                icon.Margin = new Thickness(xShift, 0, 0, 0);
                icon.HorizontalAlignment = HorizontalAlignment.Left;

                attackGrid.Children.Add(icon);
                
                xShift += 50;
            }
            xShift = 0;
            foreach (int suit in eyrie.moveDecree)
            {

                Image icon = GenerateIcon(suit);
                icon.Margin = new Thickness(xShift, 0, 0, 0);
                icon.HorizontalAlignment = HorizontalAlignment.Left;
                moveGrid.Children.Add(icon);
                xShift += 50;
            }
            xShift = 0;
            foreach (int suit in eyrie.recruitDecree)
            {

                Image icon = GenerateIcon(suit);
                icon.Margin = new Thickness(xShift, 0, 0, 0);
                icon.HorizontalAlignment = HorizontalAlignment.Left;

                recruitGrid.Children.Add(icon);

                xShift += 50;
            }
            xShift = 0;
            foreach (int suit in eyrie.buildDecree)
            {

                Image icon = GenerateIcon(suit);
                icon.Margin = new Thickness(xShift, 0, 0, 0);
                icon.HorizontalAlignment = HorizontalAlignment.Left;
                buildGrid.Children.Add(icon);

                xShift += 50;
            }
            CheckDecree();

        }
        //Suit 1 is Wild, Suit 2 is Fox, Suit 3 is Bunny, Suit 4 is Rat.
        private Image GenerateIcon(int suit)
        {
            Uri imageUri;
            switch (suit)
            {
                case 1:
                    imageUri = new Uri("pack://application:,,,/Assets/Bird.png", UriKind.RelativeOrAbsolute);
                    break;
                case 2:
                    imageUri = new Uri("pack://application:,,,/Assets/Fox.png", UriKind.RelativeOrAbsolute);
                    break;
                case 3:
                    imageUri = new Uri("pack://application:,,,/Assets/Bunny.png", UriKind.RelativeOrAbsolute);
                    break;
                case 4:
                    imageUri = new Uri("pack://application:,,,/Assets/Mouse.png", UriKind.RelativeOrAbsolute);
                    break;

                default:
                    imageUri = new Uri("pack://application:,,,/Assets/Bird.png", UriKind.RelativeOrAbsolute);
                    break;
            }

            return new Image
            {
                Source = new BitmapImage(imageUri),
                Width = 60,
                Height = 40,
                Stretch = Stretch.Uniform
            };
             

        }
    }
}
