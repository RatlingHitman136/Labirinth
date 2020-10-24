using Labirinth.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth
{
    class Program//НИЧЕГО НЕ МЕНЯТЬ. ТУТ НАПИСАНО ВСЕ ЧТО НАДО. ДАЖЕ НЕ ПЫТАЙТЕСЬ ЧТО ТО ПОМЕНЯТЬ. ВЫ БУДЕТЕ ЖАЛЕТЬ ОБ ЭТОМ. ВСЮ СВОЮ ОСТАВШУЮСЯ НЕДОЛГУ ЖИЗНЬ И УМРЕТЕ В ОГОНИЯХ
    {
        static void Main(string[] args)
        {
            Game OurBestGame = new Game();
            OurBestGame.Start();
            while (OurBestGame.isGameOn)
            {
                OurBestGame.Update();
            }

        }

    }
}