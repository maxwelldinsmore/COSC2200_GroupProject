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
    /// Interaction logic for EyrieSetup.xaml
    /// </summary>
    public partial class EyrieSetup : UserControl
    {
        public event EventHandler SidePanelLoaded;
        private int lastLocationClicked;
        public EyrieSetup()
        {
            InitializeComponent();
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SidePanelLoaded?.Invoke(this, EventArgs.Empty);

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.LocationClicked += ParentWindow_LocationClicked;
                parentWindow.HighlightLocations(new int[] { 0, 3, 8, 11 });

            }
        } 
    
        private void ParentWindow_LocationClicked(object sender, int locationId)
        {
            lastLocationClicked = locationId;

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.AddWarriorToLocation(locationId, 6, lastLocationClicked);

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
