using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes
{
    struct Graphics
    {
        public char symbol;
        public ConsoleColor BgColor;
        public ConsoleColor FColor;

        public Graphics(char s, ConsoleColor BgC, ConsoleColor FC)
        {
            symbol = s;
            BgColor = BgC;
            FColor = FC;
        }
        public Graphics(char s)
        {
            symbol = s;
            Console.ResetColor();
            BgColor = Console.BackgroundColor;
            FColor = Console.ForegroundColor;
        }
    }
}
