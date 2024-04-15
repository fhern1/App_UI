namespace App_UI;

public partial class Activity : ContentPage
{
	public Activity()
	{
        InitializeComponent();

        // Example list of notifications
        var notifications = new List<Notification>
        {
            new Notification { Title = "New Message", Message = "You have a new message.", TimeReceived = "10:45 AM" },
            // Add more notifications as needed
        };

        NotificationsCollectionView.ItemsSource = notifications;
    }
}

public class Notification
{
    public string Title { get; set; } = "Default Title";
    public string Message { get; set; } = "Default Message";
    public string TimeReceived { get; set; } = "Default Time";
}