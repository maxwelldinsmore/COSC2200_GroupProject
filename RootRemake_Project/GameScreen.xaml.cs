using RootRemake_Project.LocationClasses;
using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace RootRemake_Project
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        public LocationInfo locationInfo;
        public Player[] players;
        
        public GameScreen()
        {
            InitializeComponent();
            this.players = players;
            this.locationInfo = new LocationInfo();
            testCardLoad();
        }

        private void imgMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(imgMap);
            MessageBox.Show("X(%): " +
               (position.X / imgMap.ActualWidth * 100).ToString("F4") +
               " Y(%): " + (position.Y / imgMap.ActualHeight * 100).ToString("F4")
            );
        }

        /// <summary>
        /// Collision detection for locations on the map
        /// Temporaryily will be square locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgMap_MouseMove(object sender, MouseEventArgs e)
        {
            double imgWidth = imgMap.ActualWidth;
            double imgHeight = imgMap.ActualHeight;
            Point position = e.GetPosition(imgMap);
            Debug.WriteLine($"Img Size: {imgWidth},{imgHeight}");
            this.locationInfo = new LocationInfo();
            double[][][] locationSquares = locationInfo.locationSqaures;
            double[][] location = locationSquares[0];

            //for (int i = 0; i < location.Length; i++)
            //{
            //    for (int j = 0; j < location[i].Length; j++)
            //    {
            //        try
            //        {
            //            if (j % 2 == 0)
            //            {
            //                location[i][j] = location[i][j] * imgHeight;
            //            }
            //            else
            //            {
            //                location[i][j] = location[i][j] * imgWidth;
            //            }
            //        }
            //        catch (OverflowException ex)
            //        {
            //            // Handle the exception, e.g., log the error
            //            Console.WriteLine($"Overflow at location[{i}][{j}]: {location[i][j]}");
            //        }
            //    }
            //}

            location[0][0] *= imgWidth / 100;
            location[0][1] *= imgHeight / 100;

            location[1][0] *= imgWidth / 100;
            location[1][1] *= imgHeight / 100;

            location[2][0] *= imgWidth / 100;
            location[2][1] *= imgHeight / 100;

            location[3][0] *= imgWidth / 100;
            location[3][1] *= imgHeight / 100;
            Debug.WriteLine($"Location 0 bottom right: {location[2][0]},{location[2][1]}");

            isWithinBounds((double)position.X, (double)position.Y, location);
        }
        private Boolean isWithinBounds(double x, double y, double[][] location)
        {
            if (x >= location[0][0] && x <= location[2][0] && y >= location[0][1] && y <= location[2][1])
            {
                MessageBox.Show("Mouse is within bounds of location 0");
                return true;
            
            }
            //Debug.WriteLine($"Mouse X: {x} Mouse Y: {y}\nLocation 0 bottom right: {location[2][0]},{location[2][1]}");

            return false;

        }

        private void testCardLoad()
        {
            Card card = CardDeck.cardDeck[9];
            BitmapSource cardImage = card.GetCardImage();
            CardImage.Source = cardImage;
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        
    }
}
