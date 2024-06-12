using eCommerce.MAUI.ViewModels;
namespace eCommerce.MAUI.Views;

public partial class CheckoutView : ContentPage
{
	public CheckoutView()
	{
		InitializeComponent();
		BindingContext = new ShopManagementViewModel();
	}
}