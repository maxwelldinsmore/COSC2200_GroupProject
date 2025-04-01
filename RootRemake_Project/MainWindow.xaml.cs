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

        private void CharacterSelected(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            if (selectedButton != null) 
            {
                selectedButton.Background = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));
            }
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
            if (playerCount >= 6) return; // limit to 6 players

            playerCount++;
            StackPanel playerPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(10) };

            Label playerLabel = new Label
            {
                Content = $"Player {playerCount}",
                FontSize = 20,
                FontWeight = FontWeights.Bold,
            };

            TextBox playerNameBox = new TextBox
            {
                Width = 150,
                Margin = new Thickness(5)
            };

            StackPanel characterSelection = new StackPanel { Orientation = Orientation.Horizontal };

            string[] characterNames = { "Marquis", "Eyrie", "Woodland", "VagabondGray", "VagabondBlack" };

            foreach (string character in characterNames)
            {
                Button characterButton = new Button
                {
                    Content = new Image { Source = new BitmapImage(new Uri($"/Assets/{character}/Icon.png", UriKind.Relative)) },
                    Width = 50,
                    Height = 50,
                    Tag = character,
                };
                characterButton.Click += CharacterSelected;
                characterSelection.Children.Add(characterButton);
            }

            playerPanel.Children.Add(playerLabel);
            playerPanel.Children.Add(playerNameBox);
            playerPanel.Children.Add(characterSelection);

   
        }
    }
}