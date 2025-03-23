namespace TerraIntel_lAI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
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
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnCurrentPush(object sender, EventArgs e)
        {
            await GetCurrentLocation();
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
                    cords.Text = lat.ToString() + " " + lon.ToString() + " " + location.Altitude.ToString();
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
