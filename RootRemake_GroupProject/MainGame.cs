using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RootRemake_GroupProject
{
    public partial class MainGame : Form
    {
        internal Player[] players;


        private double currentWidth;
        private double currentHeight;

        public MainGame(Player[] players)
        {
            InitializeComponent();
            this.players = players ?? throw new ArgumentNullException(nameof(players));
            currentWidth = this.Width;
            currentHeight = this.Height;
        }

        private void MainGame_Load(object sender, EventArgs e)
        {

        }

        private void picBoxBoard_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            //MessageBox.Show("X: " + me.X + " Y: " + me.Y);
            MessageBox.Show("X(%): " +
               ((double)me.X / picBoxBoard.Width * 100).ToString("F4") +
               " Y(%): " + ((double)me.Y / picBoxBoard.Height * 100).ToString("F4")
            );

        }

        private void MainGame_Resize(object sender, EventArgs e)
        {
            //if (this.Width != currentWidth)
            //{
            //    double ratio = (double)16 / 9; ;
            //    currentWidth = this.Width;
            //    currentHeight = this.Height;
            //    picBoxBoard.Width = (int)(picBoxBoard.Width * ratio);
            //    picBoxBoard.Height = (int)(picBoxBoard.Height * ratio);
            //}
            //if (this.Height != currentHeight)
            //{
            //    double ratio = (double)16 / 9;
            //    currentWidth = this.Width;
            //    currentHeight = this.Height;
            //    picBoxBoard.Width = (int)(picBoxBoard.Width * ratio);
            //    picBoxBoard.Height = (int)(picBoxBoard.Height * ratio);
            //}

        }
    }
}
