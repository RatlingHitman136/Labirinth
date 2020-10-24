using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes.Itemobjects
{

    enum rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
    }


    class ItemObject
    {
        public string name { get; private set; }
        public string description { get; private set; }

        public rarity Rarity { get; private set; }

        public uint mass { get; private set; }
        public uint cost { get; private set; }

        public ItemObject(rarity R, string n, uint m, uint c, string d = "")
        {
            name = n;
            description = d;
            Rarity = R;
            mass = m;
            cost = c;
        }


        public static ItemObject operator +(ItemObject Item1, ItemObject Item2)
        {
            if (Item1 == null || Item2 == null)
                return null;

            if (Item1.Rarity != Item2.Rarity)
                return new ItemObject(rarity.Common, Item1.name, Item1.mass, Item1.cost, Item1.description);


            Random r = new Random();
            double ChanceOnFail = r.NextDouble();
            rarity newRarity = rarity.Common;
            uint moneyMultiplier = 2;

            switch (Item1.Rarity)
            {
                case rarity.Common:
                    if (ChanceOnFail < 0.1) moneyMultiplier = 0;
                    else newRarity = rarity.Uncommon;
                    break;

                case rarity.Uncommon:
                    if (ChanceOnFail < 0.2) moneyMultiplier = 0;
                    else newRarity = rarity.Rare;
                    break;

                case rarity.Rare:
                    if (ChanceOnFail < 0.3) moneyMultiplier = 0;
                    else newRarity = rarity.Epic;
                    break;

                case rarity.Epic:
                    if (ChanceOnFail < 0.4) moneyMultiplier = 0;
                    else newRarity = rarity.Legendary;
                    break;
            }

            return new ItemObject(newRarity, Item1.name, Item1.mass, Item1.cost * moneyMultiplier, Item1.description);
        }

    }
}
