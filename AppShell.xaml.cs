namespace TerraIntel_lAI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TestFile), typeof(TestFile));
        }
    }
}
