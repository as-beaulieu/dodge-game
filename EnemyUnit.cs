using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeGame
{
    class EnemyUnit : Unit
    {
        /*
        This is a generic enemy unit that moves from right to left on 
        the screen. Then disappears
        */

        public EnemyUnit(int x, int y, string UnitGraphic) : base(x, y, UnitGraphic)
        {
            //Be dormant for up to one second
            sleepForMS = Game.RandomNum(0, 1000); //In milliseconds
            //And change up how fast some are
            TimeBetweenMoves = Game.RandomNum(25, 60); //bigger spread
        }

        //If TimeBetweenMoves ++, then enemy speed --
        public int TimeBetweenMoves; //In Milliseconds
        private int timeSinceLastMove = 0;

        //A lazy way to create enemies at different times instead of all at once
        private int sleepForMS = 0;

        public override void Update( int deltaTime )
        {
            sleepForMS -= deltaTime;
            if(sleepForMS > 0)
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

            //If we get here, it means we need to make a move
            timeSinceLastMove -= TimeBetweenMoves; //'Reset' the counter


            //These enemies simply move from right to left on screen
            if( X > 0)
            {
                X = X - 1;
            }
            else
            {
                //We are at our move limit
                //So let's move back to the right
                X = Console.WindowWidth - 1;
                //And Randomize our row (Do not spawn on bottom row)
                Y = Game.RandomNum(0, Console.WindowHeight - 1);
                //And sleep for a moment, to break up the creation more
                sleepForMS = Game.RandomNum(0, 2000); //in milliseconds

                //Every time an enemy respawns, we get a little faster
                //by reducing TimeBetweenMoves
                TimeBetweenMoves = (int)(TimeBetweenMoves * 0.95);
                //But lets keep a minimum time limit
                if (TimeBetweenMoves < 30)
                {
                    //Keep time to something sane.
                    TimeBetweenMoves = 30;
                    //Want something much slower now that other types
                    //are in the game
                }

                //Give the player a point when the enemy 'leaves' the map
                //Game.Score = Game.Score + 1;
                if (TimeBetweenMoves > 100)
                {
                    //Standard Score
                    Game.Score += 1;
                }
                else if (TimeBetweenMoves > 50 && TimeBetweenMoves <= 100 )
                {
                    //Bonus for surviving faster enemies
                    Game.Score += 2;
                }
                else
                {
                    //Okay, big points for fast enemies
                    Game.Score += 3;
                }
                
            }
            
            
            //If they go out of bounds, then we delete instance

            /*
            --Now that AI calculation is done, lets call our base class
            Update() in case it has any important work to do.
            --'base' just means to go up one step on hierarchy
            */
            base.Update(deltaTime);
        }
        //End Update()

        override public void Draw()
        {
            if(sleepForMS > 0)
            {
                //still asleep, so do not draw us.
                return;
            }
            base.Draw(); //We are awake now, draw us.
        }
        //End Draw()
    }
}
