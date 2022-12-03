using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudActive.Models
{
    public class InventoryModel
    {
        public Guid InventoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
