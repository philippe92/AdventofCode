using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Jour5_2020 : IJour
    {
        List<int> seatsID;

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            seatsID = new List<int>();
            foreach (var line in lines)
            {
                int row = 0;
                int column = 0;
                var chars = line.ToCharArray();
                for (int i = 0; i < 8; i++)
                {
                    if (chars[i] == 'B')
                    {
                        row += (int)Math.Pow(2, (6 - i));
                    }
                }

                for (int i = 7; i < 10; i++)
                {
                    if (chars[i] == 'R')
                    {
                        column += (int)Math.Pow(2, (2 - (i-7)));
                    }
                }

                seatsID.Add(row*8+column);
            }
        }

        public void ResolveFirstPart()
        {
            Console.WriteLine($"Resultat = {seatsID.Max()}");
        }

        public void ResolveSecondPart()
        {
            seatsID.Sort();
            for (int i=1;i<seatsID.Count;i++)
            {
                if (seatsID[i] != seatsID[i-1]+1)
                {
                    Console.WriteLine($"Resultat = {seatsID[i]-1}");
                    break;
                }
            }
        }
    }
}
