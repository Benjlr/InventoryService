using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Persistence
{
    public class InventoryManager
    {
        public List<InventoryItem> _inventory { get; set; }

        public InventoryManager() {
            _inventory = new List<InventoryItem>();
        }

        public InventoryItem GetObjectsFromInventory(InventoryItem requestedObjects) {
            var item = _inventory.FirstOrDefault(x => x.Name.Equals(requestedObjects.Name));
            if (item != null) 
                return ObjectsFromInventory(requestedObjects, item);
            else
                throw new Exception("none found!");
        }

        public List<InventoryItem> GetDifferentObjectsFromInventory(List<InventoryItem> requestedObjects) {

            var itemsToReturn = new List<InventoryItem>();
            foreach (var t in requestedObjects) 
                itemsToReturn.Add(GetObjectsFromInventory(t));
            return itemsToReturn;
        }

        private InventoryItem ObjectsFromInventory(InventoryItem requestedObjects, InventoryItem item) {
            if (requestedObjects.Amount > item.Amount) {
                var myAmount = item.Amount;
                item.Amount = 0;
                return new InventoryItem() {Name = item.Name, Amount = myAmount};
            }
            else {
                item.Amount -= requestedObjects.Amount;
                return new InventoryItem() {Name = requestedObjects.Name, Amount = requestedObjects.Amount};
            }
        }

        public void AddToInventory(InventoryItem item) {
            var inventoryItem = _inventory.FirstOrDefault(x => x.Name.Equals(item.Name));
            if (inventoryItem != null)
                inventoryItem.Amount += item.Amount;
            else
                _inventory.Add(item);
        }

        public void AddItemsToInventory(List<InventoryItem> items) {
            foreach (var t in items)
                AddToInventory(t);
        }


    }

}