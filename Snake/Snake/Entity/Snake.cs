using SnakeGame.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Entity
{
    public class Snake
    {
        public List<Coords> Body;
        public Coords Head;
        public DirectionEnum Direction = DirectionEnum.STAY;
        public DirectionEnum PrevDire = DirectionEnum.STAY;
        public Coords removedPart;

        public readonly string HeadChar = "▲▼◄►▲";
        public bool isAlive = false;

        public Snake() { }
        public Snake(int sizeX, int sizeY)
        {
            Head = new Coords(sizeX / 2, sizeY / 2);
            Body = new List<Coords>();
        }
        public void SetDirection(ConsoleKey Key)
        {
           
            if ((Key == ConsoleKey.W || Key == ConsoleKey.UpArrow) && ((Body.Count > 0 && PrevDire != DirectionEnum.DOWN) || Body.Count == 0))
            {
                
                Direction = DirectionEnum.UP;
                
                
            }
            else if ((Key == ConsoleKey.S || Key == ConsoleKey.DownArrow) && ((Body.Count > 0 && PrevDire != DirectionEnum.UP) || Body.Count == 0))
            {
                Direction = DirectionEnum.DOWN;
            }
            else if ((Key == ConsoleKey.A || Key == ConsoleKey.LeftArrow) && ((Body.Count > 0 && PrevDire != DirectionEnum.RIGHT) || Body.Count == 0))
            {
                Direction = DirectionEnum.LEFT;
            }
            else if ((Key == ConsoleKey.D || Key == ConsoleKey.RightArrow) && ((Body.Count > 0 && PrevDire != DirectionEnum.LEFT) || Body.Count == 0))
            {
                Direction = DirectionEnum.RIGHT; ;
            }
        }
        public void UpdateBody()
        {
            if (Direction != DirectionEnum.STAY)
            {
                Body.Add(new Coords(Head.X, Head.Y));
                PrevDire = Direction;
                if (Direction == DirectionEnum.UP)
                {
                    Head.Y -= 1;
                }
                else if (Direction == DirectionEnum.DOWN)
                {
                    Head.Y += 1;
                }
                else if (Direction == DirectionEnum.LEFT)
                {
                    Head.X -= 1;
                }
                else if (Direction == DirectionEnum.RIGHT)
                {
                    Head.X += 1;
                }
                
            }
        }

        public enum DirectionEnum
        {
            UP = 0,
            DOWN = 1,
            LEFT = 2,
            RIGHT = 3,
            STAY = 4,
        }
    }
}
