﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Entity;

namespace SnakeGame.Games
{
    public class Game
    {
        public int sizeX;
        public int sizeY;
        public Snake Snake;
        public Fruit Fruit;
        public int Score = 0;
        public readonly char BorderChar = '█';
        public bool writeBorder = true;
        public bool writeFruit = true;
        public bool removedPart = false;

        public bool EndedByDeath = false;

        public Stopwatch sw;
        public Game() 
        {
            Snake = new Snake();
        }
        public Game(int sizeX, int sizeY, Stopwatch sw)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            Snake = new Snake(sizeX, sizeY);
            Fruit = new Fruit(Snake.Body, sizeX, sizeY);
            Score = 0;
            this.sw = sw;
        }

        public static void SetCursorEasy(Coords SetCoord)
        {
            Console.SetCursorPosition(SetCoord.X, SetCoord.Y);
        }
        public void UpdateView()
        {

            // nakresli inside box - nepoužívat - vypadá to divně, bílá se nehodí
            /*
            for(int i = 0; i < sizeY; i++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, i);
                Console.WriteLine(new string(' ', sizeX+1));
            }
            Console.BackgroundColor = ConsoleColor.Black;
            */
            if (writeBorder)
            {
                // nakresli border
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(new string(BorderChar, sizeX + 1));
                Console.SetCursorPosition(0, sizeY);
                Console.WriteLine(new string(BorderChar, sizeX + 1));
                for (int i = 0; i < sizeY; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write(BorderChar);
                    Console.SetCursorPosition(sizeX, i);
                    Console.Write(BorderChar);
                }
                writeBorder = false;
            }


            // nakresli fruit
            if (writeFruit)
            {
                SetCursorEasy(Fruit.Location);
                Console.ForegroundColor = Fruit.FruitTypeColor[Fruit.FruitType];
                Console.Write(Fruit.FruitTypes[Fruit.FruitType]);
                writeFruit = false;
            }
            // naklesli hada
            if (removedPart)
            {
                SetCursorEasy(Snake.removedPart);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');
                removedPart = false;
            }
            for (int i = 0; i < Snake.Body.Count; i++)
            {
                SetCursorEasy(Snake.Body[i]);
                if (Snake.Body.Count % 2 == 0 && i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (Snake.Body.Count % 2 != 0 && i % 2 != 0) { Console.ForegroundColor = ConsoleColor.Green; }
                else { Console.ForegroundColor = ConsoleColor.DarkGreen; }
                Console.Write('█');
            }
            SetCursorEasy(Snake.Head);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Snake.HeadChar[(int)Snake.Direction]); ;

            // nakresli skore

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, Console.BufferHeight-4);
            Console.WriteLine($"Score    :  {Score}");
            Console.WriteLine($"MaxScore :  {Save.GetMaxScore()}");
            string elapsed = string.Format("{0:00}:{1:00}:{2:00}", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);

            Console.WriteLine($"Time     :  {elapsed}"); 

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void UpdateGame(ref bool IsRunning)
        {
            Snake.UpdateBody();



            foreach (Coords cell in Snake.Body)   // Controls if head hit tail
            {
                if (Coords.Equel(cell, Snake.Head))
                {
                    Snake.isAlive = false;
                }

            }



            if (Coords.Equel(Snake.Head, Fruit.Location)) // Checks if ate fruit
            {
                writeFruit = true;
                Fruit.GenerateFruit(Snake.Body, sizeX, sizeY);
                Score += 10;
            }
            else if (Snake.Head.X == 0 || Snake.Head.X == sizeX || Snake.Head.Y == 0 || Snake.Head.Y == sizeY) // Checks if hit border
            {
                Snake.isAlive = false;
            }
            else if (Snake.Direction != Snake.DirectionEnum.STAY) // if only moved -> remove last cell
            {
                Snake.removedPart = new Coords(Snake.Body[0].X, Snake.Body[0].Y);
                Snake.Body.RemoveAt(0);
                removedPart = true;
            }

            if (Snake.isAlive)
            {
                UpdateView();
            }
            else
            {
                Snake.isAlive = false;
                EndedByDeath = true;
                Save.WriteMaxScore(Score);
                Save.WriteLastScore(Score);
                
            }



        }

        
    }
}
