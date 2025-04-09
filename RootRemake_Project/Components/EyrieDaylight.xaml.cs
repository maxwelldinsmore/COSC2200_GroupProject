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
        private bool turmoilOccured;
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
                parentWindow.endTurnBtn.IsEnabled = false;
                playerID = parentWindow.CurrentPlayerTurn;

                eyrie = (Eyrie)parentWindow.Players[playerID];
            }
            RefreshDecree();
            // Loads in icons for the eyrie Events
        }
        private void ParentWindow_LocationClicked(object sender, int locationId)
        {

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

            if (eyrie.attackDecree.Count > 0)
            {
                
            }
           

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
    }
}
