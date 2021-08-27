using System;
using System.Collections.Generic;
using Inventory.Models;
using Xunit;
using System.Linq;
using Inventory.Persistence;

namespace Inventory.Tests
{
    public class PersistedDataTests
    {
        [Fact]
        public void ShouldAddNewItemsToInventory() {
            var myManager = new InventoryManager();
            myManager.AddToInventory(new InventoryItem(){Name = "apples", Amount = 20});
            ItemsAreSame(new InventoryItem() { Name = "apples", Amount = 20 }, (InventoryItem)myManager._inventory.FirstOrDefault(x => x.Name.Equals("apples")));
        }

        [Fact]
        public void ShouldAddToExistingItemsToInventory() {
            var myManager = new InventoryManager();
            myManager.AddToInventory(new InventoryItem() { Name = "apples", Amount = 20 });
            myManager.AddToInventory(new InventoryItem() { Name = "apples", Amount = 20 });
            ItemsAreSame(new InventoryItem() { Name = "apples", Amount = 40 }, myManager._inventory.FirstOrDefault(x=>x.Name.Equals("apples")));
        }

        [Fact]
        public void ShouldRemoveItemsFromInventory() {
            var myManager = new InventoryManager();
            myManager.AddToInventory(new InventoryItem() { Name = "apples", Amount = 50 });
            var returned = myManager.GetObjectsFromInventory(new InventoryItem() { Name = "apples", Amount = 20 });
            ItemsAreSame(new InventoryItem() { Name = "apples", Amount = 30 }, myManager._inventory.FirstOrDefault(x => x.Name.Equals("apples")));
            ItemsAreSame(new InventoryItem(){Name = "apples" , Amount = 20}, returned);
        }

        [Fact]
        public void ShouldNotRemoveNonExistentItemsFromInventory() {
            var myManager = new InventoryManager();
            myManager.AddToInventory(new InventoryItem() { Name = "apples", Amount = 50 });
            
            Assert.Throws<Exception>(() => {
                myManager.GetObjectsFromInventory(new InventoryItem() { Name = "bananas", Amount = 20 });
            });
        }

        [Fact]
        public void ShouldReturnOnlyRemainingItemsFromInventory() {
            var myManager = new InventoryManager();
            myManager.AddToInventory(new InventoryItem() { Name = "apples", Amount = 50 });
            var returned = myManager.GetObjectsFromInventory(new InventoryItem() { Name = "apples", Amount = 60 });
            Assert.True( myManager._inventory.FirstOrDefault(x => x.Name.Equals($"apples")).Amount == 0);
            ItemsAreSame(new InventoryItem() { Name = "apples", Amount = 50 }, returned);

        }

        [Fact]
        public void CanGetMultipleItems() {
            var myManager = new InventoryManager();
            myManager.AddToInventory(new InventoryItem() { Name = "apples", Amount = 50 });
            myManager.AddToInventory(new InventoryItem() { Name = "bananas", Amount = 50 });

            var myObjects =  myManager.GetDifferentObjectsFromInventory(new List<InventoryItem>()
            {
                new InventoryItem(){Name = "apples", Amount = 25},
                new InventoryItem(){Name = "bananas", Amount = 30},
            });

            ItemsAreSame(new List<InventoryItem>() { new InventoryItem() { Name = "apples", Amount = 25 }, new InventoryItem() { Name = "bananas", Amount = 30 } }, myObjects);
        }

        [Fact]
        public void CanAddMultipleItems() {
            var myManager = new InventoryManager();
            myManager.AddItemsToInventory(new List<InventoryItem>() {
                new InventoryItem() { Name = "apples", Amount = 50 },
                new InventoryItem() { Name = "bananas", Amount = 60 }});

            ItemsAreSame(new List<InventoryItem>() { new InventoryItem() { Name = "apples", Amount = 50 }, new InventoryItem() { Name = "bananas", Amount = 60 } }, myManager._inventory);
        }

        private void ItemsAreSame(IInventoryItem itemOne, IInventoryItem itemTwo) => Assert.True(itemOne.Name.Equals(itemTwo.Name) && itemOne.Amount.Equals(itemTwo.Amount));

        private void ItemsAreSame(List<InventoryItem> itemOne, List<InventoryItem> itemTwo) {
            Assert.True(itemOne.Count==itemTwo.Count);
            for (int i = 0; i < itemOne.Count; i++) 
                ItemsAreSame(itemOne[i], itemTwo[i]);
        }
    }
}
