using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Jour4_2022 : IJour
    {
        List<(int, int, int, int)> data;

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            data = new List<(int, int, int, int)>();
            var rx = new Regex(@"([0-9]+)-([0-9]+),([0-9]+)-([0-9]+)", RegexOptions.Compiled);
            foreach (var line in lines)
            {
                var r = rx.Match(line);
                if (!r.Success)
                {
                    Console.WriteLine("Erreur de donnée!!!");
                    continue;
                }
                data.Add((int.Parse(r.Groups[1].Value), int.Parse(r.Groups[2].Value), int.Parse(r.Groups[3].Value), int.Parse(r.Groups[4].Value)));
            }
        }

        public void ResolveFirstPart()
        {
            int resultat = 0;
            foreach (var e in data)
            {
                if (e.Item1 <= e.Item3 && e.Item2 >= e.Item4
                    || e.Item3 <= e.Item1 && e.Item4 >= e.Item2)
                {
                    resultat++;
                }
            }
            Console.WriteLine($"Resultat = {resultat}");
        }

        public void ResolveSecondPart()
        {
            int resultat = 0;
            foreach (var e in data)
            {
                if (e.Item2 >= e.Item3 && e.Item1 <= e.Item4)
                {
                    resultat++;
                }
            }
            Console.WriteLine($"Resultat = {resultat}");
        }
    }
}
