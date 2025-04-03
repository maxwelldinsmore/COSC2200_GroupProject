using RootRemake_Project.CharacterClasses;
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
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace RootRemake_Project
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        public Random random = new Random();
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
        /// Overall turn number for the game,
        /// 0 is setup turn
        /// </summary>
        public int TurnNumber = 0;

        /// <summary>
        /// Viewability of the location polygons on the map
        /// 1 is fully visible, 0 is invisible
        /// Don't need to be shown only for debugging / resizing collision boxes
        /// </summary>
        public double locationPolygonViewability = 0;

        public string TurnPhase = "Setup";

        public int StartingPlayersTurn;

        public GameScreen()
        {
            InitializeComponent();
            this.Locations = LocationInfo.MapLocations;
            Players = new Player[5];
            Players[0] = new MarquisDeCat("Bilgan");
            Players[1] = new MarquisDeCat("Mariah");
            Players[2] = new Eyrie("Shane");
            Players[3] = new MarquisDeCat("Max");
            Players[4] = new MarquisDeCat("Carlos");
            cardDeck = CardDeck.cardDeck;
            discardPile = new List<Card>();
            
            // Shuffle and deal initial cards
            ShuffleDeck();
            DealInitialCards();
            cardHand.DisplayHand(Players[0].Hand);
            cardHand.CardClicked += OnCardClicked;
            this.Loaded += GameScreen_Loaded; // Add this line to attach the Loaded event

            // Could randomize this later
            StartingPlayersTurn = 0;
            CurrentPlayerTurn = StartingPlayersTurn;
            playerNameTextBlock.Text = Players[CurrentPlayerTurn].UserName;
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
        }



        private void GameScreen_Loaded(object sender, RoutedEventArgs e)
        {
            //locationPolygonViewability = 1; // Ensure polygons are visible
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
            if (player.Hand.Count > 5)
            {
                // For now, we'll just discard down to 5 randomly
                // In a real game, you'd want the player to choose which cards to discard
                while (player.Hand.Count > 5)
                {
                    Card cardToDiscard = player.Hand[0]; // Always discards first card - should be changed to player choice
                    player.DiscardCard(cardToDiscard, discardPile);
                }
            }
        }

        private bool isHandVisible = false;

        private void toggleHandBtn_Click(object sender, RoutedEventArgs e)
        {
            isHandVisible = !isHandVisible; // Toggle state

            if (isHandVisible)
            {
                // Show the hand
                cardHand.Visibility = Visibility.Visible;
                toggleHandBtn.Content = "Hide Hand";

                // Refresh the display in case cards changed
                cardHand.DisplayHand(Players[CurrentPlayerTurn].Hand);
            }
            else
            {
                // Hide the hand
                cardHand.Visibility = Visibility.Collapsed;
                toggleHandBtn.Content = "Show Hand";
            }
        }

        // Call this whenever the hand changes to keep it updated
        private void UpdateHandDisplay()
        {
            if (isHandVisible)
            {
                cardHand.DisplayHand(Players[CurrentPlayerTurn].Hand);
            }
        }

        private void OnCardClicked(int cardIndex)
        {
            // Get the clicked card from current player's hand
            Card clickedCard = Players[CurrentPlayerTurn].Hand[cardIndex];

            MessageBox.Show($"Card {cardIndex + 1} clicked\n" +
                           $"{clickedCard.CardText}\n" +
                           $"Suit: {GetSuitName(clickedCard.Suit)}",
                           "Card Selected",
                           MessageBoxButton.OK,
                           MessageBoxImage.Information);
        }

        private string GetSuitName(int suit)
        {
            return suit switch
            {
                1 => "Bird (Wild)",
                2 => "Fox",
                3 => "Rabbit",
                4 => "Mouse",
                _ => "Unknown"
            };
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
            card = Players[0].Hand[0];
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


        private void placeWarriorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            image.Source = new BitmapImage(Players[0].WarriorArt);
            image.Width = 20;
            image.Height = 20;
            image.Name = "Warrior_0";
            image.IsHitTestVisible = false;
            Point buildingPoint = Locations[0].WarriorLocation;
            Canvas.SetLeft(image, buildingPoint.X);
            Canvas.SetTop(image, buildingPoint.Y);
           
            if (canvasGameBoard is Canvas canvas)
            {
                canvas.Children.Add(image);
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// General on click event for location being clicked
        /// will need to be cleaned up to be used for specific events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Location_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Polygon source = (Polygon)sender;
            MessageBox.Show("Location Clicked " + source.Name.Split('_')[1]);

        }

        /// <summary>
        /// NOT USED AS OF YET BUT COULD BE USED FOR SUMMARY SCREEN WHEN GAME OVER
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Implements a test use for highlight locations
        /// highlighting the corner locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseKeepMenuItem_Click(object sender, RoutedEventArgs e)
        {
            int[] cornerLocations = [0, 3, 8, 11];
            HighlightLocations(cornerLocations);
        }


        /// <summary>
        /// Highlights all the locations
        /// </summary>
        private void highlightMenuItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (var image in  canvasGameBoard.Children.OfType<Image>())
            {
                image.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Button for when the turn is ended
        /// </summary>
        private void endPhaseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TurnPhase == "Setup") // Setup goes once for each player
            {
                ChangePlayersTurn();
                if (CurrentPlayerTurn == StartingPlayersTurn)
                {
                    TurnPhase = "Birdsong";
                    turnPhaseImage.Stretch = Stretch.UniformToFill;
                    turnPhaseImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Birdsong.png", UriKind.RelativeOrAbsolute));
                }
            } else if (TurnPhase == "Birdsong")
            {
                TurnPhase = "Daylight";
                turnPhaseImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Daylight.png", UriKind.RelativeOrAbsolute));

            }
            else if (TurnPhase == "Daylight")
            {
                TurnPhase = "Evening";
                turnPhaseImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Evening.png", UriKind.RelativeOrAbsolute));

            }
            else if (TurnPhase == "Evening")
            {
                // Next Players turn and set to 
                ChangePlayersTurn();
                TurnPhase = "Birdsong";
                turnPhaseImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Birdsong.png", UriKind.RelativeOrAbsolute));
            }

        }
        public void ChangePlayersTurn()
        {

            CurrentPlayerTurn++;
            if (CurrentPlayerTurn >= Players.Length)
            {
                CurrentPlayerTurn = 0;
            }
            if (CurrentPlayerTurn == StartingPlayersTurn)
            {
                TurnNumber++;
            }
            playerNameTextBlock.Text = Players[CurrentPlayerTurn].UserName;
            chaBannerImage.Source = new BitmapImage(Players[CurrentPlayerTurn].BannerArt);
        }


        private void loadBuildingMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Image BuildingImage = new Image();
            MarquisDeCat marquis = (MarquisDeCat)Players[0];
            BuildingImage.Source = new BitmapImage(marquis.RecruiterArt);
            BuildingImage.Width = 25;
            BuildingImage.Height = 25;
            BuildingImage.Name = "Recruiter";

            BuildingImage.IsHitTestVisible = false;
            if (Locations[0].Building1Location.HasValue)
            {
                Point buildingPoint = Locations[0].Building1Location.Value;
                Canvas.SetLeft(BuildingImage, buildingPoint.X);
                Canvas.SetTop(BuildingImage, buildingPoint.Y);
            }
            canvasGameBoard.Children.Add(BuildingImage);

        }

        
        /// <summary>
        /// For later loads the setup character methods
        /// and assigns random player order
        /// </summary>
        private void GameStart()
        {
            // highlights the 4 corners on the map
        }

        /// <summary>
        /// Takes in an array and highlights all the 
        /// locations on the map and unhighlights the rest
        /// </summary>
        /// <param name="highlightedAreas">Array of Locations ID's to be highlighted</param>
        private void HighlightLocations(int[] highlightedAreas)
        {
            // Hide locations that are not adjacent to the current location
            foreach (var image in canvasGameBoard.Children.OfType<Image>())
            {
                if (image.Name.Contains("Highlight_"))
                {
                    image.Visibility = Visibility.Hidden;
                    int currentLocation = Int32.Parse(image.Name.Split('_')[1]);
                    if (highlightedAreas.Contains(currentLocation))
                    {
                        image.Visibility = Visibility.Visible;
                    } else
                    {
                        image.Visibility = Visibility.Hidden;
                    }
                }
            }
            foreach (var polygon in canvasGameBoard.Children.OfType<Polygon>())
            {
                if (polygon.Name.Contains("Polygon_"))
                {
                    polygon.Visibility = Visibility.Hidden;
                    int currentLocation = Int32.Parse(polygon.Name.Split('_')[1]);
                    if (highlightedAreas.Contains(currentLocation))
                    {
                        polygon.Visibility = Visibility.Visible;
                    } else
                    {
                        polygon.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
    }
}
//Image BuildingImage = new Image();
//MarquisDeCat marquis = (MarquisDeCat)Players[building.playerID].Character;
//BuildingImage.Source = new BitmapImage(marquis.RecruiterArt);
//BuildingImage.Width = 50;
//BuildingImage.Height = 50;
//BuildingImage.Name = "Recruiter_" + building.BuildingID;

//// Assuming BuildingPoints is a property in Location that gives the position of buildings
//double[] buildingPoint = location.LocationPolygon[building.BuildingID];
//Canvas.SetLeft(BuildingImage, buildingPoint[0]);
//Canvas.SetTop(BuildingImage, buildingPoint[1]);

//// Assuming canvasGameBoard is a Canvas or similar container
//if (canvasGameBoard is Canvas canvas)
//{
//    canvas.Children.Add(BuildingImage);
//}

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