using System.Runtime.Serialization;

namespace Inventory.Models
{

    public interface IInventoryItem
    {
        string Name { get; set; }
        int Amount { get; set; }
    }

    [DataContract]
    public class InventoryItem : IInventoryItem
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Amount { get; set; }
    }

}
