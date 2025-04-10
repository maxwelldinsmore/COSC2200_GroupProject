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
    /// Interaction logic for EyrieEvening.xaml
    /// </summary>
    public partial class EyrieEvening : UserControl
    {
        private Eyrie eyrie;
        int[] roostVPProgression = { 0, 1, 2, 3, 4, 4, 5 };

        public EyrieEvening()
        {
            InitializeComponent();
            this.Loaded += UserControl_Loaded;
            this.Unloaded += UserControl_Unloaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                // get the current player as Marquis de Cat
                eyrie = (Eyrie)parentWindow.Players[parentWindow.CurrentPlayerTurn];
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.Players[parentWindow.CurrentPlayerTurn] = eyrie;
            }
        }

        private void ExecuteEveningPhase()
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null) return;

            // counting how manu roosts are on the board 
            int roostsOnBoard = 7 - eyrie.AvailableRoosts;

            // card draw
            int cardsToDraw = 1;

            if (eyrie.Hand.Count > 5)
            {
                int cardsToDiscard = eyrie.Hand.Count - 5;
                for (int i = 0; i < cardsToDiscard; i++)
                {
                    var cardToDiscard = eyrie.Hand[0];
                    eyrie.Hand.RemoveAt(0);
                    parentWindow.discardPile.Add(cardToDiscard);
                }
            }

            // victory points
            eyrie.VictoryPoints += roostsOnBoard;

            // --- Victory Points from Roosts ---
            int[] roostVPProgression = { 0, 1, 2, 3, 4, 4, 5 };
            int roostsPlaced = 7 - eyrie.AvailableRoosts;

            if (roostsPlaced >= 1 && roostsPlaced <= 7)
            {
                eyrie.VictoryPoints += roostVPProgression[roostsPlaced - 1];
            }


        }
        private void EndEveningButton_Click (object sender, RoutedEventArgs e)
        {
            ExecuteEveningPhase();
        }

    }
}
