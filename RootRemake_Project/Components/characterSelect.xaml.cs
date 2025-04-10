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
    /// Interaction logic for characterSelect.xaml
    /// </summary>
    public partial class characterSelect : UserControl
    {
        public string SelectedCharacter { get; private set; } // Property to store the selected character
        public characterSelect()
        {
            InitializeComponent();
        }
        private void CharacterSelected(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;

           
            if (selectedButton != null)
            {
                // Highlight the selected button in green
                selectedButton.Background = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

                switch (selectedButton.Name)
                {
                    case "marquisBtn":
                        SelectedCharacter = "Marquis";
                        break;
                    case "eyrieBtn":
                        SelectedCharacter = "Eyrie";
                        break;
                    case "woodlandBtn":
                        SelectedCharacter = "Woodland";
                        break;
                    case "vagabondBtn":
                        SelectedCharacter = "Vagabond";
                        break;
                    default:
                        SelectedCharacter = string.Empty;
                        break;
                }





                foreach (Button button in charGrid.Children.OfType<Button>())
                {
                    if (button != selectedButton)
                    {
                        // Reset the background of other buttons to white
                        button.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                    }
                }




            }

            
        }
    }
}
