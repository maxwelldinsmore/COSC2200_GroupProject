using RootRemake_Project.CharacterClasses;
using RootRemake_Project.Components;
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

        public bool toggled3 = false; // Toggle for the third button
        public bool toggled4 = false; // Toggle for the fourth button
        public MainWindow()
        {
            InitializeComponent();



            //Player[] players = new Player[2];
            //players[0] = new MarquisDeCat("Bilgan");
            //players[1] = new Eyrie("Mariah");
            //GameScreen gameScreen = new GameScreen(players);
            //gameScreen.Closed += (sender, e) => this.Show();
            //this.Hide();
            //gameScreen.Show();


        }

        private Player assignCharacter(characterSelect characterSelect)
        {
            if (characterSelect.SelectedCharacter == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(characterSelect.charText.Text))
            {
                return null;
            }

            Player player;
            if (characterSelect.SelectedCharacter == "Marquis")
            {
                player = new MarquisDeCat(characterSelect.charText.Text);
            }
            else if (characterSelect.SelectedCharacter == "Eyrie")
            {
                player = new Eyrie(characterSelect.charText.Text);
            }
            else if (characterSelect.SelectedCharacter == "Woodland")
            {
                player = new Woodland(characterSelect.charText.Text);
            }
            else 
            {
                player = new Vagabond(characterSelect.charText.Text);
            }
            return player;
        }


        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            Player[] players;
            
           
            Player[] UpdatedPlayers;
            // Get player array 
            if (toggled3 && toggled4)
            {
                players = new Player[2];
                players[0] = assignCharacter(c1);
                players[1] = assignCharacter(c2);
                UpdatedPlayers = players;
            }
            else if (toggled3)
            {
                players = new Player[3];
                players[0] = assignCharacter(c1);
                players[1] = assignCharacter(c2);
                players[3] = assignCharacter(c4);
            }
            else if (toggled4)
            {
                players = new Player[3];
                players[0] = assignCharacter(c1);
                players[1] = assignCharacter(c2);
                players[2] = assignCharacter(c3);
            }
            else
            {
                players = new Player[4];
                players[0] = assignCharacter(c1);
                players[1] = assignCharacter(c2);
                players[2] = assignCharacter(c3);
                players[3] = assignCharacter(c4);
            }
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == null)
                {
                    MessageBox.Show("Please select a character for all players.");
                    return;
                }
            }
            GameScreen gameScreen = new GameScreen(players);

            gameScreen.Closed += (sender, e) => this.Show();
            this.Hide();
            gameScreen.Show();

        }

        private int playerCount = 1;
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!toggled3)
            {
                c3.Opacity = 0.5;
                toggled3 = !toggled3;
                c3.IsEnabled = false; // Disable the button
            }
            else
            {
                c3.Opacity = 1;
                toggled3 = !toggled3;
                c3.IsEnabled = true; // Enable the button

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!toggled4)
            {
                c4.Opacity = 0.5;
                toggled4 = !toggled4;
                c4.IsEnabled = false; // Disable the button
            }
            else
            {
                c4.Opacity = 1;
                toggled4 = !toggled4;
                c4.IsEnabled = true;
            }
        }
    }
}