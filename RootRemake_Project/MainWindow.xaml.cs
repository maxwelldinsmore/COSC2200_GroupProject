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
        public MainWindow(Player[] players)
        {
            InitializeComponent();
            this.players = players ?? throw new ArgumentNullException(nameof(players));
            currentWidth = this.Width;
            currentHeight = this.Height;
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