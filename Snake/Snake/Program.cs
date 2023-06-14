using SnakeGame.Entity;
using SnakeGame.Games;
using System.Diagnostics;
using System.Xml.Linq;


namespace SnakeGame
{

    class Program
    {

        static void Main(string[] args)
        {


            int TimeOut = 100; // default
            ConsoleKey Key;
            ConsoleKey MenuKey = ConsoleKey.Escape;
            
            
            Game game = new Game();
            Save.Check();

            bool IsRunning = true;
            
            

            Console.BufferHeight = Console.WindowHeight; // disable the thing on right side for scrolling down


            Console.WriteLine("Set text size => 28 by clicking on the text console logo in top left corner");
            Console.WriteLine("Set to fullscreen => F11");
            
            Console.ReadKey(true);


            new Thread(new ThreadStart(() =>
            {
                while (IsRunning)
                {
                    // if we started, died or quit
                    

                    // Pokud hrajeme
                    while (game.Snake.isAlive)
                    {
                        // Kód pro hru
                        
                        game.UpdateGame(ref IsRunning);
                        // Sleep for time
                        Thread.Sleep(TimeOut);
                    }
                    Menu.ShowMenu(ref game, ref IsRunning, ref TimeOut);
                }

            })).Start(); 
            
            
            while (IsRunning)
            {

                if (game.Snake.isAlive) 
                { 
                    game.sw.Start(); 
                    Console.CursorVisible = false;
                    while (game.Snake.isAlive)
                    {
                        Key = Console.ReadKey(true).Key;
                        if (Key == MenuKey)
                        {
                            game.sw.Reset();
                            game.Snake.isAlive = false;
                            break;
                        }
                        game.Snake.SetDirection(Key);
                    }
                }
                


            }
         

        }
    }
}