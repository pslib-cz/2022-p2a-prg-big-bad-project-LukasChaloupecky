using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Games
{
    public class Coords
    {
        public int X;
        public int Y;
        public Coords()
        {

        }
        public Coords(int X, int Y) // Přiřadí polohu jednoho článku 
        {
            this.X = X;
            this.Y = Y;
        }

        public void Assign(int X, int Y) // Přiřadí polohu jednoho článku 
        {
            this.X = X;
            this.Y = Y;
        }
        public static bool Equel(Coords coords1, Coords coords2)
        {
            if(coords1.X == coords2.X && coords1.Y == coords2.Y)
            {
                return true;
            }
            return false;
        }
    }
}
