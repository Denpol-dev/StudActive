using System;

namespace StudActive.Entities
{
    public partial class Inventory
    {
        public Guid InventoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
