using RootRemake_Project.CharacterClasses;
using RootRemake_Project.ObjectClasses;
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
    /// Interaction logic for EyrieBirdsong.xaml
    /// </summary>
    public partial class EyrieBirdsong : UserControl
    {
        int playerID;
        private Eyrie eyrie;
        public EyrieBirdsong()
        {
            InitializeComponent();
            this.Loaded += EyrieBirdsong_Loaded;

        }

        private void EyrieBirdsong_Loaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;
            if (parentWindow != null)
            {
                parentWindow.endTurnBtn.IsEnabled = false;
                playerID = parentWindow.CurrentPlayerTurn;
                eyrie = (Eyrie)parentWindow.Players[playerID];
            }
            RefreshDecree();
            // Loads in icons for the eyrie Events

            // Ties on cardhand click from parent window to function
            if (parentWindow?.cardHand != null)
            {
                parentWindow.cardHand.CardClicked += (cardId) => CardClicked(parentWindow.cardHand, cardId);
            }
        }

        private void CardClicked(object sender, string cardID)
        {
            // Handle the card click event here
            // You can access the clicked card using e.Card
            // For example, you can display the card's name in a message box


            // reenables all the buttons
            recruitBtn.IsEnabled = true;
            moveBtn.IsEnabled = true;
            attackBtn.IsEnabled = true;
            buildBtn.IsEnabled = true;


            foreach (Card card in eyrie.Hand)
            {
                if (card.CardKey == cardID)
                {
                    // Do something with the clicked card
                    // For example, display its name in a message box
                    
                    break;
                }
            }

            RefreshDecree();
        }

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

        private void recruitBtn_Click(object sender, RoutedEventArgs e)
        {
            moveBtn.IsEnabled = false;
            attackBtn.IsEnabled = false;
            buildBtn.IsEnabled = false;
        }

        private void moveBtn_Click(object sender, RoutedEventArgs e)
        {
            recruitBtn.IsEnabled = false;
            attackBtn.IsEnabled = false;
            buildBtn.IsEnabled = false;
        }

        private void attackBtn_Click(object sender, RoutedEventArgs e)
        {
            recruitBtn.IsEnabled = false;
            moveBtn.IsEnabled = false;
            buildBtn.IsEnabled = false;
        }

        private void buildBtn_Click(object sender, RoutedEventArgs e)
        {
            recruitBtn.IsEnabled = false;
            moveBtn.IsEnabled = false;
            attackBtn.IsEnabled = false;
        }
    }
}
