using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeGame
{
    /*
    This is a subclass of Unit. Want it to have all properties and 
    behaviors of Unit, but then have its more specific behaviors here
    --Subclasses have a ':' reference that points to parent class it
    belongs to
    */
    public class PlayerUnit : Unit
    {
        /*
        When you instantiate a subclass constructor, it still has to satisfy
        the base constructor of the parent class
        -- So if your subclass constructor passes no values, if your parent
        constructor needs values, you have to address those needed values
        -- :base() calls the parents constructor
        --Note how subclass parameters pass up to the parent class constructor
        */
        public PlayerUnit(int x, int y, string UnitGraphic) : base(x, y, UnitGraphic)
        {

        }

        /*
        What if PlayerUnit doesn't have an Update() ??
        --If a child doesn't have a function, it will then jump up to look
        at the parent class for that function.
        --So let's make one for the child
        --child gets override, so when a child's () is called, and there is
        a parent () of same name, the child () gets picked instead
        --Specifically override ()'s overrides virtual ()'s of same name
        */
        override public void Update( int deltaTime )
        {
            /*
            When the player update() is called, we would like to execute
            this INSTEAD of our parent class' Update()
            --This is overriding the parent
            */
            //throw new Exception("We are in PlayerUnit::Update()!");
            /*
            ^^ A test. With no other code, this does not appear to run
            until we establish override and virtual on ()'s
            */

            //Has the user pressed a key? 
            if (Console.KeyAvailable == true)
            {
                /*If so, lets move based on input
                ReadKey returns as soon as a key is hit
                Returns a ConsoleKeyInfo
                --ReadKey() can take no parameter, or it can take an 'intercept'
                --This intercepts the normal console operation, which is to display
                on the console the key hit. We don't want this in a game
                --Passing 'true' enables this intercept
                */
                ConsoleKeyInfo cki = Console.ReadKey(true);

                // If the key character is a w or up, then move up
                /*
                -- KeyChar holds a single character, not a string
                -- In C#, single character variables are listed with ''
                Not "" like for string, which is a series of characters.
                --With ConsoleKey, uses the actual key pressed on keyboard
                */
                switch (cki.Key)
                {
                    //C# does not let you fall through without a break...
                    case ConsoleKey.UpArrow:
                    //Unless there is nothing here
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.W:
                        if(Y > 0)
                        {
                            Y = Y - 1;
                        }
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.DownArrow:
                        if(Y < Console.WindowHeight - 1)
                        {
                        Y = Y + 1;
                        }
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.LeftArrow:
                        if(X > 0)
                        {
                        X = X - 1;
                        }
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.RightArrow:
                        if(X < Console.WindowWidth - 1)
                        {
                            X = X + 1;
                        }
                        break;
                    //Diagonal Movements
                    case ConsoleKey.NumPad9:
                        if( X < Console.WindowWidth -1
                            || Y > 0
                            )
                        {
                        X = X + 1;
                        Y = Y - 1;
                        }
                        break;
                    case ConsoleKey.NumPad3:
                        if (X < Console.WindowWidth - 1
                            || Y < Console.WindowHeight - 1
                            )
                        {
                            X = X + 1;
                            Y = Y + 1;
                        }
                        break;
                    case ConsoleKey.NumPad1:
                        if (X > 0
                            || Y < Console.WindowHeight - 1
                            )
                        {
                            X = X - 1;
                            Y = Y + 1;
                        }
                        break;
                    case ConsoleKey.NumPad7:
                        if (    X > 0
                            ||  Y > 0
                            )
                        {
                            X = X - 1;
                            Y = Y - 1;
                        }
                        break;
                }


                /*
                Do Player actions here!
                --Now that keyboard input is done, lets call our base class
                Update() in case it has any important work to do.
                --'base' just means to go up one step on hierarchy
                */
                base.Update( deltaTime );
            }
        }
    }
}
