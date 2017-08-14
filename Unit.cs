using System;


namespace DodgeGame
{
    /*
    'abstract' means this class can not create a Unit by itself
    --All classes must be the subclasses, like PlayerUnit, or EnemyUnit,
    or Neutral Unit, etc.
    */
    abstract public class Unit
    {
        /*
        Constructor is a special () that is called when creating 
        a new instance of an object of a class.
        --If you don't specify any constructors for a class automatically
        get an empty, public constructor that anyone can instantiate
        --Constructor has the same name as the class
        --Constructors do not have a return type, not even void
        */
        public Unit() //--A default constructor--
        {
            this.X = 0;
            this.Y = 0;
            this.UnitGraphic = "@";
        }
        //Can declare a variable in the constructor
        //So it has a default value if not passed into the constructor
        public Unit (int x, int y, string token = "@")
        {
            this.X = x;
            this.Y = y;
            this.UnitGraphic = token;
        }
        /*
        in C# can have multiple Constructors
        --In C# can have multiple () with same name as long as they have
        a different signature
        --A different signature as in different parameters inside the ()
        --The cpu will tell which one you want, based on the parameters
        --Unit() --> Oh, just the default constructor
        --Unit(1, 2, "I") --> Oh, you want the detailed constructor
        */

        /*
        Private means that these variables cannot be modified OUTSIDE
        of the Unit class. Main can call them, but cannot otherwise
        touch them.
        */
        /*
        But if I turn it into a structure, with specific permissions,
        I can make the read outside public, but keep the write to private
        */
        //C# Convention: Public uses capital letters, so X instead of x
        public int X //The way the rest of the program interacts with x
        {
            //This is public for setting and reading
            //A public interface, but not an actual value with its own memory
            get
            {
                //Will instead return value of underscore x (_x)
                return _x;
            }
            set
            {
                if (value < 0 || value >= Console.WindowWidth)
                {
                    //Create a specilized exception
                    //Will pass the message, and this location as the error
                    //Also nice because in debug mode, usually goes to error and highlights it
                    throw new Exception("Invalid X coordinate passed.");
                }
                UnDraw(); //we are moving, so undraw
                _x = value;
            }
        }
        //but the public X is interacing with a private _x
        //underscore _ is a hint to programmers: "Don't mess with directly"
        private int _x; //Where the value of x is actually stored in memory

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value < 0 || value >= Console.WindowHeight)
                {
                    throw new Exception("Invalid Y coordinate passed.");
                }
                UnDraw();
                _y = value;
            }
        }
        private int _y; //Where the actual value of y is stored in memory

        public string UnitGraphic { get; set; }

        /*
        ---Rendered Obsolete by establishing structs for x, y variables---
        //Set our x and y to the positions entered to the 
        public void SetPosition(int x, int y)
        {
            if (x >= 0 
                && x < Console.WindowWidth
                && y >= 0
                && y < Console.WindowHeight
                )
            {
                //X and Y are valid for the instance, so build as directed
                this.x = x;
                this.y = y;
            }
            //Instead of failing silently, consider two possibilities:
            //Opt 1: Fix the input (ex: x = 0)
            //Better option is to let it fail, but give an alert
            else
            {
                //Create a specilized exception
                //Will pass the message, and this location as the error
                throw new Exception("Invalid X/Y coordinate passed.");
            }
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        //Other functions if you want to keep the variables private...
        public void MoveLeft()
        {
            //Moves the cursor left
        }
        //Same things for MoveUp(), MoveDown(), MoveRight()...
        */

        //Draws the unit on the screen
        virtual public void Draw()
        {
            /*Instance Method. So when we refer to fields like x and y,
                we are only using the values that belong to a single instance,
                and that instance only.
            */
            Console.SetCursorPosition(this.X, this.Y);
            Console.Write(this.UnitGraphic);
        }

        public void UnDraw()
        {
            //We look at the x,y location for the token...
            Console.SetCursorPosition(this.X, this.Y);
            //And replace it with a blank space
            Console.Write(' ');
        }

        //Example ()
        public void DoSomethingToOtherUnit(Unit other)
        {
            if(this.X < other.X)
            {
                //do Something
            }
        }

        public bool IsCollidingWith(Unit other)
        {
            //'This' is the current Unit
            //'Other' is any other Unit

            if(this.X == other.X && this.Y == other.Y)
            {
                //We are in the same square, so we are colliding
                return true;
            }
            return false;
        }

        //public bool IsPlayer = false;

        //Update(): A common function in a game
        /*
        'virtual' --> Indicates the C# that more supervision or homework,
        checking an extra table of information to determine which Update()
        should be called
        --Parent gets 'virtual', child gets 'override'
        */
        virtual public void Update(int deltaTime)
        {
            /*
            This is an instance method that gets run every frame,
            where the Unit should resolve any game things that are
            going on.
            --The idea is that all Units Update themselves, then all
            units will be drawn.
            */

            /*
                Since this update runs for both Enemies and Players,
                it will need to be overriden by the child classes.
            */
            //throw new Exception("We are in Unit::Update()!");

        }
    }
}
