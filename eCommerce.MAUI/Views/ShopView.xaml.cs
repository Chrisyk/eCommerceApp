using eCommerce.Library.Models;
using eCommerce.MAUI.ViewModels;
namespace eCommerce.MAUI.Views;

public partial class ShopView : ContentPage
{
    public ShopView()
	{
        InitializeComponent();
        BindingContext = new CartManagementViewModel();
        
    }

    private void AddCartClicked(object sender, EventArgs e)
    {
        (BindingContext as CartManagementViewModel)?.AddCart();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var Cart = button?.BindingContext as CartViewModel;
        if (Cart != null)
        {
            (BindingContext as CartManagementViewModel)?.RemoveCart(Cart);
        }


    }

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CartManagementViewModel)?.RefreshItems();

    }

}