using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes.Gameobjects
{
    class Door : GameObject, IChangingStateObject
    {
        Graphics graphO, graphC;
        public Door(Graphics graphClose, Graphics graphOpen, bool isW, int X_pos, int Y_pos) : base(graphClose, isW, X_pos, Y_pos)
        {
            graphC = graphClose;
            graphO = graphOpen;
            if (isW) { graphics = graphO; }
            Description = "";
        }

        public void DoorSetState(bool state)
        {
            graphics = state ? graphO : graphC;
            isWalkable = state;
        }
        public void ChangeState()
        {
            if (isWalkable)
            {
                isWalkable = false;
                graphics = graphC;
            }
            else
            {
                graphics = graphO;
                isWalkable = true;
            }
        }
    }
}
