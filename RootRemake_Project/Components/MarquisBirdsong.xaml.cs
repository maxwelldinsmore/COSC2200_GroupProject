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
    /// Interaction logic for MarquisBirdsong.xaml
    /// </summary>
    public partial class MarquisBirdsong : UserControl
    {
        private MarquisDeCat marquis;
        public MarquisBirdsong()
        {
            InitializeComponent();
            this.Loaded += UserControl_Loaded;
    
        }


        private void MarquisBirdsong_Unloaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.Players[parentWindow.CurrentPlayerTurn] = marquis;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                // get the current player as Marquis de Cat
                marquis = (MarquisDeCat)parentWindow.Players[parentWindow.CurrentPlayerTurn];
                AddWoodToSawmill();
            }
        }

        public void AddWoodToSawmill()
        {
            marquis.AddWood(6 - marquis.AvailableSawmills);
            UpdateWoodlabel();
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.Players[parentWindow.CurrentPlayerTurn] = marquis;
            }
        }

        private void UpdateWoodlabel()
        {
            woodCountLabel.Content = marquis.AvailableWood.ToString();
        }
    }
}
