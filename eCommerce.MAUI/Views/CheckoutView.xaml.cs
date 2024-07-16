using eCommerce.Library.Service;
using eCommerce.MAUI.ViewModels;
namespace eCommerce.MAUI.Views;
[QueryProperty(nameof(CartId), "CartId")]

public partial class CheckoutView : ContentPage
{
	public CheckoutView()
	{
		InitializeComponent();
		
	}
    private void BackClicked(object sender, EventArgs e)
    {
        ShopServiceProxy.Current.RemoveCart(ShopServiceProxy.Current.Carts.FirstOrDefault(c => c.Id == CartId));
        Shell.Current.GoToAsync("//Shop");
    }
    public int CartId { get; set; }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ShopManagementViewModel(CartId);
    }

}