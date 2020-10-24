using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes.Gameobjects
{
    class StartPoint : GameObject
    {
        public StartPoint(Graphics graph, bool isW, int X_pos, int Y_pos, ref Player p) : base(graph, isW, X_pos, Y_pos)
        {
            Description = "Тут начинаеться ваше приключение";//надо написать что то прикольное типо лора
            p.X = X_pos;
            p.Y = Y_pos;
            p.OnThisObject = this;
        }
    }
}
