using System;
using System.Threading;
using System.Diagnostics;

namespace DodgeGame
{
    public class Game
    {
        /*Game() Constructor
        Constructors read the entire class before 'building',
        So you can place at top, and it will read variables below it
        */
        public Game()
        {
            //Clearing the screen at game start
            Console.Clear();

            //Instantiate a Unit that will represent the player.
            //new Unit has () because you can pass variables in (to the constructor)
            playerUnit = new PlayerUnit(15, 17, "H"); //Unit(15, 20, "H");
            /*
            allowed since 'set' for X was set to public
            secretly setting _x, allowing for validity checking
            */
            //playerUnit.X = 15;
            //playerUnit.Y = 10;
            //playerUnit.UnitGraphic = "H"; 
            //also allowed since 'get' for X was set to public, reading _x
            if (playerUnit.X > 20)
            {
                //Do something
            }

            //Instantiate a single enemy Unit --Obsolete with [] enemies
            //enemyUnit = new EnemyUnit(Console.WindowWidth - 1, 17, "M");

            /*Create many Array that will hold our enemies
            When instiating an array, must list size
            --In most languages, Arrays have a fixed size after creation
            */
            enemyUnits = new Unit[numEnemies];
            //Right now, enemyUnits is created, but each slot is empty

            //Random number generator
            random = new Random();
            //Set score at beginning to 0
            Score = 0;
            
            for(int i = 0; i < enemyUnits.Length; i++)
            {
                //Setting a random row for the enemy to appear
                int row = random.Next(0, Console.WindowHeight - 1);
                //Filling each index with it's own EnemyUnit
                enemyUnits[i] = new EnemyUnit(Console.WindowWidth - 1, row, "M");
            }

            //Making an array of UpSlopeEnemies
            upSlopeEnemies = new Unit[numEnemies / 5]; //Want a lot fewer of these

            for (int i = 0; i < upSlopeEnemies.Length; i++)
            {
                //Setting a random row for the enemy to appear
                int yStart = Console.WindowHeight - 1;
                int xStart = random.Next(Console.WindowWidth / 4, Console.WindowWidth - 1);
                //Filling each index with it's own EnemyUnit
                upSlopeEnemies[i] = new UpSlopeEnemy(xStart, yStart, "N");
            }

            stopwatch = new Stopwatch();

        }
        //End Game() Constructor

        private static Random random;
        public static int Score;

        //Just like a real stopwatch, can stop and start up, but can only reset
        //Really handy for a game when you want to pause it
        private Stopwatch stopwatch;

        //Privatizes these instances so no one can change their values
        private Unit playerUnit;
        //private Unit enemyUnit; obsolete with multiple enemies on screen
        private int numEnemies = 30;
        private Unit[] enemyUnits; //Unit[] = An Array of enemy units
        private Unit[] upSlopeEnemies;

        /*
        So even though these are marked as the type of their parents, since
        we marked the parent Update() as virtual, and child () as override,
        the cpu will still look at child ()'s before parent's ()'s
        */

        /*
        public Static - shared by the class as a whole, no longer belonging to an
        instance. Don't even need an instance to access a static variable
        --Just call the class with a Capital letter (ex: Game.RandomNum(0, 9))
        */
        public static int RandomNum(int min, int max)
        {
            return random.Next(min, max);
        }

        public void Run()
        {

            stopwatch.Start();
            long timeAtPreviousFrame = stopwatch.ElapsedMilliseconds; //start @ 0

            /*
            Temporary - Let's try to set a fixed frame rate
            --Old school approach
            */
            //int desiredFPS = 100; //Obsolete by Stopwatch
            

            //Infinite Loop, at simple code, is running as fast as it can
            while (true)
            {
                //(int) is a cast, telling the operation that yes, ElapsedMilliseconds
                //is in 'long' datatype, but we just need it in int
                int deltaTime = (int)(stopwatch.ElapsedMilliseconds - timeAtPreviousFrame);
                timeAtPreviousFrame = stopwatch.ElapsedMilliseconds;

                //First, Update all units
                playerUnit.Update(deltaTime); //PlayerUnit has its own Update()
                //Update and check for collisions on enemyUnits
                for(int i = 0; i < enemyUnits.Length; i++)
                {
                    //update the enemy
                    enemyUnits[i].Update(deltaTime); //Relies on parent Unit Update()
                    
                    //Now that all units have moved, let's see if the player is colliding with an enemy
                    if (playerUnit.IsCollidingWith(enemyUnits[i]))
                    {
                        GameOver(); //If so, then game over
                        return; //Break out of the game loop
                    } 
                }

                //Update and check for collisions on upSlopeEnemies
                for(int i = 0; i < upSlopeEnemies.Length; i++)
                {
                    upSlopeEnemies[i].Update(deltaTime);

                    if (playerUnit.IsCollidingWith(upSlopeEnemies[i]))
                    {
                        GameOver(); //If so, then game over
                        return; //Break out of the game loop
                    }
                }

                //Now Draw both units with Draw()
                playerUnit.Draw();
                //Another way to loop through all enemyUnits, foreach()
                //foreach hits every element (u) in an Array (enemyUnits)
                foreach(Unit u in enemyUnits)
                {
                    u.Draw();
                }

                foreach(Unit p in upSlopeEnemies)
                {
                    p.Draw();
                }

                //Draw the score in bottom left
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                Console.Write("SCORE: " + Game.Score);
                

                //Now we need to wait for the correct time in FPS
                //This is not the ideal solution, and causes player input lag
                //Thread.Sleep(deltaTime); //Obsolete by Stopwatch

                if(deltaTime < 5)
                {
                    //Lets just do a tiny sleep to avoid running as fast as CPU
                    //Prevents running a a million FPS
                    Thread.Sleep( 5 ); //in milliseconds
                }
                

                //Example functions to act on other Units
                //playerUnit.DoSomethingToOtherUnit(enemyUnits);
                //enemyUnits.DoSomethingToOtherUnit(playerUnit);
            }
        }
        //End Run()

        void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game Over! Final Score: " + Game.Score);
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
    }
}
