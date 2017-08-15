using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeGame
{
    class DownSlopeEnemy : Unit
    {
        //Constructor
        public DownSlopeEnemy(int x, int y, string UnitGraphic) : base (x, y, UnitGraphic)
        {
            sleepForMS = Game.RandomNum(1000, 2000); //in milliseconds
            TimeBetweenMoves = Game.RandomNum(100, 300); //Slow movers
        }
        //End Constructor

        //If TimeBetweenMoves ++, then enemy speed --
        public int TimeBetweenMoves; //In Milliseconds
        private int timeSinceLastMove = 0;
        private int sleepForMS = 0;

        public override void Update(int deltaTime)
        {
            sleepForMS -= deltaTime;
            if (sleepForMS > 0)
            {
                //We are still asleep so do nothing
                return;
            }

            //Has enough time passed that we should be moving?
            timeSinceLastMove += deltaTime;

            if (timeSinceLastMove < TimeBetweenMoves)
            {
                //Not enough time has passed, lets not do anything
                return;
            }

            timeSinceLastMove -= TimeBetweenMoves; //'Reset' the counter

            //These enemies move diagonally down and left
            if (    X > 0 
                    && Y < Console.WindowHeight - 1
                )
            {
                X -= 1; //Left
                Y += 1; //Up
            }
            else
            {
                //We are at our move limit, so start again
                //Randomizing X and Y starting locations
                X = Game.RandomNum(Console.WindowWidth / 4, Console.WindowWidth - 1);
                Y = 1;

                //And sleep for a moment, to break up the creation more
                sleepForMS = Game.RandomNum(0, 2000); //in milliseconds

                //Want these to be slow drifters
                TimeBetweenMoves = Game.RandomNum(100, 400);

                //Give the player a point when the enemy 'leaves' the map
                Game.Score += 1; //No bonus since don't move too fast


                base.Update(deltaTime);
            }
        }
        //End Update()

        override public void Draw()
        {
            if (sleepForMS > 0)
            {
                //still asleep, so do not draw us.
                return;
            }
            base.Draw(); //We are awake now, draw us.
        }
        //End Draw()
    }
}

