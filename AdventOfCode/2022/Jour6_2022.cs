using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Jour6_2022 : IJour
    {
        string data;
        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            data = lines[0];
        }

        public void ResolveFirstPart()
        {
            int i;
            for (i=3; i<data.Length; i++)
            {
                char[] a = new char[4];
                a[0] = data[i - 3];
                a[1] = data[i - 2];
                a[2] = data[i - 1];
                a[3] = data[i];
                if (a.Count(c => c == a[0]) == 1 
                    && a.Count(c => c == a[1]) == 1
                    && a.Count(c => c == a[2]) == 1
                    && a.Count(c => c == a[3]) == 1)
                {
                    break;
                }
            }

            Console.WriteLine($"Resultat = {i+1}");
        }

        public void ResolveSecondPart()
        {
            int i;
            for (i = 13; i < data.Length; i++)
            {
                char[] a = new char[14];
                for (int j = 0; j < 14; j++)
                {
                    a[j] = data[i - (13-j)];
                }

                bool good = true;
                
                for (int j = 0; j < 14; j++)
                {
                    if (a.Count(c => c == a[j])>1)
                    {
                        good = false;
                        break;
                    }
                }
                if (good)
                {
                    break;
                }
            }

            Console.WriteLine($"Resultat = {i + 1}");
        }
    }
}
