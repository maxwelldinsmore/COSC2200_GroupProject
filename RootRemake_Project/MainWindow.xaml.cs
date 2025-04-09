using RootRemake_Project.CharacterClasses;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;




namespace RootRemake_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            /// If you click run with debugging the debugger is attached code 
            /// will run, i.e. straight into the game screen or allowing
            /// character selection to go first.
            //if (Debugger.IsAttached)
            //{
            //}
                Player[] players = new Player[4];

                GameScreen gameScreen = new GameScreen();
                gameScreen.Closed += (sender, e) => this.Show();
                this.Hide();
                gameScreen.Show();
            

        }

        private Button lastSelectedButton = null; // Store the last selected character

        private void CharacterSelected(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            
            if (selectedButton == null) return;

            StackPanel characterSelection = selectedButton.Parent as StackPanel;
            if (characterSelection == null) return;

            // Reset all buttons in the current player's selection area
            foreach (Button btn in characterSelection.Children)
            {
                btn.Background = Brushes.Transparent;
            }

            // Highlight the selected button in green
            selectedButton.Background = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

            // If there was a previously selected character, highlight it in red
            if (lastSelectedButton != null && lastSelectedButton != selectedButton)
            {
                lastSelectedButton.Background = Brushes.Red;
            }

            // Store this as the last selected button
            lastSelectedButton = selectedButton;
        }




        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            GameScreen gameScreen = new GameScreen();
            gameScreen.Closed += (sender, e) => this.Show();
            this.Hide();
            gameScreen.Show();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private int playerCount = 1;
        private void AddPLayer_Click(object sender, RoutedEventArgs e)
        {
            if (playerCount >= 6) return; // Limit to 6 players

            playerCount++;

            // Increase the window height dynamically
            this.Height += 170;

            // Create a rectangle for the new player's section
            Rectangle playerRectangle = new Rectangle
            {
                Height = 257,
                Width = 496,
                Stroke = Brushes.Black,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Label nameLabel = new Label
            {
                Content = "Player Name:",
                Margin = new Thickness(23, 10, 0, 0)
            };

            TextBox playerNameBox = new TextBox
            {
                Width = 200,
                Margin = new Thickness(23, 5, 0, 0)
            };

            Label charSelectionLabel = new Label
            {
                Content = "Select Character:",
                Margin = new Thickness(23, 5, 0, 0)
            };

            StackPanel characterSelection = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(23, 5, 0, 0) };

            string[] characterNames = { "Marquis", "Eyrie", "Woodland", "Vagabond", "Vagabond" };

            foreach (string character in characterNames)
            {
                Button characterButton = new Button
                {
                    Width = 61,
                    Height = 61,
                    Tag = character,
                    Margin = new Thickness(5)
                };

                Image characterImage = new Image
                {
                    Source = new BitmapImage(new Uri($"/Assets/{character}/Icon.png", UriKind.Relative)),
                    Width = 59,
                    Height = 59
                };

                characterButton.Content = characterImage;
                characterButton.Click += CharacterSelected;

                characterSelection.Children.Add(characterButton);
            }

            // Create a StackPanel to hold the player's elements
            StackPanel playerPanel = new StackPanel { Margin = new Thickness(0, 10, 0, 0) };
            playerPanel.Children.Add(playerRectangle);
            playerPanel.Children.Add(nameLabel);
            playerPanel.Children.Add(playerNameBox);
            playerPanel.Children.Add(charSelectionLabel);
            playerPanel.Children.Add(characterSelection);

            // Add the player panel to the main StackPanel (PlayersContainer)
            PlayersContainer.Children.Add(playerPanel);
        }


    }
}