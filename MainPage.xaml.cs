using OpenAI.Chat;      // for ChatClient & ChatCompletion

namespace TerraIntel_lAI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
        private string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnNavigateToTestFile(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TestFile));
        }
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {apiKey} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnCurrentPush(object sender, EventArgs e)
        {
            await GetCurrentLocation();
        }

        public async Task DisplayAI(double lat, double lon)
        {
            try
            {
                string prompt =
$"Give me an interesting historical fact about the location at coordinates {lat}, {lon}. Start it off with 'You are currenty located at 'Name of area:'. Then give an interesting fact.";
                cords.Text = "test1";
                // Instantiate the official ChatClient
                var client = new ChatClient(
                    model: "gpt-4.1",      // or "gpt-3.5-turbo", etc.
                    apiKey: apiKey
                );
                
                // Async call into the API
                ChatCompletion completion =
                    await client.CompleteChatAsync(prompt);  // :contentReference[oaicite:1]{index=1}

                // Extract and show the result
                string fact = completion.Content[0].Text.Trim();
                cords.Text = fact;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                cords.Text = $"❌ {ex.GetType().Name}: {ex.Message}";
            }

            }

        public async Task GetCurrentLocation()
        {
            try
            {

                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    double lat = location.Latitude;
                    double lon = location.Longitude;
                    await DisplayAI(lat,lon);
                }

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }

}
