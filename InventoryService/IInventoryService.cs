using Inventory.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace InventoryService
{

    [ServiceContract]
    public interface IInventoryService {

        [OperationContract]
        InventoryItem GetItem(InventoryItem composite);

        [OperationContract]
        void AddItem(InventoryItem composite);

        [OperationContract]
        List<InventoryItem> ViewItems();

        [OperationContract]
        List<InventoryItem> GetItems(List<InventoryItem> composite);

        [OperationContract]
        void AddItems(List<InventoryItem> composite);
    }
}
