using CRM.Library.Service;
using System.Windows.Input;
using CRM.Models;
using CRM.Library.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eCommerce.MAUI.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        public ICommand? ViewCartCommand { get; private set; }

        public ShoppingCart Cart;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id
        {
            get => Cart.Id;

        }

        public void ExecuteViewCart(CartViewModel? cart)
        {
            if (cart == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Cart?CartId={cart.Id}");

        }

        public void SetupCommands()
        {
            ViewCartCommand = new Command(
                (c) => ExecuteViewCart(c as CartViewModel));

        }
        public CartViewModel()
        {
            Cart = new ShoppingCart();
            SetupCommands();
        }

        public CartViewModel(ShoppingCart cart)
        {
            if (cart == null)
            {
                Cart = new ShoppingCart();
            }
            else
            {
                Cart = cart;
            }
            SetupCommands();
        }





    }
}
