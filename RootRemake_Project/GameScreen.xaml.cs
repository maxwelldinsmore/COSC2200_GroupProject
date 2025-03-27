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
        /// Don't need to be shown only for debugging / resizing collision boxes
        /// </summary>
        public double locationPolygonViewability = 0;



        public GameScreen()
        {
            InitializeComponent();
            this.Locations = LocationInfo.MapLocations;
            Players = new Player[4];
            Players[0] = new Player("Max");
            Players[1] = new Player("Mariah");
            Players[2] = new Player("Shane");
            Players[3] = new Player("Bilgan");
            cardDeck = CardDeck.cardDeck;
            discardPile = new List<Card>();
            // Shuffle and deal initial cards
            ShuffleDeck();
            DealInitialCards();
            // Temporary verification code
            for (int i = 0; i < Players.Length; i++)
            {
                Debug.WriteLine($"Player {i + 1} has {Players[i].hand.Count} cards");
                foreach (Card card in Players[i].hand)
                {
                    Debug.WriteLine($"- {card.CardText}");
                }
            }
        }
            // Images don't properly load here so need to be pushed to the loaded event
            this.Loaded += GameScreen_Loaded; 
        }

        private void ShuffleDeck()
        {
            Random rng = new Random();
            int n = cardDeck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cardDeck[k];
                cardDeck[k] = cardDeck[n];
                cardDeck[n] = value;
            }
        }

        private void DealInitialCards()
        {
            foreach (Player player in Players)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (cardDeck.Count > 0)
                    {
                        player.DrawCard(cardDeck);
                    }
                }
            }
        private void GameScreen_Loaded(object sender, RoutedEventArgs e)
        {
            loadLocationHighlights(); // Call the method to load location highlights
        }

        public void DrawCardsForPlayer(int playerIndex, int numberOfCards)
        {
            if (playerIndex < 0 || playerIndex >= Players.Length)
                return;

            Player player = Players[playerIndex];
            for (int i = 0; i < numberOfCards; i++)
            {
                if (cardDeck.Count > 0)
                {
                    player.DrawCard(cardDeck);
                }
                else
                {
                    // If deck is empty, reshuffle discard pile into deck
                    if (discardPile.Count > 0)
                    {
                        cardDeck.AddRange(discardPile);
                        discardPile.Clear();
                        ShuffleDeck();
                        player.DrawCard(cardDeck);
                    }
                    else
                    {
                        // No cards left to draw
                        break;
                    }
                }
            }

            // Check if player needs to discard
            if (player.hand.Count > 5)
            {
                // For now, we'll just discard down to 5 randomly
                // In a real game, you'd want the player to choose which cards to discard
                while (player.hand.Count > 5)
                {
                    Card cardToDiscard = player.hand[0]; // Always discards first card - should be changed to player choice
                    player.DiscardCard(cardToDiscard, discardPile);
                }
            }
        }



        private void imgMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
         
            Point position = e.GetPosition(imgMap);
            MessageBox.Show(position.X.ToString("F2") + ", " + position.Y.ToString("F2"));
          
            Clipboard.SetText(position.X.ToString("F2") + ", " + position.Y.ToString("F2")
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

       


        public void loadLocationHighlights()
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
                polygon.Name = "Polygon_" + location.LocationID;
                polygon.AddHandler(MouseDownEvent, new MouseButtonEventHandler(Location_MouseDown), true);

                // Debug: Log the points being added to the polygon
                Debug.WriteLine($"Location {location.LocationID} Points:");
                foreach (var p in points)
                {
                    Debug.WriteLine($"X: {p.X}, Y: {p.Y}");
                }

                Image image = new Image();
                Uri imageUri = new Uri("pack://application:,,,/Assets/Areas/" + location.LocationHighlight + ".png", UriKind.RelativeOrAbsolute );


                image.Source = new BitmapImage(imageUri);
                image.Width = imgMap.ActualWidth;
                image.Height = imgMap.ActualHeight;
                image.Opacity = 1;
                image.Name = "Highlight_" + location.LocationID;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Top;
                image.Visibility = Visibility.Visible;
                image.IsHitTestVisible = false;

                // Assuming canvasGameBoard is a Canvas or similar container
                if (canvasGameBoard is Canvas canvas)
                {
                    canvas.Children.Add(polygon);
                    canvas.Children.Add(image);
                }
                
            }
        }



        private void hideLocationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            int locationAdjacent = 0;

            // Hide locations that are not adjacent to the current location
            foreach (var image in canvasGameBoard.Children.OfType<Image>())
            {
                if (image.Name.Contains("Highlight_"))
                {
                    image.Visibility = Visibility.Hidden;
                    int currentLocation = Int32.Parse(image.Name.Split('_')[1]);
                    if (Locations[currentLocation].ConnectedLocations.Contains(locationAdjacent))
                    {
                        image.Visibility = Visibility.Visible;
                    }
                    Console.WriteLine("Current Location: " + currentLocation);

                }
            }
            foreach (var polygon in canvasGameBoard.Children.OfType<Polygon>())
            {
                if (polygon.Name.Contains("Polygon_"))
                {
                    polygon.Visibility = Visibility.Hidden;
                    int currentLocation = Int32.Parse(polygon.Name.Split('_')[1]);
                    if (Locations[currentLocation].ConnectedLocations.Contains(locationAdjacent))
                    {
                        polygon.Visibility = Visibility.Visible;
                    }
                    Console.WriteLine("Current Location: " + currentLocation);

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
            foreach (var image in  canvasGameBoard.Children.OfType<Image>())
            {
                image.Visibility = Visibility.Visible;
            }
        }

        private void endTurnBtn_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}


//#if DEBUG
//MessageBox.Show("App is running in Debug mode!");
//#endif

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