using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Jour17_2021 : IJour
    {
        int min_x = 156;
        int max_x = 202;
        int min_y = -110;
        int max_y = -69;

        public void Init(string inputfile)
        {
        }

        private bool IsFinished(int x, int y)
        {
            return x >= min_x && x <= max_x && y >= min_y && y <= max_y;
        }

        private bool Simulate(int vel_x, int vel_y, out int result)
        {
            int x = 0;
            int y = 0;
            result = int.MinValue;
            int i = 0;
            while (!IsFinished(x, y) && i <= 1000)
            {
                x += vel_x;
                y += vel_y;

                if (vel_x < 0)
                {
                    vel_x++;
                }
                if (vel_x > 0)
                {
                    vel_x--;
                }
                vel_y--;
                result = Math.Max(result, y);
                i++;
            }
            return i < 1000;
        }

        public void ResolveFirstPart()
        {
            int result = int.MinValue;
            for (int i = -1000; i < 1000; i++)
            {
                for (int j = -1000; j < 1000; j++)
                {
                    if (Simulate(i, j, out var tmp))
                    {
                        result = Math.Max(result, tmp);
                    }
                }
            }

            Console.WriteLine($"Resultat : {result}");
        }

        public void ResolveSecondPart()
        {
            int result = 0;
            for (int i = -1000; i < 1000; i++)
            {
                for (int j = -1000; j < 1000; j++)
                {
                    if (Simulate(i, j, out _))
                    {
                        result++;
                    }
                }
            }

            Console.WriteLine($"Resultat : {result}");

        }
    }
}
