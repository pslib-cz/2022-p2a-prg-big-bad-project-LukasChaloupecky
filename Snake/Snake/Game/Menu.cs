using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
namespace SnakeGame.Games
{
    public static class Menu
    {
        public static void ShowMenu(ref Game game, ref bool IsRunning, ref int TimeOut)
        {
            int OptionCount = 2; // THIS VALUE IS IMPORTANT - NEED TO CHANGE IF MORE OPTIONS ARE ADDED TO MENU (etc. 3 options => OptionCount = 2)
            Console.CursorVisible = false;
            Keys Option = Keys.STAY;
            int Selected = 0;
            WriteMenu(Selected);

            while(true)
            {
                if(Console.KeyAvailable)
                {
                    Option = GetKeyNumber(Console.ReadKey(true).Key);
                    
                    // Do code based on Key
                    if(Option == Keys.UP)
                    {
                        if(Selected != 0)
                        {
                            Selected += -1;
                            WriteMenu(Selected);
                        }
                    }
                    else if (Option == Keys.DOWN)
                    {
                        if(Selected != OptionCount) 
                        {
                            Selected += 1;
                            WriteMenu(Selected);
                        }
                    }
                    else if (Option == Keys.SELECT)
                    {
                        if (Selected == 0) 
                        {
                            // Start New Game
                            StartNewGame(ref game, ref IsRunning, ref TimeOut);
                            break;

                        }
                        else if (Selected == 1) 
                        {
                            // Use Stream Reader to write Creadits
                            WriteCredits();
                            
                            //Wait for Player to quit Credits
                            while(true)
                            {
                                if (Console.KeyAvailable)
                                {
                                    Option = GetKeyNumber(Console.ReadKey(true).Key);
                                    if (Option == Keys.SELECT)
                                    {
                                        Selected = 0;
                                        WriteMenu(Selected);
                                        break;
                                    }
                                }
                            }
                        }
                        else if (Selected == 2)
                        {
                            // Quit Game
                            Console.Clear();
                            game.Snake.isAlive = false;
                            IsRunning = false;
                            break;
                        }
                    }
                }
            }
        }



        public static void StartNewGame(ref Game game, ref bool IsRunning, ref int TimeOut)
        {
            Console.Clear();
            Console.CursorVisible = true;
            int boxX = 0;
            int boxY = 0;
            int TimeOut_ = 0;

            bool CorrectValue = false;
            while (!CorrectValue)
            {

                Console.WriteLine("Set box lenght - depends on font size - [ Min = 20 , Max =  (100, the max can depend on your device) ]- recommended 70");
                CorrectValue = Int32.TryParse(Console.ReadLine(), out boxX);
                if (CorrectValue &&( boxX < 20 || boxX > Console.BufferWidth-1))
                {
                    CorrectValue = false;
                }
                

            }
            Console.Clear();

            CorrectValue = false;
            while (!CorrectValue)
            {
                Console.WriteLine("Set box height - depends on font size - [ Min = 20 , Max = (100, the max can depend on your device) ] - recommended 20,");
                CorrectValue = Int32.TryParse(Console.ReadLine(), out boxY);
                if (CorrectValue &&( boxY < 20 || boxY > Console.BufferHeight-7))
                {
                    CorrectValue = false;
                }
            }
            Console.Clear();

            CorrectValue = false;
            while (!CorrectValue)
            {
                Console.WriteLine("Set game speed - FPS/TimeOut in ms, recommended is 100");
                CorrectValue = Int32.TryParse(Console.ReadLine(), out TimeOut_);
                if (CorrectValue &&( TimeOut_ < 1 || TimeOut_ > 2147483600))
                {
                    CorrectValue = false;
                }
            }
            TimeOut = TimeOut_;
            game = new Game(boxX, boxY, new Stopwatch());
            game.Snake.isAlive = true;
            Console.Clear();
        }



        private static void WriteCredits()
        {
            Console.Clear();
            string? line = "";
            using (StreamReader sr = new StreamReader("Data/Credits.txt"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    Thread.Sleep(500);
                }
                Console.ForegroundColor = ConsoleColor.White;

            }
            Thread.Sleep(3000);
            Console.Clear();


            Console.WriteLine();
            using (StreamReader sr = new StreamReader("Data/VirusPrank.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    Thread.Sleep(1);
                }
            }

            Thread.Sleep(1000);
            Console.Clear();
            using (StreamReader sr = new StreamReader("Data/BB.txt"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Thread.Sleep(1000);

        }
        private static void WriteMenu(int Selected)
        {
            Console.Clear();
            char[] selection = new char[3];
            for(int i = 0; i < selection.Length; i++)
            {
                selection[i] = ' ';
            }
            selection[Selected] = '#';


            // Write StreamReader Snake icon
            using (StreamReader sr = new StreamReader("Data/Logo.txt"))
            {
                string? line = " ";
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            // Menu options
            Console.WriteLine($"   {selection[0]} : Start Game");
            Console.WriteLine($"   {selection[1]} : Credits");
            Console.WriteLine($"   {selection[2]} : Quit");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            // Here use StreamReader for MaxScore and Last Score
            Console.WriteLine($"  Last Score : {Save.GetLastScore()}");
            Console.WriteLine($"  Max Score  : {Save.GetMaxScore()}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static Keys GetKeyNumber(ConsoleKey Key)
        {
            if (Key == ConsoleKey.W || Key == ConsoleKey.UpArrow) return Keys.UP;
            else if (Key == ConsoleKey.S || Key == ConsoleKey.DownArrow) return Keys.DOWN;
            else if (Key == ConsoleKey.Enter) return Keys.SELECT;
            return Keys.STAY;
        }
        private enum Keys
        {
            STAY,
            UP,
            DOWN,
            SELECT   
        }
    }
}
