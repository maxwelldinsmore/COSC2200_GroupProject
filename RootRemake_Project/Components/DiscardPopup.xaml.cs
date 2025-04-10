using RootRemake_Project.ObjectClasses;
using System.Windows.Controls;
using System.Windows;

namespace RootRemake_Project.Components
{
    public partial class DiscardPopup : UserControl
    {
        public event Action<bool> DialogResult;
        private Card _currentCard;

        public DiscardPopup()
        {
            InitializeComponent();
        }

        // Mode 1: Initial warning (no card selection)
        

    }
}