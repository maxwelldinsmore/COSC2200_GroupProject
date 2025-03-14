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
            Player[] players = new Player[4];
            GameScreen gameScreen = new GameScreen();
            gameScreen.Show();
        }


        private void imgBoxBoard_Click(object sender, EventArgs e)
        {
            //MouseEventArgs me = (MouseEventArgs)e;
            ////MessageBox.Show("X: " + me.X + " Y: " + me.Y);
            //MessageBox.Show("X(%): " +
            //   ((double)me.X / picBoxBoard.Width * 100).ToString("F4") +
            //   " Y(%): " + ((double)me.Y / picBoxBoard.Height * 100).ToString("F4")
            //);
        }
    }
}