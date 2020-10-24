using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirinth.Classes.Gameobjects
{
    class EndPoint : GameObject, IInteractable
    {
        Game gameToStop;
        public EndPoint(Graphics graph, bool isW, int X_pos, int Y_pos, Game gameToStopPar) : base(graph, isW, X_pos, Y_pos)
        {
            gameToStop = gameToStopPar;
            Description = "Ну что же вы прошли этот уровень, настала пора перейти на новый. Удачи странник.";//надо написать что то прикольное типо лора
        }

        public void Interact(Player player)
        {
            gameToStop.StopGame();
        }
    }
}
