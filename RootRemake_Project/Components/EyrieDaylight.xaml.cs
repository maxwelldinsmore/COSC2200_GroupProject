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
                    FinalizeMove(locationId);
                    break;
                case "MoveTo":
                    FinalizeSecondMove(locationId);
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
            CheckDecree();
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
                currentAction = "Recruit";
                Recruit();
                return;
            }

            if (eyrie.moveDecree.Count > 0)
            {
                moveLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0269C6"));
                recruitLbl.Foreground = new SolidColorBrush(Colors.White);
                currentAction = "Move";
                Move();
                return;
            }

            if (eyrie.attackDecree.Count > 0)
            {
                attackLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0269C6"));
                moveLbl.Foreground = new SolidColorBrush(Colors.White);
                currentAction = "Attack";
                
                // TODO: IMPLEMENT ATTACK
                //Attack();
                return;
            }

            if (eyrie.buildDecree.Count > 0)
            {
                buildLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0269C6"));
                attackLbl.Foreground = new SolidColorBrush(Colors.White);
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

            foreach (Location location in parentWindow.Locations)
            {
                // Check if roosts are in the location
                if (location.Buildings.Any(building => building.BuildingType == "Roost") )
                {
                    if (eyrie.recruitDecree.Contains(1))
                    {
                        Highlightable.Add(location.LocationID);
                    } else if (eyrie.recruitDecree.Any(i => i == location.LocationFaction))
                    {
                        Highlightable.Add(location.LocationID);
                    }
                }
            }
            parentWindow.HighlightLocations(Highlightable);
        }
        
        

        private void FinalizeRecruit(int locationID)
        {
            var parentLocation = Window.GetWindow(this) as GameScreen;
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
            MessageBox.Show("Move");
            var parentWindow = Window.GetWindow(this) as GameScreen;

            List<int> Highlightable = new List<int>();

            foreach (Location location in parentWindow.Locations)
            {
                // Check if roosts are in the location

                if (eyrie.moveDecree.Contains(1))
                {
                    Highlightable.Add(location.LocationID);
                } else if (eyrie.moveDecree.Any(i => i == location.LocationFaction))
                {
                    Highlightable.Add(location.LocationID);
                }
                
            }
            parentWindow.HighlightLocations(Highlightable);
        }

        private void FinalizeMove(int locationID)
        {
            List<int> Highlightable = new List<int>();
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                Location location = parentWindow.Locations[locationID];

                // Adds to 
                foreach (Location locationCurrent in parentWindow.Locations)
                {
                    // Check if roosts are in the location

                    if (eyrie.recruitDecree.Contains(1))
                    {
                        Highlightable.Add(locationCurrent.LocationID);
                    }
                    else if (eyrie.recruitDecree.Any(i => i == location.LocationFaction))
                    {
                        Highlightable.Add(location.LocationID);
                    }

                }
                parentWindow.HighlightLocations(Highlightable);
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
            }
            
            
        }
        
        #endregion

        #region Attack Actions

        private void Attack()
        {
        }

        private void FinalizeAttack(int locationID)
        {
        }
        #endregion

        #region Build Actions
        private void Build()
        {
        }
        private void FinalizeBuild(int locationID)
        {

        }

        #endregion
        private void RefreshDecree()
        {
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
