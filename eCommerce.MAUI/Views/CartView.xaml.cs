using eCommerce.MAUI.ViewModels;
using eCommerce.Library.Service;

namespace eCommerce.MAUI.Views;
[QueryProperty(nameof(CartId), "CartId")]
public partial class CartView : ContentPage
{
	public CartView()
	{
		InitializeComponent();
	}

    public int CartId { get; set; }


    private void AddClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.BindingContext as InventoryViewModel;
        if (item != null)
        {
            (BindingContext as CompositeViewModel)?.ShopViewModel?.AddItem(item);
        }
        (BindingContext as CompositeViewModel).InventoryViewModel?.RefreshItems();
        (BindingContext as CompositeViewModel).ShopViewModel?.RefreshItems();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.BindingContext as ShopViewModel;
        if (item != null)
        {
            (BindingContext as CompositeViewModel)?.ShopViewModel.RemoveItem(item);
        }
        (BindingContext as CompositeViewModel).InventoryViewModel?.RefreshItems();
        (BindingContext as CompositeViewModel).ShopViewModel?.RefreshItems();
    }
    private void CheckoutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Checkout?CartId={CartId}");
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CompositeViewModel(CartId);
    }

}