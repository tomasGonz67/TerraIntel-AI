using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraIntel_lAI.ViewModels
{
    public partial class HistoryViewModel : ObservableObject
    {
        readonly InfoItemDatabase _db;  // concrete type

        public ObservableCollection<InfoItem> Items { get; } = new();
        public IAsyncRelayCommand<InfoItem> DeleteCommand { get; }

        public HistoryViewModel(InfoItemDatabase db)
        {
            _db = db;
            DeleteCommand = new AsyncRelayCommand<InfoItem>(OnDeleteAsync);
            LoadItems();
        }

        async Task LoadItems()
        {
            Items.Clear();
            var list = await _db.GetItemsAsync();
            foreach (var i in list)
                Items.Add(i);
        }

        async Task OnDeleteAsync(InfoItem item)
        {
            await _db.DeleteItemAsync(item);
            Items.Remove(item);
        }
    }

}
