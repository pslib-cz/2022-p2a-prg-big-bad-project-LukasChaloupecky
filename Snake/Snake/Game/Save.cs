using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Games
{
    public static class Save
    {
        public static readonly string adrLast = "Data/LastScore.txt";
        public static readonly string adrMax = "Data/MaxScore.txt";

        public static void Check()
        {
            bool hasMax = true;
            bool hasLast = true;
            
            using (StreamReader sr =  new StreamReader(adrLast))
            {
                if(sr.ReadLine() == null) hasLast = false;
                
            }
            
            using (StreamReader sr = new StreamReader(adrMax))
            {
                if (sr.ReadLine() == null) hasMax = false;
            }


            if (!hasLast)
            {
                using (StreamWriter sw = new StreamWriter(adrLast))
                {
                    sw.WriteLine(0);
                }
            }
            if (!hasMax)
            {
                using (StreamWriter sw = new StreamWriter(adrMax))
                {
                    sw.WriteLine(0);
                }
            }
        }
        public static int GetLastScore()
        {
            int x = new int();
            using (StreamReader sr = new StreamReader(adrLast))
            {
                if(!Int32.TryParse(sr.ReadLine(), out x) )
                {
                    throw new ArgumentException("Invalid value : Snake/Data/LastScore.txt");
                }
                return x;
            }
        }
        public static void WriteLastScore(int score)
        {
            using (StreamWriter sw = new StreamWriter(adrLast))
            {
                sw.WriteLine(score);
            }
        }

        public static int GetMaxScore()
        {
            int x = new int();
            using (StreamReader sr = new StreamReader(adrMax))
            {
                if (!Int32.TryParse(sr.ReadLine(), out x))
                {
                    throw new ArgumentException("Invalid value : Snake/Data/MaxScore.txt");
                }
                return x;
            }
        }
        public static void WriteMaxScore(int score)
        {
            int CurrentMaxScore = GetMaxScore();
            if (score > CurrentMaxScore)
            {
                using (StreamWriter sw = new StreamWriter(adrMax))
                { 
                    sw.WriteLine(score);
                }
            }
        }
    }
}
