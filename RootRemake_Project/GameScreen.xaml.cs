using System;
using System.Collections.Generic;
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

        public Player[] players;
        public GameScreen()
        {
            InitializeComponent();
            this.players = players;

        }

        private void imgMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            //MessageBox.Show("X: " + me.X + " Y: " + me.Y);
            MessageBox.Show("X(%): " +
               ((double)me.X / imgMap.Width * 100).ToString("F4") +
               " Y(%): " + ((double)me.Y / imgMap.Height * 100).ToString("F4")
            );
        }
    }
}
