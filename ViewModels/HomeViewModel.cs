using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;    // or use your own RelayCommand
using Microsoft.Maui.Controls;

namespace TerraIntel_lAI.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        public ICommand GoToHistoryCommand { get; }
        public ICommand GoToMainCommand { get; }

        public HomeViewModel()
        {
            GoToHistoryCommand = new AsyncRelayCommand(GoToHistory);
            GoToMainCommand = new AsyncRelayCommand(GoToMain);
        }

        private async Task GoToHistory()
        {

            await Shell.Current.GoToAsync(nameof(History));
        }

        private async Task GoToMain()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
