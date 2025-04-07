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
            this.Loaded += MarquisSetup_Loaded;
        }

        private void MarquisSetup_Loaded(object sender, RoutedEventArgs e)
        {
            SetupLoaded?.Invoke(this, EventArgs.Empty);

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.LocationClicked += ParentWindow_LocationClicked;
            }
        }

        private void ParentWindow_LocationClicked(object sender, int locationId)
        {
            lastLocationClicked = locationId;

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
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
                        parentWindow.AddWarriorToLocation(location.LocationID, 1, lastLocationClicked);
                    }
                }

                // Disables Location highlights
                parentWindow.HighlightLocations([]);

                // Changes Text telling player to end setup phase
                setupTextBlock.Text = "Setup Complete!";

                // Disconnects the event
                parentWindow.LocationClicked -= ParentWindow_LocationClicked;
            }
  

        }

    }


    
}
