using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Jour9_2020 : IJour
    {
        List<long> serie = new List<long>();

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            foreach (var line in lines)
            {
                serie.Add(long.Parse(line));
            }
        }

        private bool VerifyAt(int i, int preamble_length)
        {
            for (int j = 0; j < preamble_length; j++)
            {
                for (int k = 0; k < preamble_length; k++)
                {
                    var pos1 = i - (j + 1);
                    var pos2 = i - (k + 1);
                    if (serie[pos1] != serie[pos2] && serie[i] == (serie[pos1] + serie[pos2]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ResolveFirstPart()
        {
            for (int i=25; i<serie.Count; i++)
            {
                if (!VerifyAt(i, 25))
                {
                    Console.WriteLine($"Resultat = {serie[i]}, {i}");
                }
            }
        }

        public void ResolveSecondPart()
        {
            var found = 22406676;
            for (int i=2; i<serie.Count; i++)
            {
                for (int j=0; j<serie.Count-i; j++)
                {
                    long sum = 0;
                    long min = 0;
                    long max = long.MaxValue;
                    for (int k=0; k<i;k++)
                    {
                        long value = serie[j + k];
                        sum += value;
                        if (value> min)
                        {
                            min = value;
                        }
                        if (value<max)
                        {
                            max = value;
                        }
                    }
                    if (sum == found)
                    {
                        Console.WriteLine($"Resultat = {min+max}, {i}");
                    }
                }
            }
        }
    }
}
