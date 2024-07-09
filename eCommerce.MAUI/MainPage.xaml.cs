using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void ManageInventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Inventory");
        }

        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Shop");
        }
        private void SettingsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Configuration");
        }
    }
}
