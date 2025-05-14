namespace TerraIntel_lAI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(History), typeof(History));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
