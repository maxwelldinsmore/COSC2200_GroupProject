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
using RootRemake_Project.Components;
using Microsoft.VisualBasic;

namespace RootRemake_Project
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        #region Properties
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

        private bool IsHandVisible = false;

        public double LocationPolygonViewability = 0;

        public int LocationHighlightViewability = 1;

        public string TurnPhase = "Setup";

        public int StartingPlayersTurn;

        public event EventHandler<int> LocationClicked;

        

        /// <summary>
        /// Keeps track of users actions
        /// </summary>
        public int lastLocationClicked = -1;

        public int[] CornersNotChosen = new int[] {0,3,8,11};

        public bool SidePanelLoaded = false;

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            this.Locations = LocationInfo.MapLocations;
            Players = new Player[2];
            Players[0] = new MarquisDeCat("Bilgan");
            Players[1] = new Eyrie("Mariah");
            cardDeck = CardDeck.cardDeck;
            discardPile = new List<Card>();
            
            // Shuffle and deal initial cards
            ShuffleDeck();
            DealInitialCards();
            cardHand.DisplayHand(Players[0].Hand);
            cardHand.CardClicked += OnCardClicked;
            this.Loaded += GameScreen_Loaded; // Add this line to attach the Loaded event

            // Place initial VP tokens
            foreach (var player in Players)
            {
                PlaceVPToken(player); // This will use each faction's VictoryPointArt
            }

            // Could randomize this later
            StartingPlayersTurn = 0;
            CurrentPlayerTurn = StartingPlayersTurn;
            playerNameTextBlock.Text = Players[CurrentPlayerTurn].UserName;

        }

        /// <summary>
        /// Game Start method that runs after window fully loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameScreen_Loaded(object sender, RoutedEventArgs e)
        {
            //LocationPolygonViewability = 1; // Ensure polygons are visible
            loadLocationHighlights(); // Call the method to load location highlights

            LoadUserControl(TurnPhase, Players[0].CharacterName());


        }

        #region Location Clicking

        protected virtual void OnLocationClicked(int locationId)
        {
            LocationClicked?.Invoke(this, locationId);
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
            //MessageBox.Show("Location Clicked " + source.Name.Split('_')[1]);
            //Updates what location the user clicked
            lastLocationClicked = Int32.Parse(source.Name.Split('_')[1]);
            OnLocationClicked(lastLocationClicked);
        }

        #endregion

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

        public void toggleHandBtn_Click(object sender, RoutedEventArgs e)
        {
            IsHandVisible = !IsHandVisible; // Toggle state

            if (IsHandVisible)
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
        public void UpdateHandDisplay()
        {
                if (IsHandVisible)
                {
                    cardHand.DisplayHand(Players[CurrentPlayerTurn].Hand);
                    // Refresh the display in case cards changed
                    cardHand.Visibility = Visibility.Visible;
                }
                
        }
        

        private void OnCardClicked(string cardIndex)
        {
            var currentPlayer = Players[CurrentPlayerTurn];
            int excessCards = currentPlayer.Hand.Count - 5;

            if (excessCards > 0 && TurnPhase == "Evening")
            {
                // Discard mode
                Card clickedCard = currentPlayer.Hand.Find(c => c.CardKey == cardIndex);
            }
            else
            {
                // Normal card viewing
                Card clickedCard = currentPlayer.Hand.Find(c => c.CardKey == cardIndex);
                
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
                IsHandVisible = true;
                cardHand.Visibility = Visibility.Visible;
                toggleHandBtn.Content = "Hide Hand";
                cardHand.DisplayHand(currentPlayer.Hand);
            }
        }

        #endregion

        #region Victory Points

        private readonly Point[] _scoreTrackPositions = new Point[]
      {
new (17, 740),  // VP 0
           new (43.3, 740),  // VP 1 
           new (69.6, 740),  // VP 2
           new (95.9, 740),  // VP 3
           new (122.2, 740), // VP 4                      
           new (148.5, 740), // VP 5
           new (174.8, 740), // VP 6
           new (201.1, 740), // VP 7 
           new (227.4, 740), // VP 8
           new (253.7, 740), // VP 9
           new (280, 740),   // VP 10                     
           new (306.3, 740), // VP 11
           new (332.6, 740), // VP 12
           new (358.9, 740), // VP 13
           new (385.2, 740), // VP 14
           new (411.5, 740), // VP 15
           new (437.8, 740), // VP 16
           new (464.1, 740), // VP 17
           new (490.4, 740), // VP 18
           new (516.7, 740), // VP 19
           new (543, 740),   // VP 20
           new (569.3, 740), // VP 21
           new (595.6, 740), // VP 22
           new (621.9, 740), // VP 23
           new (648.2, 740), // VP 24
           new (674.5, 740), // VP 25
           new (700.8, 740), // VP 26
           new (727.1, 740), // VP 27
           new (753.4, 740), // VP 28
           new (779.7, 740), // VP 29
           new (806, 740)    // VP 30
     };

        private void PlaceVPToken(Player player)
        {
            // Remove existing token if any
            var oldToken = canvasGameBoard.Children
                .OfType<Image>()
                .FirstOrDefault(img => img.Name == $"VP_{player.UserName}");

            if (oldToken != null)
            {
                canvasGameBoard.Children.Remove(oldToken);
            }

            // Create new token using the player's VictoryPointArt
            Image vpToken = new Image
            {
                Source = new BitmapImage(new Uri(player.VictoryPointArt.ToString(), UriKind.Absolute)), // Correct URI format
                Width = 25,
                Height = 25,
                Name = $"VP_{player.UserName}",
                IsHitTestVisible = false
            };

            // Position on score track
            Canvas.SetLeft(vpToken, _scoreTrackPositions[player.VictoryPoints].X);
            Canvas.SetTop(vpToken, _scoreTrackPositions[player.VictoryPoints].Y);

            canvasGameBoard.Children.Add(vpToken);
        }

        private void CheckForVictory(Player player)
        {
            if (player.VictoryPoints >= 30)
            {
                // Stop game flow
                endTurnBtn.IsEnabled = false;
                toggleHandBtn.IsEnabled = false;

                // Show victory message
                MessageBox.Show(
                    $"{player.UserName} has won with {player.VictoryPoints} Victory Points!",
                    "Game Over",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);

                // Optional: Highlight winning player
                chaBannerImage.Source = new BitmapImage(player.BannerArt);
                playerNameTextBlock.Text = $"{player.UserName} (WINNER)";
                playerNameTextBlock.Foreground = Brushes.Gold;
            }
        }
        #endregion

        #region Location Highlighting Methods

        /// <summary>
        /// On game start loads the locations on the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       


        private void ToggleFactionBoard()
        {
            factionBoardImage.Source = new BitmapImage(Players[CurrentPlayerTurn].BoardArt);
            factionBoardImage.Visibility = factionBoardImage.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        private void viewBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleFactionBoard();
        }
       
            //if (InsideLocation(position, location))
            //{
            //    Debug.WriteLine("Inside Location 0");
            //}
        

      

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
                polygon.Opacity = LocationPolygonViewability;
                polygon.Name = "Polygon_" + location.LocationID;
                polygon.AddHandler(MouseDownEvent, new MouseButtonEventHandler(Location_MouseDown), true);

                // Debug: Log the points being added to the polygon
                Debug.WriteLine($"Location {location.LocationID} Points:");
                foreach (var p in points)
                {
                    Debug.WriteLine($"X: {p.X}, Y: {p.Y}");
                }

                Image image = new Image();
               // TODO: Once we Have all the assets revert This to the original
                if (location.LocationType == "Forest")
                {
                    Uri imageUri = new Uri("pack://application:,,,/Assets/Areas/" + location.LocationID + "_gray.png", UriKind.RelativeOrAbsolute);
                    image.Source = new BitmapImage(imageUri);
                } else
                {
                    Uri imageUri = new Uri("pack://application:,,,/Assets/Areas/" + location.LocationID + ".png", UriKind.RelativeOrAbsolute);
                    image.Source = new BitmapImage(imageUri);
                }

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
        /// Takes in a list and highlights all the 
        /// locations on the map and unhighlights the rest
        /// </summary>
        /// <param name="highlightedAreas">List of Location IDs to be highlighted</param>
        public void HighlightLocations(List<int> highlightedAreas)
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
                    polygon.Opacity = LocationPolygonViewability;
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

            //Point position = e.GetPosition(imgMap);
            //MessageBox.Show(position.X.ToString("F2") + ", " + position.Y.ToString("F2"));

            //Clipboard.SetText(position.X.ToString("F2") + ", " + position.Y.ToString("F2")
            //);
        }
        private void placeWarriorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (var location in Locations)
            {
                AddWarriorToLocation(location.LocationID, 2, CurrentPlayerTurn);
            }
            
           
        
        }

        private void displayPolygonsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (LocationPolygonViewability == 0)
            {
                LocationPolygonViewability = 1;
            }
            else
            {
                LocationPolygonViewability = 0;
            }
            foreach (var polygon in canvasGameBoard.Children.OfType<Polygon>())
            {
                if (polygon.Name.Contains("Polygon_"))
                {
                    polygon.Opacity = LocationPolygonViewability;
                    int currentLocation = Int32.Parse(polygon.Name.Split('_')[1]);
                }
            }
        }
        private void displayHighlightsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (LocationHighlightViewability == 0)
            {
                LocationHighlightViewability = 1;
            }
            else
            {
                LocationHighlightViewability = 0;
            }
            foreach (var image in canvasGameBoard.Children.OfType<Image>())
            {
                if (image.Name.Contains("Highlight_"))
                {
                    image.Opacity = LocationHighlightViewability;
                    int currentLocation = Int32.Parse(image.Name.Split('_')[1]);
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
        /// NOT USED AS OF YET BUT COULD BE USED FOR SUMMARY SCREEN WHEN GAME OVER
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {

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
            // 
            RemoveControlByName(Players[CurrentPlayerTurn].CharacterName(), TurnPhase);
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
                DrawCardsForPlayer(CurrentPlayerTurn, 1);

                // Check for discard before changing turns
                CheckForDiscard();
                Players[CurrentPlayerTurn].VictoryPoints++;
                PlaceVPToken(Players[CurrentPlayerTurn]);

                // Add this victory check
                CheckForVictory(Players[CurrentPlayerTurn]);

                // Only proceed if game isn't over
                if (endTurnBtn.IsEnabled)
                {
                    ChangePlayersTurn();
                    TurnPhase = "Birdsong";
                    turnPhaseImage.Source = new BitmapImage(
                        new Uri("pack://application:,,,/Assets/Birdsong.png", UriKind.RelativeOrAbsolute));
                }
            }
            // TODO: CARLOS ADD YOUR CODE HERE

            // Adds new Side Panel user control
            LoadUserControl(TurnPhase, Players[CurrentPlayerTurn].CharacterName());
        }
        public void ChangePlayersTurn()
        {
            // Removes Side Panel user Control
            RemoveControlByName(Players[CurrentPlayerTurn].CharacterName(), TurnPhase);
            // Hide any visible faction board
            if (factionBoardImage != null && factionBoardImage.Visibility == Visibility.Visible)
            {
                factionBoardImage.Visibility = Visibility.Collapsed;
            }


            // Advance turn
            CurrentPlayerTurn++;
            if (CurrentPlayerTurn >= Players.Length)
            {
                CurrentPlayerTurn = 0;
            }

            // Update turn counter if full round completed
            if (CurrentPlayerTurn == StartingPlayersTurn)
            {
                TurnNumber++;
            }

            // Update UI elements
            playerNameTextBlock.Text = Players[CurrentPlayerTurn].UserName;
            chaBannerImage.Source = new BitmapImage(Players[CurrentPlayerTurn].BannerArt);

            // Refresh victory point tokens (in case of tie situations)
            foreach (var player in Players)
            {
                PlaceVPToken(player);
            }

            // Update hand display if visible
            if (IsHandVisible)
            {
                cardHand.DisplayHand(Players[CurrentPlayerTurn].Hand);
            }
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
                        yShift += 30;
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
            // Checks if there is already warriors in the location
            if (Locations[locationID].ContainsArmy(playerID))
            {
                // If there is already an army in the location, add to it
                Locations[locationID].Armies[0].WarriorCount += WarriorsAdded;
                UpdateWarriorPlacement(locationID);
                return;
            }

            string keyString = helperFunctions.GetRandomString();
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

        /// <summary>
        /// Removes the image and label of the warrior from the map
        /// and then calls UpdateWarriorPlacement to update placements
        /// </summary>
        /// <param name="location"></param>
        /// <param name="deletedArmy"></param>
        public void DeleteWarriorImage(int locationID, Army deletedArmy)
        {
            if (canvasGameBoard is Canvas canvas)
            {
                foreach (var image in canvas.Children.OfType<Image>())
                {
                    if (image.Name.Contains(deletedArmy.KeyString))
                    {
                        canvas.Children.Remove(image);
                    }
                }
                foreach (var labelNum in canvas.Children.OfType<Label>())
                {
                    if (labelNum.Name.Contains(deletedArmy.KeyString))
                    {
                        canvas.Children.Remove(labelNum);
                    }
                }
            }
            // Removes from players army from the location
            Locations[locationID].Armies.Remove(deletedArmy);
            UpdateWarriorPlacement(locationID);
        }

        #endregion


        #region Building Related Methods
        /// <summary>
        /// Updates the information of the warriors on the map
        /// </summary>
        public void UpdateBuildingPlacement(int locationID)
        {
            Location updatedLocation = Locations[locationID];
            if (updatedLocation.TotalBuildings == 1)
            {
                foreach (var image in canvasGameBoard.Children.OfType<Image>())
                {
                    if (image.Name.Contains(updatedLocation.Buildings.First().BuildingKey))
                    {
                        Canvas.SetLeft(image, updatedLocation.Building1Location.Value.X);
                        Canvas.SetTop(image, updatedLocation.Building1Location.Value.Y);
                    }
                }
               
            } else
            {
                foreach (var image in canvasGameBoard.Children.OfType<Image>())
                {
                    if (image.Name.Contains(updatedLocation.Buildings.First().BuildingKey))
                    {
                        Canvas.SetLeft(image, updatedLocation.Building1Location.Value.X);
                        Canvas.SetTop(image, updatedLocation.Building1Location.Value.Y);
                    }
                    if (image.Name.Contains(updatedLocation.Buildings.Last().BuildingKey))
                    {
                        Canvas.SetLeft(image, updatedLocation.Building2Location.Value.X);
                        Canvas.SetTop(image, updatedLocation.Building2Location.Value.Y);
                    }
                }
            }
            
        }

        public void AddBuildingToLocation(int locationID, int playerID, string BuildingType)
        {
            Building addedBuilding = new Building(
                    playerID,
                    BuildingType
            );

            Locations[locationID].Buildings.Add(addedBuilding);
            Image image = new Image();
            image.Source = new BitmapImage(addedBuilding.BuildingArt);
            image.Width = 30;
            image.Height = 30;
            image.Name = addedBuilding.BuildingKey;
            image.IsHitTestVisible = false;

    
            if (canvasGameBoard is Canvas canvas)
            {
                canvas.Children.Add(image);
            }
            UpdateBuildingPlacement(locationID);
        }

        /// <summary>
        /// Removes the image and label of the warrior from the map
        /// and then calls UpdateWarriorPlacement to update placements
        /// </summary>
        /// <param name="location"></param>
        /// <param name="deletedArmy"></param>
        public void DeleteBuildingImage(int locationID, Building deletedBuilding)
        {
            if (canvasGameBoard is Canvas canvas)
            {
                foreach (var image in canvas.Children.OfType<Image>())
                {
                    if (image.Name.Contains(deletedBuilding.BuildingKey))
                    {
                        canvas.Children.Remove(image);
                    }
                }
                
            }
            // Removes from players army from the location
            Locations[locationID].Buildings.Remove(deletedBuilding);
        }

        #endregion

        #region load Side panel character actions 

        public void LoadUserControl(string turnPhase, string characterName)
        {
            if (SidePanelLoaded)
            {
                throw new InvalidOperationException($"User control Already Loaded");
            }
            // Example usage: Load a user control based on character and time of day
            var control = AddControlByName(characterName, turnPhase);

            sidePanelGrid.Children.Add(control);
            Grid.SetRow(control, 3);
            SidePanelLoaded = true;

        }
        public static UserControl? AddControlByName(string character, string turnPhase)
        {
            string className = $"{character}{turnPhase}";
            string fullTypeName = $"RootRemake_Project.Components.{className}"; // Replace with your actual namespace

            var type = Type.GetType(fullTypeName);
            if (type != null && typeof(UserControl).IsAssignableFrom(type))
            {
                var control = (UserControl?)Activator.CreateInstance(type);
               

                return control;

            }

            throw new InvalidOperationException($"UserControl class '{fullTypeName}' not found.");
        }

        public void RemoveControlByName(string character, string turnPhase)
        {
            try
            {
                sidePanelGrid.Children.Remove(
                    sidePanelGrid.Children
                    .OfType<UserControl>()
                    .FirstOrDefault(c => c.GetType().Name == $"{character}{turnPhase}")
                );
                SidePanelLoaded = false;
            } catch
            {
                Debug.WriteLine($"Error removing control");
            }

        }

        #endregion

    

    }
}