using System;
using System.Collections.Generic;
using Labirinth.Classes;
using Labirinth.Classes.Commands;
using Labirinth.Classes.Gameobjects;
using Labirinth.Classes.Itemobjects;

namespace Labirinth
{

    class Game
    {
        Renderer r;
        World Level0;
        World LevelRandom;
        Player p;
        CommandReader CReader;

        public bool isGameOn {get; private set;}

        public void Start()
        {
            isGameOn = true;

            r = new Renderer();
            p = new Player('@', int.MaxValue);
            CReader = new CommandReader();

            p.Invetory.TryAddItem(new ItemObject(rarity.Legendary, "Sword of eternity", 10, 100, "Самый мощный мечь за всю историю. Его урон не может быть исчислен"));

            #region GRaphics creation

            Graphics wallGr = new Graphics(' ', ConsoleColor.Gray, ConsoleColor.Black);
            Graphics doorOpenGr = new Graphics(' ');
            Graphics doorCloseGr = new Graphics(' ', ConsoleColor.DarkGray, ConsoleColor.White);
            Graphics buttonGr = new Graphics('o');
            Graphics startpointGr = new Graphics('s');
            Graphics endpointGr = new Graphics('e');
            Graphics chestGr = new Graphics('c');


            #endregion

            #region Adding Objects To World\

            Level0 = new World(wallGr,20, 10);
            
            Level0.TryAddObject(new GameObject(wallGr, false, 1, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 2, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 3, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 4, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 5, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 6, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 8, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 9, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 10, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 11, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 12, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 13, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 14, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 15, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 16, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 17, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 18, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 19, 6));
            Level0.TryAddObject(new GameObject(wallGr, false, 20, 6));

            Level0.TryAddObject(new StartPoint(startpointGr, true, 1, 1, ref p));
            Level0.TryAddObject(new EndPoint(endpointGr, true, 20, 10, this));

            Door door = new Door(doorCloseGr, doorOpenGr, false, 7, 6);
            Level0.TryAddObject(door);
            Level0.TryAddObject(new Button(buttonGr, true, 8, 1, door as IChangingStateObject));

            List<ItemObject> Chest1 = new List<ItemObject>();
            Chest1.Add(new ItemObject(rarity.Epic, "Bow of death", 7, 500));
            Chest1.Add(new ItemObject(rarity.Epic, "Dragon`s sword", 40, 1000));
            Level0.TryAddObject(new Chest(chestGr, true, 3, 1, Level0, Chest1));

            Level0.TryAddObject(new Chest(chestGr, true, 3, 3, Level0));
            #endregion

            #region Generate Random World

            LevelRandom = new World(wallGr, 20,20);
            Random Rand = new Random();

            for(int i =0;i<40;i++)
            {
                int x = Rand.Next(2, LevelRandom.size_x - 2);
                int y = Rand.Next(2, LevelRandom.size_y - 2);

                LevelRandom.TryAddObject(new GameObject(wallGr,false,x,y));
            }

            #endregion
            
            r.Render(Level0, p, true,"");
        }

        public void Update()
        {
            string CommandOutput;
            InputHandler(Level0,out CommandOutput);
            r.Render(Level0, p, false, CommandOutput);
        }

        void TryMove(World world, int change_x, int change_y)
        {
            bool canMove = (change_x + p.X) > 0 && (change_x + p.X) <= world.size_x && (change_y + p.Y) > 0 && (change_y + p.Y) <= world.size_y;

            if (canMove && world.All_Objects[change_y + p.Y - 1, change_x + p.X - 1] != null && !world.All_Objects[change_y + p.Y - 1, change_x + p.X - 1].isWalkable)
                canMove = false;

            if (canMove)
            {
                p.X += change_x;
                p.Y += change_y;
                p.OnThisObject = world.All_Objects[p.Y - 1, p.X - 1];
            }

        }

        void TryUpdatePlayerPosition(World world,ConsoleKey key)
        {
            if (key == ConsoleKey.D)
            {
                TryMove(world,1, 0);
            }
            if (key == ConsoleKey.W)
            {
                TryMove(world,0, -1);
            }
            if (key == ConsoleKey.S)
            {
                TryMove(world,0, 1);
            }
            if (key == ConsoleKey.A)
            {
                TryMove(world ,- 1, 0);
            }
        }

        bool TryToInteract(ConsoleKey key)
        {
            if (key == ConsoleKey.F)
            {
                (p.OnThisObject as IInteractable)?.Interact(p);
                return true;
            }
            return false;
        }

        void TryToActivateCommand(ConsoleKey key, out string CommandOutput)
        {
            CommandOutput = "";
            if (key == ConsoleKey.Enter)
            {
                string Command = Console.ReadLine();
                CReader.TryActivateCommand(Command,p, out CommandOutput);
            }
        }

        public void InputHandler(World w, out string CommandOutput)
        {
            ConsoleKey key = Console.ReadKey().Key;
            TryUpdatePlayerPosition(w, key);
            TryToInteract(key);
            TryToActivateCommand(key, out CommandOutput);
        }

      
        public void StopGame()
        {
            isGameOn = false;
        }

    }
}
