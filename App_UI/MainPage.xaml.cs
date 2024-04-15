using System.Collections.ObjectModel;

namespace App_UI
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        
    }

    public class MainPageViewModel
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>
        {
            new Item { Text = "Device 1", BackgroundColor = Colors.LightBlue },
            new Item { Text = "Automation 1", BackgroundColor = Colors.LightBlue },
            new Item { Text = "Notification 1", BackgroundColor = Colors.LightBlue }
            // Add more items as needed
        };
    }

    public class Item
    {
        public string Text { get; set; } = string.Empty; // Default to empty string
        public Color BackgroundColor { get; set; } = Colors.Red; // Default color
    }
}
