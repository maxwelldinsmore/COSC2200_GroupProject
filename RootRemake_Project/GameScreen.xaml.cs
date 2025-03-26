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

        /// <summary>
        /// Array of Players in the game
        /// </summary>
        public Player[] Players;
        /// <summary>
        /// Array of cards in the card deck
        /// that are dealt out to the Players
        /// </summary>
        public List<Card> cardDeck;
        /// <summary>
        /// Array of cards that are discarded
        /// </summary>
        public List<Card> discardPile;
        /// <summary>
        /// turn for the current player based off position
        /// in the Players array
        /// </summary>
        public int CurrentPlayerTurn;
        /// <summary>
        /// Overall turn number for the game
        /// </summary>
        public int TurnNumber = 1;

        /// <summary>
        /// Viewability of the location polygons on the map
        /// 1 is fully visible, 0 is invisible
        /// </summary>
        public double locationPolygonViewability = .8;

        public GameScreen()
        {
            InitializeComponent();
            this.Locations = LocationInfo.MapLocations;
            //testCardLoad();
            Players = new Player[4];
            Players[0] = new Player("Carlos");
            cardDeck = CardDeck.cardDeck;
            discardPile = new List<Card>();

        }




        private void imgMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(imgMap);
            MessageBox.Show(position.X.ToString("F4") + ", " + position.Y.ToString("F4"));
          
            Clipboard.SetText(position.X.ToString("F4") + ", " + position.Y.ToString("F4")
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


            //if (InsideLocation(position, location))
            //{
            //    Debug.WriteLine("Inside Location 0");
            //}
        }
     

        private void testCardLoad()
        {
            Card card = CardDeck.cardDeck[25];
            BitmapSource cardImage = card.GetCardImage();

            canvasGameBoard.Children.Add(
                new Image
                {
                    Source = cardImage,
                    Height = 250,
                    Margin = new Thickness(29, 257, 50, 256)
                }
                );
        }

       


 void HighlightLocation()
        {
            foreach (var location in Locations)
            {
                Polygon polygon = new Polygon();
                PointCollection points = new PointCollection();

                // Convert LocationPolygon to PointCollection
                foreach (var point in location.LocationPolygon)
                {
                    points.Add(new Point(point[0], point[1]));
                }

                polygon.Points = points;
                polygon.Fill = Brushes.Red;
                polygon.Opacity = locationPolygonViewability;
                polygon.Name = "Location_" + location.LocationID;
                polygon.AddHandler(MouseDownEvent, new MouseButtonEventHandler(Location_MouseDown), true);

                // Debug: Log the points being added to the polygon
                Debug.WriteLine($"Location {location.LocationID} Points:");
                foreach (var p in points)
                {
                    Debug.WriteLine($"X: {p.X}, Y: {p.Y}");
                }

                // Assuming canvasGameBoard is a Canvas or similar container
                if (canvasGameBoard is Canvas canvas)
                {
                    canvas.Children.Add(polygon);
                }
            }
        }




      

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Location_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Location Clicked");
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void resizeMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }


        private void highlightMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            HighlightLocation();
        }

        private void endTurnBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


//     /// <summary>
//     /// Hit detection for locations on the map
//     /// CURRENTLY UNUSED: polygon on click event has replaced it,
//     /// might be important if polygon needs to be replaced with another asset
//     /// </summary>
//     /// <param name="mouse">point for the position of player mouse</param>
//     /// <param name="locationPolygon">location being checked for collision</param>
//     /// <returns></returns>
//        public bool InsideLocation(Point mouse, double[][] locationPolygon)
//{
//    // First gets inner polygon line used in point in polygon method
//    double x1 = 0;
//    double x2 = 0;
//    double y1 = 0;
//    double y2 = 99999999;
//    foreach (double[] location in locationPolygon)
//    {
//        if (location[1] < y1)
//        {
//            y1 = location[1];
//            x1 = location[0];
//        }
//        if (location[1] > y2)
//        {
//            y2 = location[1];
//            x2 = location[0];
//        }
//    }

//    // Ray Casting method 
//    int intersectionCount = 0;
//    // IDK IF THIS WORKS TOTALLY NOT AI
//    for (int i = 0; i < locationPolygon.Length; i++)
//    {
//        double x1p = locationPolygon[i][0];
//        double y1p = locationPolygon[i][1];
//        double x2p = locationPolygon[(i + 1) % locationPolygon.Length][0];
//        double y2p = locationPolygon[(i + 1) % locationPolygon.Length][1];
//        if (y1p == y2p)
//        {
//            continue;
//        }
//        if (mouse.Y < y1p && mouse.Y < y2p)
//        {
//            continue;
//        }
//        if (mouse.Y >= y1p && mouse.Y >= y2p)
//        {
//            continue;
//        }
//        double x = (mouse.Y - y1p) * (x2p - x1p) / (y2p - y1p) + x1p;
//        if (x > mouse.X)
//        {
//            intersectionCount++;
//        }
//    }
//    // If even return false, if odd return true
//    return intersectionCount % 2 == 1;
//}

///// <summary>
///// Sets the locations boundaries from percentage
///// to real values for the game board
///// Only Called when gamescreen is loaded so far.
///// NOT TIED TO AN EVENT
///// </summary>
//async public void OnResize()
//{

//    double imgWidth = imgMap.ActualWidth;
//    double imgHeight = imgMap.ActualHeight;


//    foreach (var location in Locations)
//    {
//        for (int i = 0; i < location.LocationPolygonPercents.Length; i++)
//        {
//            location.LocationPolygon[i][0] = location.LocationPolygonPercents[i][0] * imgWidth / 100;
//            location.LocationPolygon[i][1] = location.LocationPolygonPercents[i][1] * imgHeight / 100;
//            Debug.WriteLine("Location " + location.LocationID + " X: " + location.LocationPolygon[i][0] + " Y: " + location.LocationPolygon[i][1]);
//        }
//    }
//}