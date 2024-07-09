using eCommerce.MAUI.ViewModels;
namespace eCommerce.MAUI.Views;

public partial class ConfigurationView : ContentPage
{
	public ConfigurationView()
	{
		InitializeComponent();
		BindingContext = new ShopManagementViewModel();
	}
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}