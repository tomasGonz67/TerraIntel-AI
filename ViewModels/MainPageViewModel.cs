using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;    // or use your own RelayCommand
using Microsoft.Maui.Devices.Sensors;
using System.Threading.Tasks;
using TerraIntel_lAI.Services;


namespace TerraIntel_lAI.ViewModels
{
    /// <summary>
    /// ViewModel for MainPage - barebones (no commands or methods yet)
    /// </summary>
    public partial class MainPageViewModel : ObservableObject
    {
        readonly IAIService _aiService;

        readonly InfoItemDatabase _db;    // concrete type


        [ObservableProperty]
        string aiResponse;

        [ObservableProperty]
        string notificationMessage;

        public IAsyncRelayCommand GetFactCommand { get; }

        public ICommand GoToHistoryCommand { get; }
        public MainPageViewModel(IAIService aiService, InfoItemDatabase db)
        {
            _aiService = aiService;
            _db = db;
            GetFactCommand = new AsyncRelayCommand(OnGetFactAsync);

            GoToHistoryCommand = new AsyncRelayCommand(GoToHistory);
            // TODO: Initialize any properties or services here
        }

        async Task OnGetFactAsync()
        {
            try
            {
                // 1) get coords
                var req = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var loc = await Geolocation.Default.GetLocationAsync(req);
                if (loc == null)
                {
                    AiResponse = "Unable to get location.";
                    return;
                }

                // 2) fetch fact
                AiResponse = await _aiService.GetFactAsync(loc.Latitude, loc.Longitude);

                await _db.AddInfoItemAsync(AiResponse);

                NotificationMessage = "Response has been added to the database.";

            }
            catch (Exception ex)
            {
                AiResponse = $"❌ {ex.GetType().Name}: {ex.Message}";
                NotificationMessage = string.Empty;

            }
        }

        private async Task GoToHistory()
        {
            await Shell.Current.GoToAsync(nameof(History));
        }
    }
}
