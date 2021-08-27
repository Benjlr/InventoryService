using Inventory.Models;
using Inventory.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;

namespace InventoryService
{
    // NOTE: In order to launch WCF Test Client for testing this service, please select InventoryService.svc or InventoryService.svc.cs at the Solution Explorer and start debugging.
    public class InventoryService : IInventoryService
    {
        private PersistedData _data => PersistedData.Instance;
        public List<InventoryItem> GetItems(List<InventoryItem> composite) {
            if (composite == null) 
                throw new ArgumentNullException("composite");

            Thread.Sleep(1500);
            return _data.Manager.GetDifferentObjectsFromInventory(composite);
        }

        public InventoryItem GetItem(InventoryItem composite) {
            if (composite == null)
                throw new ArgumentNullException("composite");

            Thread.Sleep(1500);
            return _data.Manager.GetObjectsFromInventory(composite);
        }

        public void AddItems(List<InventoryItem> composite) {
            if (composite == null)
                throw new ArgumentNullException("composite");

            Thread.Sleep(1500);
            _data.Manager.AddItemsToInventory(composite);
        }

        public void AddItem(InventoryItem composite) {
            if (composite == null)
                throw new ArgumentNullException("composite");

            Thread.Sleep(1500);
            _data.Manager.AddToInventory(composite);
        }

        public List<InventoryItem> ViewItems() {
            Thread.Sleep(1500);
            return _data.Manager._inventory;
        }
    }
}
