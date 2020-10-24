using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes.Gameobjects
{
    class Button : GameObject, IInteractable
    {
        IChangingStateObject ToInteractWith;
        public Button(Graphics graph, bool isW, int X_pos, int Y_pos, IChangingStateObject InteractWith) : base(graph, isW, X_pos, Y_pos)
        {
            ToInteractWith = InteractWith;
            Description = "Нажми на это и произойдет магия";
        }

        public void Interact(Player player)
        {
            ToInteractWith.ChangeState();
        }
    }
}

