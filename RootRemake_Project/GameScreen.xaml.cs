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

        private bool isHandVisible = false;

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

        private void GameScreen_Loaded(object sender, RoutedEventArgs e)
        {
            //locationPolygonViewability = 1; // Ensure polygons are visible
            loadLocationHighlights(); // Call the method to load location highlights
        }

        #region Card Related Methods
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
        }

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
            var currentPlayer = Players[CurrentPlayerTurn];
            int excessCards = currentPlayer.Hand.Count - 5;

            if (excessCards > 0 && TurnPhase == "Evening")
            {
                // Discard mode
                Card clickedCard = currentPlayer.Hand[cardIndex];

                var result = MessageBox.Show($"Discard this card?\n{clickedCard.CardText}",
                                            "Confirm Discard",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    currentPlayer.DiscardCard(clickedCard, discardPile);
                    UpdateHandDisplay();


                }
            }
            else
            {
                // Normal card viewing
                Card clickedCard = currentPlayer.Hand[cardIndex];
                MessageBox.Show($"Card {cardIndex + 1} clicked\n" +
                               $"{clickedCard.CardText}\n" +
                               $"Suit: {GetSuitName(clickedCard.Suit)}",
                               "Card Selected",
                               MessageBoxButton.OK,
                               MessageBoxImage.Information);
            }
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

        private void CheckForDiscard()
        {
            var currentPlayer = Players[CurrentPlayerTurn];
            int excessCards = currentPlayer.Hand.Count - 5;

            if (excessCards > 0)
            {
                // Show discard notification
                string message = $"{currentPlayer.UserName} has too many cards!\n" +
                               $"You have {currentPlayer.Hand.Count} cards (max 5).\n" +
                               $"Please discard {excessCards} card(s).\n\n" +
                               "Click OK then select cards to discard from your hand.";

                MessageBox.Show(message, "Too Many Cards", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                // Force hand to be visible
                isHandVisible = true;
                cardHand.Visibility = Visibility.Visible;
                toggleHandBtn.Content = "Hide Hand";
                cardHand.DisplayHand(currentPlayer.Hand);
            }
        }

        #endregion



        #region Location Highlighting Methods

        /// <summary>
        /// On game start loads the locations on the map
        /// </summary>
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
                Uri imageUri = new Uri("pack://application:,,,/Assets/Areas/" + location.LocationID + ".png", UriKind.RelativeOrAbsolute );

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
                    }
                    else
                    {
                        image.Visibility = Visibility.Hidden;
                    }
                }
            }
            foreach (var polygon in canvasGameBoard.Children.OfType<Polygon>())
            {
                if (polygon.Name.Contains("Polygon_"))
                {
                    polygon.Visibility = Visibility.Visible;
                    polygon.Opacity = locationPolygonViewability;
                    int currentLocation = Int32.Parse(polygon.Name.Split('_')[1]);
                    if (highlightedAreas.Contains(currentLocation))
                    {
                        polygon.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        polygon.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
        #endregion


        #region Debug on Click methods

        private void imgMap_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Point position = e.GetPosition(imgMap);
            MessageBox.Show(position.X.ToString("F2") + ", " + position.Y.ToString("F2"));

            Clipboard.SetText(position.X.ToString("F2") + ", " + position.Y.ToString("F2")
            );
        }
        private void placeWarriorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddWarriorToLocation(0, 2, CurrentPlayerTurn);
           
        
        }

        private void displayPolygonsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            locationPolygonViewability = locationPolygonViewability + 1 % 2;
            foreach (var polygon in canvasGameBoard.Children.OfType<Polygon>())
            {
                if (polygon.Name.Contains("Polygon_"))
                {
                    polygon.Opacity = locationPolygonViewability;
                    int currentLocation = Int32.Parse(polygon.Name.Split('_')[1]);
                }
            }
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
                Point buildingPoint = Locations[6].Building2Location.Value;
                Canvas.SetLeft(BuildingImage, buildingPoint.X);
                Canvas.SetTop(BuildingImage, buildingPoint.Y);
            }
            canvasGameBoard.Children.Add(BuildingImage);

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
        #endregion


        #region Player Turn Related
        /// <summary>
        /// Button for when the turn is ended
        /// </summary>
        private void endPhaseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TurnPhase == "Setup")
            {
                ChangePlayersTurn();
                if (CurrentPlayerTurn == StartingPlayersTurn)
                {
                    TurnPhase = "Birdsong";
                    turnPhaseImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Birdsong.png", UriKind.RelativeOrAbsolute));
                }
            }
            else if (TurnPhase == "Birdsong")
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
                // Check for discard before changing turns
                CheckForDiscard();

                // Only proceed if player doesn't need to discard
                if (Players[CurrentPlayerTurn].Hand.Count <= 5)
                {
                    ChangePlayersTurn();
                    TurnPhase = "Birdsong";
                    turnPhaseImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Birdsong.png", UriKind.RelativeOrAbsolute));
                }
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

        #endregion



        #region Warrior Related Methods
        /// <summary>
        /// Updates the information of the warriors on the map
        /// </summary>
        public void UpdateWarriorPlacement(int locationID)
        {
            int yShift = 0;
            foreach (var Army in Locations[locationID].Armies)
            {
                foreach (var image in canvasGameBoard.Children.OfType<Image>())
                {
                    if (image.Name.Contains("Army_" + Army.KeyString))
                    {
                        Canvas.SetLeft(image, Locations[locationID].WarriorLocation.X);
                        Canvas.SetTop(image, Locations[locationID].WarriorLocation.Y + yShift);
                       
                    }
                }
                foreach (var labelNum in canvasGameBoard.Children.OfType<Label>())
                {
                    if (labelNum.Name.Contains("Label_" + Army.KeyString))
                    {
                        Canvas.SetLeft(labelNum, Locations[locationID].WarriorLocation.X  + 20);
                        Canvas.SetTop(labelNum, Locations[locationID].WarriorLocation.Y + yShift);
                        labelNum.Content = Army.WarriorCount.ToString();
                        yShift += 40;
                    }
                }

            }
        }

        /// <summary>
        /// Adds a image and name for the warrior and the number of warriors 
        /// to the map then calls UpdateWarriorPlacement to update placements
        /// </summary>
        /// <param name="locationID"></param>
        public void AddWarriorToLocation(int locationID, int WarriorsAdded, int playerID)
        {
            string keyString = GetRandomString();
            Locations[locationID].Armies.Add(
                new Army(
                    playerID,
                    Players[CurrentPlayerTurn].WarriorArt,
                    WarriorsAdded,
                    keyString
            ));
            Image image = new Image();
            image.Source = new BitmapImage(Players[CurrentPlayerTurn].WarriorArt);
            image.Width = 30;
            image.Height = 30;
            image.Name = "Army_" + keyString;
            image.IsHitTestVisible = false;

            Label label = new Label();
            label.Content = WarriorsAdded.ToString();
            label.Width = 40;
            label.Height = 40;
            label.Name = "Label_" + keyString;
            label.FontSize = 20;
            label.Foreground = Brushes.White;
            if (canvasGameBoard is Canvas canvas)
            {
                canvas.Children.Add(image);
                canvas.Children.Add(label);
            }
            UpdateWarriorPlacement(locationID);
        }
        #endregion


        /// <summary>
        /// For later loads the setup character methods
        /// and assigns random player order
        /// </summary>
        private void GameStart()
        {
            // highlights the 4 corners on the map
        }

        /// <summary>
        /// Gets string of 10 random characters, used for adding in images and labels
        /// to the game board
        /// </summary>
        public static string GetRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
