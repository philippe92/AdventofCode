using System;
using System.IO;

namespace AdventOfCode
{
    public class Jour11_2021 : IJour
    {
        int[,] map;
        int x;
        int y;

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            y = lines[0].Length;
            x = lines.Length;

            map = new int[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    map[i, j] = lines[i][j] - '0';
                }
            }
        }

        private long flashes = 0;

        public void Increment(int i, int j)
        {
            if (i < 0 || i >= x || j < 0 || j >= y)
            {
                return;
            }
            if (map[i, j] == 10)
            {
                map[i, j]++;
                DoFlash(i, j);
            }
            map[i, j]++;
            if (map[i, j] == 10)
            {
                map[i, j]++;
                DoFlash(i, j);
            }
        }

        public void DoFlash(int i, int j)
        {
            flashes++;
            Increment(i - 1, j - 1);
            Increment(i - 1, j);
            Increment(i - 1, j + 1);
            Increment(i, j - 1);
            Increment(i, j + 1);
            Increment(i + 1, j - 1);
            Increment(i + 1, j );
            Increment(i + 1, j + 1);
        }

        private void DumpMap()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write(map[i, j] >= 10 ? '0' : map[i, j]);
                }
                Console.WriteLine();
            }
        }

        private void DoStep()
        {
            // 1 : incremente l'energie
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    map[i, j]++;
                }
            }

            // 2 : effectue les flashes, et propage les flashes si besoin
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (map[i, j] == 10)
                    {
                        map[i, j]++;
                        DoFlash(i, j);
                    }
                }
            }

            // 3 reset l'energie des octopus qui ont flashé
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (map[i, j] >= 10)
                    {
                        map[i, j] = 0;
                    }
                }
            }
        }

        public void ResolveFirstPart()
        {            
            for (int a=0;a<100;a++)
            {
                DoStep();
            }
            Console.WriteLine($"Resultat : {flashes}");
        }

        public void ResolveSecondPart()
        {
            bool done = false;
            int a = 0;
            while(!done)
            {
                DoStep();
                done = true;
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        if (map[i, j] != 0)
                        {
                            done = false;
                            break;
                        }
                    }
                }
                a++;
            }
            // on a deja fait 100 steps dans la partie 1...
            Console.WriteLine($"Resultat : {a+100}");
        }
    }
}
