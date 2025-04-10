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
            
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                // get the current player as Marquis de Cat
                eyrie = (Eyrie)parentWindow.Players[parentWindow.CurrentPlayerTurn];
                ExecuteEveningPhase();
            }
            


        }


        private void ExecuteEveningPhase()
        {
            
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                // counting how manu roosts are on the board 
                int roostsOnBoard = 7 - eyrie.AvailableRoosts;

                // card draw
                int cardsToDraw = 1;

                if (eyrie.AvailableRoosts < 4)
                {
                    // draw 1 extra card if have 3 recruiters
                    cardsToDraw++;
                }
                if (eyrie.AvailableRoosts < 2)
                {
                    cardsToDraw++;

                }

                cardText.Text = "Drew " + cardsToDraw.ToString() + " Cards";
                parentWindow.DrawCardsForPlayer(parentWindow.CurrentPlayerTurn, cardsToDraw);

                // victory points
                eyrie.VictoryPoints += roostsOnBoard;

                // --- Victory Points from Roosts ---
                int roostsPlaced = 7 - eyrie.AvailableRoosts;

                if (roostsPlaced >= 1 && roostsPlaced <= 7)
                {
                    eyrie.VictoryPoints += roostVPProgression[roostsPlaced - 1];
                }
            }

            


        }
       

    }
}
