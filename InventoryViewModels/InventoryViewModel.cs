using Inventory.Models;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

namespace InventoryViewModels
{
    public class InventoryViewModel : ViewModelBase
    {
        public ObservableCollection<InventoryItem> InventoryItems { get; set; }

        public InventoryViewModel() {
            InventoryClient.Instance.SubscribeToChanges(UpdateItems);
            UpdateItems();
        }

        private void UpdateItems() => ThreadPool.QueueUserWorkItem(new WaitCallback(GetAllItems));

        private void GetAllItems(object callback) {
            var items = InventoryClient.Instance.ViewItems();
            Application.Current.Dispatcher.Invoke(() => {
                InventoryItems = new ObservableCollection<InventoryItem>(items);
                NotifyPropertyChanged($"InventoryItems");
            });
        }

    }
}
