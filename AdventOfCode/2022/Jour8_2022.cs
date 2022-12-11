using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Jour8_2022 : IJour
    {
        int[,] map;
        bool[,] invisible;
        int size;
        int[,] score;

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            size = lines[0].Length;
            map = new int[size, size];
            score = new int[size, size];
            invisible = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = int.Parse(lines[i].Substring(j, 1));
                }
            }
        }

        private bool ResolvePart(int i, int j, int dx, int dy)
        {
            int height = map[i, j];
            while (true)
            {
                i += dx;
                j += dy;
                if (!(i >= 0 && j >= 0 && i < size && j < size))
                {
                    break;
                }
                if (map[i, j] >= height)
                {
                    return true;
                }
            }
            return false;
        }

        public void ResolveFirstPart()
        {
            for (int i = 1; i < size - 1; i++)
            {
                for (int j = 1; j < size - 1; j++)
                {
                    bool tmp = true;
                    tmp &= ResolvePart(i, j, 1, 0);
                    tmp &= ResolvePart(i, j, 0, 1);
                    tmp &= ResolvePart(i, j, -1, 0);
                    tmp &= ResolvePart(i, j, 0, -1);

                    invisible[i, j] = tmp;
                }
            }

            int result = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                { 
                    if (!invisible[i,j])
                    {
                        result++;
                    }
                }
            }

            Console.WriteLine($"Resultat = {result}");
        }

        private int CalculateScore(int i, int j, int dx, int dy)
        {
            int r = 1;
            int height = map[i, j];
            while (true)
            {
                i += dx;
                j += dy;
                if (!(i >= 0 && j >= 0 && i < size && j < size))
                {
                    r--;
                    break;
                }
                if (map[i, j] < height)
                {
                    r++;
                }
                else
                {
                    break;
                }
            }
            return r;
        }

        public void ResolveSecondPart()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int thescore = CalculateScore(i, j, 1, 0);
                    thescore *= CalculateScore(i, j, 0, 1);
                    thescore *= CalculateScore(i, j, -1, 0);
                    thescore *= CalculateScore(i, j, 0, -1);

                    score[i, j] = thescore;
                }
            }

            int max = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    max = Math.Max(max, score[i, j]);
                }
            }
            Console.WriteLine($"Resultat = {max}");
        }
    }
}
