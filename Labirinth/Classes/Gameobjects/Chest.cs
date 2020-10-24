using Labirinth.Classes.Itemobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes.Gameobjects
{
    
    class Chest : GameObject,IInteractable
    {

        string[] VariantsOfName = { "", "Elador`s", "Grankrest`s", "Kali`s", "Kraken`s" };
        string[] VariantsOfWeapon = {"bow","sword","shield","gloves","spear", "scyth" };
        string[] VariantsOfEnding = { "", "of death", "of eternity", "of sins"};

        Random r;
        InventoryObject ChestInventory;
        World w;
        public Chest(Graphics graph, bool isW, int X_pos, int Y_pos, World world) : base(graph, isW, X_pos, Y_pos)
        {
            r = new Random();
            ChestInventory = new InventoryObject(uint.MaxValue, (ulong)r.Next(0, 10000));
            w = world;
            ChestInventory.TryAddItem(RandomGeneratorOfItems(r));
        }
        public Chest(Graphics graph, bool isW, int X_pos, int Y_pos, World world, List<ItemObject> itemToAdd) : base(graph, isW, X_pos, Y_pos)
        {
            r = new Random();
            ChestInventory = new InventoryObject(uint.MaxValue, (ulong)r.Next(0, 10000));
            w = world;
            foreach (ItemObject Item in itemToAdd)
            {
                ChestInventory.TryAddItem(Item);
            }
        }

        public void Interact(Player p)
        {
            foreach(ItemObject Item in ChestInventory.Items)
            {
                p.Invetory.TryAddItem(Item);
            }
            if(w.TryDeleteObject(Y, X))
                p.Invetory.AddMoney(ChestInventory.Money);

            
        }

        ItemObject RandomGeneratorOfItems(Random r)
        {
            string name = "";
            name += VariantsOfName[r.Next(0, VariantsOfName.Length - 1)] + " ";
            name += VariantsOfWeapon[r.Next(0, VariantsOfWeapon.Length - 1)] + " ";
            name += VariantsOfEnding[r.Next(0, VariantsOfEnding.Length - 1)];
            var PossibleRarities = Enum.GetValues(typeof(rarity));
            rarity ItemRarity = (rarity)PossibleRarities.GetValue(r.Next(0, PossibleRarities.Length - 1));

            return new ItemObject(ItemRarity,name,(uint)r.Next(30,1000),(uint)r.Next(10,1000));
        }

    }
}
