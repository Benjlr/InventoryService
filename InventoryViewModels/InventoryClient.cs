using Inventory.Models;
using InventoryViewModels.InventoryService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryViewModels
{
    public class InventoryClient
    {
        public static InventoryClient Instance => _instance ??(_instance= new InventoryClient());
        private static InventoryClient _instance;


        private InventoryServiceClient _client { get; }

        private InventoryClient() {
            _client = new InventoryServiceClient();
            _subscribers = new List<Action>();
        }

        private List<Action> _subscribers;
        public void SubscribeToChanges(Action myAction) {
            _subscribers.Add(myAction);
        }


        private void NotifySubscribers() {
            for (int i = _subscribers.Count-1; i >= 0; i--) {
                if(_subscribers[i] == null)
                    _subscribers.RemoveAt(i);
                else _subscribers[i].BeginInvoke(null, null);
            }
        }

        public List<InventoryItem> ViewItems() {
            var myItems = _client.ViewItems();
            return myItems.ToList();
        }

        public void AddItem(InventoryItem item) {
            _client.AddItem(item);
            NotifySubscribers();
        }

        public void AddItems(List<InventoryItem> item) {
            _client.AddItems(item.ToArray());
            NotifySubscribers();
        }

        public List<InventoryItem> GetItems(List<InventoryItem> items) {
            var returnedItems = _client.GetItems(items.ToArray());
            NotifySubscribers();
            return returnedItems.ToList();
        }

    }
}
