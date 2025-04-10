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
    /// Interaction logic for MarquisEvening.xaml
    /// </summary>
    public partial class MarquisEvening : UserControl
    {
        private MarquisDeCat marquis;
        public MarquisEvening()
        {
            InitializeComponent();
            this.Loaded += UserControl_Loaded;
        }

        private void MarquisEvening_Unloaded(object sender, RoutedEventArgs e)
        {
            //var parentWindow = Window.GetWindow(this) as GameScreen;
            //if (parentWindow != null)
            //{
            //    parentWindow.Players[parentWindow.CurrentPlayerTurn] = marquis;
            //}
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                // get the current player as Marquis de Cat
                marquis = (MarquisDeCat)parentWindow.Players[parentWindow.CurrentPlayerTurn];
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.Players[parentWindow.CurrentPlayerTurn] = marquis;
                EndEveningPhase();
            }
        }

        private void EndEveningPhase()
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                int recruiterCount = 6 - marquis.AvailableRecruiters;
                int cardsToDraw = 1;

                // draw 1 extra card if have 3 recruiters
                // draw 1 more extra card if have 5 recruiters+
                if (recruiterCount == 3)
                {
                    cardsToDraw += 1;
                } else if (recruiterCount == 5)
                {
                    cardsToDraw += 2;
                }

                // draw cards
                parentWindow.DrawCardsForPlayer(parentWindow.CurrentPlayerTurn, cardsToDraw);

                //// auto discarding if over 5 cards
                //if (marquis.Hand.Count > 5)
                //{
                //    int cardsToDiscard = marquis.Hand.Count - 5;

                //    for (int i = 0; i < cardsToDiscard; i++)
                //    {
                //        CardHand cardToDiscard = marquis.Hand[0]; // remove the first card
                //        marquis.Hand.RemoveAt(0);
                //        parentWindow.discardPile.Add(cardToDiscard);
                //    }
                //}

            }
        }

        private void EndEveningButton_Click(object sender, RoutedEventArgs e)
        {
            EndEveningPhase();
        }

    }
}
