using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace TerraIntel_lAI
{
    public partial class TestFile : ContentPage
    {
        private readonly InfoItemDatabase _database;

        // Constructor where we inject the InfoItemDatabase
        public TestFile(InfoItemDatabase database)
        {
            InitializeComponent();
            _database = database;
        }

        // Button click handler to add data to the database
        private async void OnAddItemClicked(object sender, EventArgs e)
        {

            await _database.AddInfoItemAsync(nameEntry.Text);

        }

        // Button click handler to retrieve and display items from the database
        private async void OnRetrieveItemsClicked(object sender, EventArgs e)
        {
            itemsListView.ItemsSource =await _database.GetItemsAsync();
        }
    }
}
