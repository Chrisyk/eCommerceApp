using CRM.Library.Service;
using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;

public partial class InventoryView : ContentPage
{
	public InventoryView()
	{
		InitializeComponent();
        BindingContext = new InventoryManagementViewModel();
	}

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void DeleteItem(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.BindingContext as InventoryViewModel;
        if (item != null)
        {
            var viewModel = BindingContext as InventoryManagementViewModel;
            viewModel?.RemoveItem(item);
        }
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel).UpdateItem();
    }
}