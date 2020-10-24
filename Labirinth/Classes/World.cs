using System;
using System.Collections.Generic;

using Labirinth.Classes.Gameobjects;

namespace Labirinth.Classes
{
    class World
    {
        public int size_x { get; private set; }
        public int size_y { get; private set; }

        public GameObject[,] All_Objects;

        public Graphics externalWallGr;

        public World(Graphics EWGr, int xs = 1, int ys = 1)
        {
            size_x = xs;
            size_y = ys;
            All_Objects = new GameObject[size_y, size_x];
            externalWallGr = EWGr;
        }

        public bool TryAddObject(GameObject objectToAdd)
        {
            if(!(objectToAdd.X<1 || objectToAdd.X >size_x || objectToAdd.Y < 1 || objectToAdd.Y > size_y) && All_Objects[objectToAdd.Y-1, objectToAdd.X-1] == null)
            {
                All_Objects[objectToAdd.Y-1, objectToAdd.X-1] = objectToAdd;
                return true;
            }
            return false;
        }

        public bool TryDeleteObject(int X_pos, int Y_pos)
        {
            if (X_pos > 0 && X_pos <= size_x && Y_pos > 0 && Y_pos <= size_y)
            {
                All_Objects[X_pos-1, Y_pos-1] = null;
                return true;
            }
            return false;
        }

    }
}
