using Labirinth.Classes.Itemobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes
{
    class InventoryObject
    {
        public List<ItemObject> Items { get; private set; }
        public ulong Money { get; private set; }
        public uint Max_mass { get; private set; }
        public uint Cur_mass{ get; private set; }

        public InventoryObject(uint M_mass, ulong mon = 0)
        {
            Money = mon;
            Max_mass = M_mass;
            Cur_mass = 0;
            Items = new List<ItemObject>();
        }

        public bool TryAddItem(ItemObject ItemToAdd)
        {
            if (ItemToAdd == null)
                return false;
            if (ItemToAdd.mass + Cur_mass > Max_mass)
                return false;

            Cur_mass += ItemToAdd.mass;
            Items.Add(ItemToAdd);
            return true;
        }

        public void AddMoney(ulong moneyToAdd)
        {
            Money += moneyToAdd;
        }

        public bool TrySellItem(int ItemNumToSell, out string name)
        {
            name = "";
            if (ItemNumToSell < 0 || ItemNumToSell > Items.Count - 1)
                return false;

            name = Items[ItemNumToSell].Rarity.ToString() + " " + Items[ItemNumToSell].name;
            Cur_mass -= Items[ItemNumToSell].mass;
            Money += Items[ItemNumToSell].cost;
            
            Items.RemoveAt(ItemNumToSell);
            return true;
        }

        public ItemObject TryDeleteItem(int ItemNumToDelete)
        {
            ItemObject ItemToDelete;
            if (ItemNumToDelete < 0 || ItemNumToDelete > Items.Count - 1)
                return null;
            Cur_mass -= Items[ItemNumToDelete].mass;
            ItemToDelete = Items[ItemNumToDelete];
            Items.RemoveAt(ItemNumToDelete);
            return ItemToDelete;
        }
    }
}
