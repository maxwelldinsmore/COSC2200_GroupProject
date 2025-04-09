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
    /// Interaction logic for MarquisSetup.xaml
    /// </summary>
    public partial class MarquisSetup : UserControl
    {
        public event EventHandler SetupLoaded;
        private int lastLocationClicked;

        public MarquisSetup()
        {
            InitializeComponent();
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetupLoaded?.Invoke(this, EventArgs.Empty);

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.LocationClicked += ParentWindow_LocationClicked;
                parentWindow.HighlightLocations(new List<int> { 0, 3, 8, 11 });
                parentWindow.endTurnBtn.IsEnabled = false;
                

                
            }

        }

        private void ParentWindow_LocationClicked(object sender, int locationId)
        {
            lastLocationClicked = locationId;

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                int currentPlayer = parentWindow.CurrentPlayerTurn;
                int OppositeCorner = -1;
                if (lastLocationClicked == 0)
                {
                    OppositeCorner = 11;
                }
                else if (lastLocationClicked == 3)
                {
                    OppositeCorner = 8;
                }
                else if (lastLocationClicked == 8)
                {
                    OppositeCorner = 3;
                }
                else if (lastLocationClicked == 11)
                {
                    OppositeCorner = 0;
                }

                foreach (var location in parentWindow.Locations)
                {
                    if (location.LocationID != OppositeCorner && location.LocationType != "Forest")
                    {
                        parentWindow.AddWarriorToLocation(location.LocationID, 1, parentWindow.CurrentPlayerTurn);

                    }
                }

                // Adds each one of the building types

                // gets 2 neighboring clearing to place the other two buildings

                // Get a random connected location
                Random random = new Random();
                int location1 = parentWindow.Locations[locationId].ConnectedLocations
                    .Where(locId => parentWindow.Locations[locId].LocationType != "Forest") // Filter out forests
                    .OrderBy(x => random.Next())
                    .First();

                // Get a random connected location
                int location2 = parentWindow.Locations[locationId].ConnectedLocations
                    .Where(locId => parentWindow.Locations[locId].LocationType != "Forest" && locId != location1) // Filter out forests
                    .OrderBy(x => random.Next()) // Shuffle the remaining locations
                    .First();

                parentWindow.AddBuildingToLocation(locationId, currentPlayer, "Recruiter");

                
                parentWindow.AddBuildingToLocation(location2, currentPlayer, "Workshop");
                parentWindow.AddBuildingToLocation(location1, currentPlayer, "Sawmill");

                

                // Disables Location highlights
                parentWindow.HighlightLocations([]);

                // Changes Text telling player to end setup phase
                setupTextBlock.Text = "Setup Complete!";

                // Disconnects the event
                parentWindow.LocationClicked -= ParentWindow_LocationClicked;

                // Enables the end turn button
                parentWindow.endTurnBtn.IsEnabled = true;
            }


        }

    }



    
}
