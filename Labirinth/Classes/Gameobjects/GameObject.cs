using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes.Gameobjects
{
    class GameObject
    {
        public Graphics graphics;
        public bool isWalkable;
        public string type;
        public string Description;



        public int X { get; private set; }
        public int Y { get; private set; }

        public GameObject(Graphics graph, bool isW, int X_pos, int Y_pos)
        {
            Description = "";
            graphics = graph;
            isWalkable = isW;
            X = X_pos;
            Y = Y_pos;
        }
    }
}
