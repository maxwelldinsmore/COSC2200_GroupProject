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
        /// <summary>
        /// Array of locations
        /// </summary>
        public Location[] Locations;

        public Player[] players;
        public List<Card> cardDeck;
        public List<Card> discardPile;
        public int CurrentPlayerTurn;
        public int TurnNumber;


        public GameScreen()
        {
            InitializeComponent();
            this.Locations = LocationInfo.MapLocations;
            OnResize();
            testCardLoad();
            players = new Player[4];
            players[0] = new Player("Carlos");
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
            //Debug.WriteLine("Mouse Move");
            double imgWidth = imgMap.ActualWidth;
            double imgHeight = imgMap.ActualHeight;
            Point position = e.GetPosition(imgMap);
            double[][] location = Locations[0].LocationPolygon;


            if (InsideLocation(position, location))
            {
                Debug.WriteLine("Inside Location 0");
            }
        }
        

        private void testCardLoad()
        {
            Card card = CardDeck.cardDeck[1];
            BitmapSource cardImage = card.GetCardImage();
            CardImage.Source = cardImage;
        }

        /// <summary>
        /// Hit detection for locations on the map
        /// </summary>
        /// <param name="mouse">point for the position of player mouse</param>
        /// <param name="locationPolygon">location being checked for collision</param>
        /// <returns></returns>
        public bool InsideLocation(Point mouse, double[][] locationPolygon)
        {
            // First gets inner polygon line used in point in polygon method
            double x1 = 0;
            double x2 = 0;
            double y1 = 0;
            double y2 = 99999999;
            foreach (double[] location in locationPolygon)
            {
                if (location[1] < y1)
                {
                    y1 = location[1];
                    x1 = location[0];
                }
                if (location[1] > y2)
                {
                    y2 = location[1];
                    x2 = location[0];
                }
            }

            // Ray Casting method 
            int intersectionCount = 0;
            // IDK IF THIS WORKS TOTALLY NOT AI
            for (int i = 0; i < locationPolygon.Length; i++)
            {
                double x1p = locationPolygon[i][0];
                double y1p = locationPolygon[i][1];
                double x2p = locationPolygon[(i + 1) % locationPolygon.Length][0];
                double y2p = locationPolygon[(i + 1) % locationPolygon.Length][1];
                if (y1p == y2p)
                {
                    continue;
                }
                if (mouse.Y < y1p && mouse.Y < y2p)
                {
                    continue;
                }
                if (mouse.Y >= y1p && mouse.Y >= y2p)
                {
                    continue;
                }
                double x = (mouse.Y - y1p) * (x2p - x1p) / (y2p - y1p) + x1p;
                if (x > mouse.X)
                {
                    intersectionCount++;
                }
            }
            // If even return false, if odd return true
            return intersectionCount % 2 == 1;
        }


        /// <summary>
        /// Sets the locations boundaries from percentage
        /// to real values for the game board
        /// Only Called when gamescreen is loaded so far.
        /// NOT TIED TO AN EVENT
        /// </summary>
        public void OnResize()
        {

            double imgWidth = imgMap.ActualWidth;
            double imgHeight = imgMap.ActualHeight;

            foreach (var location in Locations)
            {
                for (int i = 0; i < location.LocationPolygonPercents.Length; i++)
                {
                    location.LocationPolygon[i][0] = location.LocationPolygonPercents[i][0] * imgWidth / 100;
                    location.LocationPolygon[i][1] = location.LocationPolygonPercents[i][1] * imgHeight / 100;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        
    }
}
