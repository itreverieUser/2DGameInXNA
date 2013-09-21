using System;

namespace RubbishCollector
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameRubbishCollector game = new GameRubbishCollector())
            {
                game.Run();
            }
        }
    }
#endif
}

