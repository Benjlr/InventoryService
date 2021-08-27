using System.Collections.Generic;
using Inventory.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace InventoryViewModels
{
    public class OrderViewItemViewModel : ViewModelBase
    {
        private List<InventoryItem> _orderedItems { get; set; }
        public ObservableCollection<InventoryItem> OrderedItems => new ObservableCollection<InventoryItem>(_orderedItems);
        public ICommand OrderItems => _confirmOrder;
        public bool OrderSent { get; set; }

        private RelayCommand _confirmOrder;

        public OrderViewItemViewModel() {
            InitialiseData();
            InitialiseCommands();
        }

        private void InitialiseData() {
            OrderSent = false;
            _orderedItems = new List<InventoryItem>();
            ThreadPool.QueueUserWorkItem(new WaitCallback(GetAllItems));
        }

        private void InitialiseCommands() {
            _confirmOrder =
                new RelayCommand(command => { ConfirmOrder(); },
                    canExeccute => !OrderSent && OrderedItems != null);
        }

        private void ConfirmOrder() {
            OrderSent = !OrderSent;
            ThreadPool.QueueUserWorkItem(new WaitCallback(SendOrder));
            NotifyPropertyChanged($"OrderSent");

        }

        private void GetAllItems(object callback) {
            var items = InventoryClient.Instance.ViewItems();
            Application.Current.Dispatcher.Invoke(() => {
                _orderedItems = items.Select(x => new InventoryItem() {Name = x.Name, Amount = 0}).ToList();
                NotifyPropertyChanged($"OrderedItems");
            });
        }

        private void SendOrder(object callback) {
            var myNewItems = InventoryClient.Instance.GetItems(OrderedItems.ToList());
            Application.Current.Dispatcher.Invoke(() => {
                _orderedItems = myNewItems;
                NotifyPropertyChanged($"OrderedItems");
            });

        }


        public void DeleteOrder() => new Task(()=> InventoryClient.Instance.AddItems(_orderedItems)).Start();
        
    }
}
