namespace RootRemake_GroupProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Player[]? players = new Player[] { new Player(), new Player() };
            MainGame m = new MainGame(players);
            m.Show();
        }
    }
}

