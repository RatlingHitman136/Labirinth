using Labirinth.Classes.Itemobjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes.Commands
{
    class CommandReader
    {
        public bool TryActivateCommand(string command, Player p, out string output)
        {
            char[] separators = { ' ' };
            output = "";

            string[] CommandParts = command.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if(CommandParts.Length == 0)
            {
                return false;
            }

            switch (CommandParts[0])
            {
                case "Sell":
                    int indexS;
                    if (CommandParts.Length == 2 && int.TryParse(CommandParts[1], out indexS))
                    {

                        output = SellCommand(p.Invetory, indexS - 1);
                    }
                    break;
                case "Merge":
                    int index1, index2;
                    if (CommandParts.Length == 3 && int.TryParse(CommandParts[1], out index1) && int.TryParse(CommandParts[2], out index2))
                    {
                        output = MergeCommand(p.Invetory, index1 - 1, index2 - 1);
                    }
                    break;
                case "Description":
                    int indexD;
                    if (CommandParts.Length == 2 && int.TryParse(CommandParts[1], out indexD))
                    {
                        output = DescriptionCommand(p.Invetory, indexD - 1);
                    }
                    break;
            }

            return false;
        }

        [Description("Вы пытаетесь продать предмет")]
        string SellCommand(InventoryObject inv, int index)
        {
            string output;
            string name;
            if (inv.TrySellItem(index, out name))
            {
                output = "Вы успешно продали " + name + ", тепер у вас на счету " + inv.Money + ".";
            }
            else
                output = "Ошибка, вы что то сделали не так.";

            return output;
        }

        [Description("Вы пытаетесь скрестить первый предмет со вторым, в случае провала (разная редкость) вы получите первый предмет низшего уровня и он обезцениваеться. Также шанс на провал зависит от уровня предмета")]
        string MergeCommand(InventoryObject inv, int index1, int index2)
        {
            if (index1 < 0 || index1 >= inv.Items.Count || index2 < 0 || index2 >= inv.Items.Count)
            {
                return "Операция отменена изза неверного ввода номера предмета";
            }

            ItemObject NewItem, Item1, Item2;
            Item1 = inv.Items[index1];
            Item2 = inv.Items[index2];

            if (Item1.Rarity != Item2.Rarity)
            {
                NewItem = new ItemObject(rarity.Common, Item1.name, Item1.mass, 0, Item1.description);
                return "Операция провалена, так как было нарушено правило скрещивания, довольствуйтесь тем, что есть";
            }
            else
            {

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

                NewItem = new ItemObject(newRarity, Item1.name, Item1.mass, Item1.cost * moneyMultiplier, Item1.description);

            }

            inv.TryDeleteItem(index2);
            inv.TryDeleteItem(index1);
            inv.TryAddItem(NewItem);

            return "Поздравляю, вам удалось. Теперь вы получили намного более мощный и ценный предмет";
        }

        
        [Description("Вы пытаетесь получить описание предмета по индексу")]
        private string DescriptionCommand(InventoryObject inv, int index)
        {
            string output = "";
            if (index < 0 || index > inv.Items.Count - 1)
                return output;
            ItemObject Item = inv.Items[index];
            output = "Описание предмета " + Item.Rarity.ToString() +" "+ Item.name + ": " + Item.description;

            return output;
        }

    }
}
