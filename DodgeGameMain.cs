using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeGame
{
    class DodgeGameMain
    {
        //You want your Main() to do as little as possible
        public static void Main(string[] args)
        {
            //Hide the blinking cursor. We won't need it
            Console.CursorVisible = false;

            //Create a new game
            Game game = new Game();
            //Run the game
            game.Run();

            //When we get here, the game is over

            Console.SetCursorPosition(
                0,
                Console.WindowHeight-1
            );

            Console.ReadLine();
            return;
        }
    }
}
