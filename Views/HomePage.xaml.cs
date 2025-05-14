using OpenAI.Chat;      // for ChatClient & ChatCompletion

namespace TerraIntel_lAI
{
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.HomeViewModel();
        }



        
    }

}
