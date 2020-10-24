using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes
{
    class Player
    {
        public char graphics { get; private set; }
        public Gameobjects.GameObject OnThisObject;
        public int X;
        public int Y;

        public InventoryObject Invetory;

        public Player(char graph, uint M_Mass)
        {
            graphics = graph;
            Invetory = new InventoryObject(M_Mass);
        }
    }
}
