using CRM.Models;
using eCommerce.MAUI.ViewModels;
namespace eCommerce.MAUI.Views;

public partial class ShopView : ContentPage
{
    public ShopView()
	{
        InitializeComponent();
        BindingContext = new CompositeViewModel();
    }

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
            (BindingContext as CompositeViewModel)?.ShopViewModel?.RemoveItem(item);
        }
      (BindingContext as CompositeViewModel).InventoryViewModel?.RefreshItems();
        (BindingContext as CompositeViewModel).ShopViewModel?.RefreshItems();

    }

    private void CheckoutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Checkout");
    }

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CompositeViewModel).ShopViewModel?.RefreshItems();
        (BindingContext as CompositeViewModel).InventoryViewModel?.RefreshItems();


    }

}