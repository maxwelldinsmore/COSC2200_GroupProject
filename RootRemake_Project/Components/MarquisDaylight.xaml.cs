using RootRemake_Project.CharacterClasses;
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

        public MarquisDaylight()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SidePanelLoaded?.Invoke(this, EventArgs.Empty);

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.LocationClicked += ParentWindow_LocationClicked;
                parentWindow.HighlightLocations(new int[] { 0, 3, 8, 11 });
                // Gets a copy of the marquis de cat object for the current player
                // then returns the object once end actions are run out
            }
        }
        private void ParentWindow_LocationClicked(object sender, int locationId)
        {
            lastLocationClicked = locationId;

            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            { 

                // Disconnects the event
                parentWindow.LocationClicked -= ParentWindow_LocationClicked;

            }

        }

        private void UpdateActions()
        {
            // Updates the actions available to the player
            // based on the number of buildings they have
            // and the number of warriors they have
            // and the number of cards in their hand
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void attackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void recruitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gainActionBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
