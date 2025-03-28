﻿using System.Numerics;
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
            Player[] players = new Player[4];

            GameScreen gameScreen = new GameScreen();
            gameScreen.Closed += (sender, e) => this.Show();
            this.Hide();
            gameScreen.Show();
        }

        private void CharacterSelected(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            string characterTag = selectedButton.Tag.ToString();
            MessageBox.Show($"Character selected: {characterTag}");
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
      
        }

        
    }
}