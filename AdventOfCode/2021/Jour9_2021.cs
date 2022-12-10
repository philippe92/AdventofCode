using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Jour9_2021 : IJour
    {
        int[,] map;
        int x;
        int y;

        List<Tuple<int, int>> lowpoints = new List<Tuple<int, int>>();

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

        private int GetValueCropped(int i, int j)
        {
            if (i < 0 || i >= x || j < 0 || j >= y)
            {
                return -1;
            }
            return map[i, j];
        }

        private bool IsLower(int val, int current)
        {
            return (val == -1 || current < val);
        }

        public void ResolveFirstPart()
        {
            int result = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    int current = map[i, j];
                    bool is_lowpoint = true;
                    is_lowpoint &= IsLower(GetValueCropped(i - 1, j), current);
                    is_lowpoint &= IsLower(GetValueCropped(i + 1, j), current);
                    is_lowpoint &= IsLower(GetValueCropped(i, j - 1), current);
                    is_lowpoint &= IsLower(GetValueCropped(i, j + 1), current);

                    if (is_lowpoint)
                    {
                        result += current + 1;
                        lowpoints.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            Console.WriteLine($"resultat = {result}");
        }

        private bool Test(bool[,] bassin, int i, int j)
        {
            if (i < 0 || i >= x || j < 0 || j >= y)
            {
                return false;
            }
            return bassin[i, j];
        }

        private bool Near(bool[,] bassin, int i, int j)
        {
            if (Test(bassin, i - 1, j))
            {
                return true;
            }
            if (Test(bassin, i + 1, j))
            {
                return true;
            }
            if (Test(bassin, i, j - 1))
            {
                return true;
            }
            if (Test(bassin, i, j + 1))
            {
                return true;
            }
            return false;
        }

        public void ResolveSecondPart()
        {
            List<int> bassins_size = new List<int>();
            foreach(var lowpoint in lowpoints)
            {
                var bassin = new bool[x, y];
                bassin[lowpoint.Item1, lowpoint.Item2] = true;
                int size = 1;
                int old_size = 0;
                while (true)
                {
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            if (map[i,j] == 9)
                            {
                                continue;
                            }
                            if (bassin[i,j])
                            {
                                continue;
                            }
                            if (Near(bassin,i,j))
                            {
                                bassin[i, j] = true;
                                size++;
                            }
                        }
                    }
                    if (size == old_size)
                    {
                        break;
                    }
                    old_size = size;
                }
                bassins_size.Add(size);
            }
            bassins_size.Sort((a, b) => b.CompareTo(a));
            var result = bassins_size.Take(3).Aggregate((a, b) => a * b);

            Console.WriteLine($"Result : {result}");
        }
        
    }
}
