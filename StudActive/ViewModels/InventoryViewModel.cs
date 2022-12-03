using Microsoft.EntityFrameworkCore;
using StudActive.Entities;
using StudActive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudActive.ViewModels
{
    public class InventoryViewModel
    {
        public static async Task<List<InventoryModel>> GetInventory()
        {
            Context context = new();
            List<InventoryModel> result = new();
            List<Inventory> inventories = new();
            await context.Inventories.ForEachAsync(c =>
            {
                inventories.Add(c);
            });

            foreach(var i in inventories)
            {
                result.Add(new InventoryModel
                {
                    InventoryId = i.InventoryId,
                    Name = i.Name,
                    Amount = i.Amount,
                    Description = i.Description
                });
            }
            return result;
        }

        public static bool SetInventory()
        {
            return false;
        }
    }
}
