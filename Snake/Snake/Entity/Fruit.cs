using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SnakeGame.Games;


namespace SnakeGame.Entity
{
    public class Fruit
    {
        //----> Constants
        public readonly string[] FruitTypes = { "♥", "☻", "♦", "♣", "♂", "♫" };
        public readonly ConsoleColor[] FruitTypeColor = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Gray, ConsoleColor.Magenta, ConsoleColor.Green, ConsoleColor.Blue };
        //----> Varibles
        public Coords Location; // Coordinates
        public int FruitType; // Number for Array of fruits COLOR and SYMBOL 


        //----> Declaration

        public Fruit(List<Coords> SnakeBody, int sizeX, int sizeY) 
        {
            Location = new Coords();
            GenerateFruit(SnakeBody, sizeX, sizeY);
        }

        //----> Generation of fruit
        public void GenerateFruit(List<Coords> SnakeBody, int sizeX, int sizeY)
        {
            Coords newCoords = new Coords();
            Random rnd = new Random();
            bool goodCoords = false;

            while (!goodCoords) // if NOT goodCoords (="coords that are not inside snakes cell")
            {
                newCoords.Assign(rnd.Next(1, sizeX - 1), rnd.Next(1, sizeY - 1)); // Asign coords for testing
                goodCoords = true;
                foreach (Coords snakeCoords in SnakeBody) // Test if coords are not in cell of snakes body
                {
                    if (Coords.Equel(newCoords, snakeCoords))
                    {
                        goodCoords = false;
                        break;
                    }
                }
                Location.Assign(newCoords.X, newCoords.Y);
                FruitType = rnd.Next(0, FruitTypes.Length-1); // Set fruit value
            }
        }

        //----> Fruit enum - For organization in what fruit has what color and symbol
        public enum FruitsEnum
        {
            Heart = 0,
            Smile = 1,
            Rhomboid = 2,
            Card = 3,
            Male = 4,
            Note = 5
        }
    }

}
