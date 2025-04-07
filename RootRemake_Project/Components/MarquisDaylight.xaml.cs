using RootRemake_Project.CharacterClasses;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RootRemake_Project.Components
{
    /// <summary>
    /// Interaction logic for Marquis.xaml
    /// </summary>
    public partial class MarquisDaylight : UserControl
    {
        public MarquisDaylight()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AttemptBuild(string buildingType)
        {
            MarquisDeCat marquis = (MarquisDeCat)Players[0]; // update if tracking current player 
            int builtCount = 0;
            int remaining = 0;
            Uri imageSource = null;
            int cost = 0;
            string buildingName = "";
            

            switch (buildingType)
            {
                case "Sawmill":
                    builtCount = 6 - marquis.AvailableSawmills;
                    remaining = marquis.AvailableSawmills;
                    imageSource = marquis.SawmillArt;
                    buildingName = "Sawmill";
                    break;
                case "Workshop":
                    builtCount = 6 - marquis.AvailableWorkshops;
                    remaining = marquis.AvailableWorkshops;
                    imageSource = marquis.WorkshopArt;
                    buildingName = "Workshop";
                    break;
                case "Recruiter":
                    builtCount = 6 - marquis.AvailableRecruiters;
                    remaining = marquis.AvailableRecruiters;
                    imageSource = marquis.RecruiterArt;
                    buildingName = "Recruiter";
                    break;
            }

            if (remaining <= 0)
            {
                MessageBox.Show($"Not enough wood to build a {buildingType}. You need {cost} wood.");
                return;
            }

            // deduct wood
            marquis.AvailableWood -= cost;

            // update remaining count
            switch (buildingType)
            {
                case "Sawmill":
                    marquis.AvailableSawmills--;
                    break;
                case "Workshop":
                    marquis.AvailableWorkshops--;
                    break;
                case "Recruiter":
                    marquis.AvailableRecruiters--;
                    break;
            }

            // render building on board -- update later to let user choose location
            PlaceBuildingOnBoard(imageSource, buildingName);
        }

        private void PlaceBuildingOnBoard(Uri buildingUri, string buildingName)
        {
            var parentWindow = Window.GetWindow(this) as GameScreen;

            Image buildingImage = new Image
            {
                Source = new BitmapImage(buildingUri),
                Width = 25,
                Height = 25,
                Name = buildingName,
                IsHitTestVisible = false
            };

            // update it later with location methods
            Point buildingPoint = new Point(100, 100); // example

            Canvas.SetLeft(buildingImage, buildingPoint.X);
            Canvas.SetTop(buildingImage, buildingPoint.Y);
            if (parentWindow != null)
            {
                parentWindow.can.Children.Add(buildingImage);

            }
        }

        private void buildSawmillButton_Click(object sender, RoutedEventArgs e)
        {
            AttemptBuild("Sawmill");
        }
        private void buildWorkshopButton_Click(object sender, RoutedEventArgs e)
        {
            AttemptBuild("Workshop");
        }
        private void buildRecruiterButton_Click(object sender, RoutedEventArgs e)
        {
            AttemptBuild("Recruiter");
        }
    }
}
