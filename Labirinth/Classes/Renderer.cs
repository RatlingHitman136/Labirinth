using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using Labirinth.Classes.Gameobjects;

namespace Labirinth.Classes
{
    class Renderer //не забыть тут очистить код от лишней фигни там где есть коменты
    {
        string[] PrevDescriptionLinesToRender = { };
        string[] PrevCommandOutputLinesToRender = { };

        public void Render(World worldToRender,Player player, bool isFirst, string CommandOutput)
        {
            RenderBound(worldToRender);
            RenderInsides(worldToRender, player ,isFirst);
            RenderPLayer(player);

            string TextToRender = "";
            AddObjectDescription(ref TextToRender, player.OnThisObject, worldToRender, 30);
            AddInventoryInfo(ref TextToRender, player, worldToRender, 30);
            RenderDescriptionTextInfo(TextToRender, worldToRender);
            RenderCommandTextInfo(CommandOutput, worldToRender);
            Console.SetCursorPosition(0, worldToRender.size_y + 2);
        }

        void RenderBound(World worldToRender)
        {
            Console.BackgroundColor = worldToRender.externalWallGr.BgColor;
            Console.ForegroundColor = worldToRender.externalWallGr.FColor;
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < worldToRender.size_x + 2; i++)
                Console.Write(worldToRender.externalWallGr.symbol);
            Console.SetCursorPosition(0, worldToRender.size_y + 1);
            for (int i = 0; i < worldToRender.size_x + 2; i++)
                Console.Write(worldToRender.externalWallGr.symbol);
            for (int i = 1; i <= worldToRender.size_y; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(worldToRender.externalWallGr.symbol);
            }
            for (int i = 1; i <= worldToRender.size_y; i++)
            {
                Console.SetCursorPosition(worldToRender.size_x + 1,i);
                Console.Write(worldToRender.externalWallGr.symbol);
            }
            Console.ResetColor();
        }
        void RenderInsides(World worldToRender, Player p, bool isF)
        {
            for (int x = 0; x < worldToRender.size_x; x++)
            {
                for (int y = 0; y < worldToRender.size_y; y++)
                {
                    if (worldToRender.All_Objects[y, x] != null)
                        RenderObject(worldToRender.All_Objects[y, x]);
                    else
                        RenderFreeSpace(x, y);
                }
            }
        }
        void RenderObject(GameObject objectToRender)
        {
            Console.SetCursorPosition(objectToRender.X, objectToRender.Y);
            Console.BackgroundColor = objectToRender.graphics.BgColor;
            Console.ForegroundColor = objectToRender.graphics.FColor;
            Console.Write(objectToRender.graphics.symbol);
            Console.ResetColor();
        }
        void RenderPLayer(Player player)
        {
            Console.SetCursorPosition(player.X, player.Y);
            if (player.OnThisObject != null)
            {
                Console.BackgroundColor = player.OnThisObject.graphics.BgColor;
                Console.ForegroundColor = player.OnThisObject.graphics.FColor;
            }
            Console.Write(player.graphics);
            Console.ResetColor();
        }
        void AddObjectDescription(ref string TextToRender, GameObject obj, World worldToRender,int maxLineLength)
        {
            if (obj == null) return;
            string[] WordsToRender = obj.Description.Split(' ');
            int CharCount = 0;
            for(int i =0;i<WordsToRender.Length;i++)
            {
                CharCount += WordsToRender[i].Length + 1;
                TextToRender += (WordsToRender[i] + " ");
                if(CharCount> maxLineLength)
                {
                    CharCount = 0;
                    TextToRender += "~";
                }
            }
            if (obj.Description!="")
            {
                if (TextToRender[TextToRender.Length - 1] != '~')
                    TextToRender += "~";
                TextToRender += " ~";
            }
        }
        void AddInventoryInfo(ref string TextToRender, Player p, World worldToRender, int maxLineLength)
        {
            Console.SetCursorPosition(worldToRender.size_x + 5, Console.CursorTop + 1);
            TextToRender += ("Ваш инвентарь заполнен на " + p.Invetory.Cur_mass +" из "+ p.Invetory.Max_mass+
                ". ~У вас в кармане "+ p.Invetory.Money+ ". " + " ~Вы с собой носите: ");
            for (int i = 0; i < p.Invetory.Items.Count; i++)
            {
                TextToRender += "~  " + (i + 1) + " " + p.Invetory.Items[i].Rarity.ToString() + " " + p.Invetory.Items[i].name;
            }
        }

        void RenderFreeSpace(int x, int y)
        {
            Console.SetCursorPosition(x+1, y+1);
            Console.ResetColor();
            Console.Write(" ");
        }

        void RenderDescriptionTextInfo(string TextToRender, World worldToRender)
        {
            Console.SetCursorPosition(worldToRender.size_x + 4, 1);
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < PrevDescriptionLinesToRender.Length; i++)
            {
                Console.Write(PrevDescriptionLinesToRender[i]);
                Console.SetCursorPosition(worldToRender.size_x + 4, Console.CursorTop + 1);
            }

            Console.ResetColor();

            Console.SetCursorPosition(worldToRender.size_x + 4, 1);
            string[] LinesToRender = TextToRender.Split('~');
            for(int i =0;i<LinesToRender.Length;i++)
            {
                Console.Write(LinesToRender[i]);
                Console.SetCursorPosition(worldToRender.size_x + 4, Console.CursorTop+1);
            }
            PrevDescriptionLinesToRender = LinesToRender;
        }//Как раз вот тут надо исправить
        void RenderCommandTextInfo(string TextToRender,World worldToRender)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, worldToRender.size_y + 2);
            int letterCounter = 0;
            for (int i = 0; i < PrevCommandOutputLinesToRender.Length; i++)
            {
                if (letterCounter + PrevCommandOutputLinesToRender[i].Length > worldToRender.size_x)
                {
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                    letterCounter = 0;
                }
                Console.Write(PrevCommandOutputLinesToRender[i] + " ");
                letterCounter += PrevCommandOutputLinesToRender[i].Length + 1;
            }
            Console.ResetColor();
            Console.SetCursorPosition(0, worldToRender.size_y+2);
            string[] WordsToRender = TextToRender.Split(' ');
            letterCounter = 0;
            for(int i =0;i< WordsToRender.Length;i++)
            {
                if (letterCounter + WordsToRender[i].Length > worldToRender.size_x)
                {
                    Console.SetCursorPosition(0,Console.CursorTop+1);
                    letterCounter = 0;
                }
                Console.Write(WordsToRender[i] + " ");
                letterCounter += WordsToRender[i].Length + 1;
            }
            PrevCommandOutputLinesToRender = WordsToRender;
        }////Как раз вот тут надо исправить

    }
}
