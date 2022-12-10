using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Jour3_2020 : IJour
    {
        List<string> map;

        private static Int64 CalculateTreesCount(List<string> map, int deltarow, int deltacol)
        {
            int width = map[0].Length;
            int col = 0;
            int row = 0;
            var result = 0;

            while (row <= map.Count - 2)
            {
                col += deltacol;
                row += deltarow;
                if (col >= width)
                {
                    col -= width;
                }
                if (map[row][col] == '#')
                {
                    result++;
                }

            }
            Console.WriteLine($"Pour {deltarow}, {deltacol} Resultat={result}");
            return result;
        }

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            map = new List<string>();
            foreach (var line in lines)
            {
                map.Add(line);
            }
        }

        public void ResolveFirstPart()
        {
            Int64 result = CalculateTreesCount(map, 1, 3);
            Console.WriteLine($"Resultat = {result}");
        }

        public void ResolveSecondPart()
        {
            Int64 result = CalculateTreesCount(map, 1, 1) * CalculateTreesCount(map, 1, 3) * CalculateTreesCount(map, 1, 5) * CalculateTreesCount(map, 1, 7) * CalculateTreesCount(map, 2, 1);
            Console.WriteLine($"Resultat = {result}");
        }

        
    }
}
