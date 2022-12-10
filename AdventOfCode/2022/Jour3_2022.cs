using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Jour3_2022 : IJour
    {
        string[] data;

        public void Init(string inputfile)
        {
            data = File.ReadAllLines(inputfile);
        }

        public void ResolveFirstPart()
        {
            int resultat = 0;
            foreach (var l in data)
            {
                var sublength = l.Length / 2;
                var half1 = l.Substring(0, sublength);
                var half2 = l.Substring(sublength, sublength);

                List<char> commons = new List<char>();
                foreach (var c in half1)
                {
                    if (half2.Contains(c))
                    {
                        if ('a' <= c && c <= 'z')
                        {
                            resultat += c - 'a' + 1;
                        }
                        if ('A' <= c && c <= 'Z')
                        {
                            resultat += c - 'A' + 27 ;
                        }
                        break;
                    }
                }
            }
            Console.WriteLine($"Resultat = {resultat}");
        }

        private List<char> GetCommons(string a, string b)
        {
            List<char> result = new List<char>();
            foreach (var c in a)
            {
                if (b.Contains(c))
                {
                    result.Add(c);
                }
            }
            return result;
        }

        public void ResolveSecondPart()
        {
            int resultat = 0;
            int i = 0;
            for (i = 0; i < data.Length / 3; i++)
            {
                List<char> common1 = GetCommons(data[i * 3], data[i * 3 + 1]);
                List<char> common2 = GetCommons(data[i * 3], data[i * 3 + 2]);
                List<char> common3 = GetCommons(data[i * 3 + 1], data[i * 3 + 2]);

                char c = common1.Intersect(common2).Intersect(common3).First();

                if ('a' <= c && c <= 'z')
                {
                    resultat += c - 'a' + 1;
                }
                if ('A' <= c && c <= 'Z')
                {
                    resultat += c - 'A' + 27;
                }
            }
            Console.WriteLine($"Resultat = {resultat}");
        }
    }
}
