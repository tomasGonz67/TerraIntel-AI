using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace TerraIntel_lAI
{
    public partial class History : ContentPage
    {
        private readonly InfoItemDatabase _database;

        // Constructor where we inject the InfoItemDatabase
        public History(InfoItemDatabase database)
        {
            InitializeComponent();
            _database = database;
            this.BindingContext = new ViewModels.HistoryViewModel(_database);
        }

    }
}
