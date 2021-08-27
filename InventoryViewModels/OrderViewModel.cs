using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace InventoryViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        public ICommand AddOrderCommand => _addOrder;
        public ICommand DeleteOrderCommand => _deleteOrder;

        public ObservableCollection<OrderViewItemViewModel> OrderItems { get; set; }
        public int SelectedOrder { get; set; }

        public OrderViewModel() {
            OrderItems = new ObservableCollection<OrderViewItemViewModel>();
            InitialiseCommands();
        }

        private RelayCommand _addOrder;
        private RelayCommand _deleteOrder;

        private void InitialiseCommands() {
            _addOrder =
                new RelayCommand(command => { AddOrder(); },
                    canExeccute => true);

            _deleteOrder =
                new RelayCommand(command => { DeleteOrder(); },
                    canExeccute => true);
        }

        private void AddOrder() {

            Application.Current.Dispatcher.Invoke(() => {
                OrderItems.Add(new OrderViewItemViewModel());
            });
        }

        private void DeleteOrder() {
            if (SelectedOrder != -1) {
                OrderItems[SelectedOrder].DeleteOrder();
                OrderItems.RemoveAt(SelectedOrder);

            }
        }


    }
}
