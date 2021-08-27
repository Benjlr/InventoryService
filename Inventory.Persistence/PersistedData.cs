using Inventory.Models;

namespace Inventory.Persistence
{
    public class PersistedData
    {
        private static PersistedData _instance;
        public static PersistedData Instance => _instance ?? (_instance = new PersistedData());

        public InventoryManager Manager { get; }
        private PersistedData() {
            Manager = new InventoryManager();
            Manager.AddToInventory(new InventoryItem(){Name = "turkeys", Amount = 1233});
            Manager.AddToInventory(new InventoryItem(){Name = "biggerturkeys", Amount = 5435});
            //Manager.AddToInventory(new InventoryItem(){Name = "oranges", Amount = 60});
            //Manager.AddToInventory(new InventoryItem(){Name = "flowers", Amount = 75});
            //Manager.AddToInventory(new InventoryItem(){Name = "boxes", Amount = 20});
        }


    }
}
