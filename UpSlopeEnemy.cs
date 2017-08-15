using System;


namespace DodgeGame
{
    class UpSlopeEnemy : Unit
    {
        //Constructor
        public UpSlopeEnemy(int x, int y, string UnitGraphic) : base (x, y, UnitGraphic)
        {
            sleepForMS = Game.RandomNum(1000, 2000); //in milliseconds
            TimeBetweenMoves = Game.RandomNum(100, 300); //Slow movers
        }

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

            //These enemies move diagonally up and left
            if (X > 0 && Y > 0)
            {
                X -= 1;
                Y -= 1;
            }
            else
            {
                //We are at our move limit, so start again
                //Randomizing X and Y starting locations
                X = Game.RandomNum(Console.WindowWidth / 4, Console.WindowWidth - 1);
                //Y = Game.RandomNum(Console.WindowHeight / 4, Console.WindowHeight - 1);
                Y = Console.WindowHeight - 1;

                //And sleep for a moment, to break up the creation more
                sleepForMS = Game.RandomNum(0, 2000); //in milliseconds

                //Want these to be slow drifters
                TimeBetweenMoves = Game.RandomNum(100, 300); ;

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

