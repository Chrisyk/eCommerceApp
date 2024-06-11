using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;
[QueryProperty(nameof(ItemId), "itemId")]
public partial class EditItemView : ContentPage
{
    public int ItemId { get; set; }
	public EditItemView()
	{
		InitializeComponent();
    }
    private void SaveClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.SaveChanges();
        Shell.Current.GoToAsync("//Inventory");

    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new InventoryViewModel(ItemId);
    }
}