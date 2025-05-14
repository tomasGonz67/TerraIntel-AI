using TerraIntel_lAI.Services;

namespace TerraIntel_lAI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var aiService = new AIService();
            var dbService = new InfoItemDatabase();
            this.BindingContext = new ViewModels.MainPageViewModel(aiService, dbService);
        }

    }

}
