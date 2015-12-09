using System;
using Tank_Game;
using TankClient;

namespace SunWarriorsGame
{
#if WINDOWS || XBOX
    static class Program2
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //static void Main(string[] args)
        //{
        //    using (Game1 game = new Game1())
        //    {
        //        game.Run();
        //    }
        //}

        internal static void Main(GridEntity[,] grid)
        {
            using (Game1 game = new Game1(grid))
            {
                game.Run();
            }
        }
    }
#endif
}

