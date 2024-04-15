namespace App_UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Navigate to the Home page route
            this.GoToAsync("//home");
        }
    }
}
