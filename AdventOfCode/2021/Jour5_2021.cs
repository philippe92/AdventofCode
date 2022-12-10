using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Jour5_2021 : IJour
    {
        List<Tuple<int, int, int, int>> lines;
        public void Init(string inputfile)
        {
            lines = new List<Tuple<int, int, int, int>>();
            var rx = new Regex(@"([0-9]+),([0-9]+) -> ([0-9]+),([0-9]+)", RegexOptions.Compiled);
            string[] input = File.ReadAllLines(inputfile);
            foreach (var line in input)
            {
                var r = rx.Match(line);
                if (!r.Success)
                {
                    Console.WriteLine("Erreur de donnée!!!");
                    continue;
                }
                lines.Add(new Tuple<int, int, int, int>(int.Parse(r.Groups[1].Value), int.Parse(r.Groups[2].Value), int.Parse(r.Groups[3].Value), int.Parse(r.Groups[4].Value)));
            }
        }

        public void ResolveFirstPart()
        {
            int max = 0;
            foreach(var item in lines)
            {
                var line_max = Math.Max(item.Item1, item.Item2);
                line_max = Math.Max(line_max, item.Item3);
                line_max = Math.Max(line_max, item.Item4);
                max = Math.Max(max, line_max);
            }

            var tableau = new int[max+1, max+1];
            foreach(var line in lines)
            {
                if (line.Item1 == line.Item3)
                {
                    var y1 = Math.Min(line.Item2, line.Item4);
                    var y2 = Math.Max(line.Item2, line.Item4);
                    for (int i = y1; i <= y2; i++ )
                    {
                        tableau[line.Item1, i]++;
                    }
                }
                else if (line.Item2 == line.Item4)
                {
                    var x1 = Math.Min(line.Item1, line.Item3);
                    var x2 = Math.Max(line.Item1, line.Item3);
                    for (int i = x1; i <= x2; i++)
                    {
                        tableau[i, line.Item2]++;
                    }
                }
            }

            int count = 0;
            for (int i = 0; i < max+1 ; i++)
            {
                for (int j = 0; j < max+1; j++)
                {
                    if (tableau[i,j] >= 2)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine($"result = {count}");
        }

        public void ResolveSecondPart()
        {
            int max = 0;
            foreach (var item in lines)
            {
                var line_max = Math.Max(item.Item1, item.Item2);
                line_max = Math.Max(line_max, item.Item3);
                line_max = Math.Max(line_max, item.Item4);
                max = Math.Max(max, line_max);
            }

            var tableau = new int[max + 1, max + 1];
            foreach (var line in lines)
            {
                if (line.Item1 == line.Item3)
                {
                    var y1 = Math.Min(line.Item2, line.Item4);
                    var y2 = Math.Max(line.Item2, line.Item4);
                    for (int i = y1; i <= y2; i++)
                    {
                        tableau[line.Item1, i]++;
                    }
                }
                else if (line.Item2 == line.Item4)
                {
                    var x1 = Math.Min(line.Item1, line.Item3);
                    var x2 = Math.Max(line.Item1, line.Item3);
                    for (int i = x1; i <= x2; i++)
                    {
                        tableau[i, line.Item2]++;
                    }
                }
                else if (line.Item1 < line.Item3 && line.Item2 < line.Item4)
                {
                    // gauche/haut vers bas/droite
                    int x = line.Item1;
                    int y = line.Item2;
                    do
                    {
                        tableau[x, y]++;
                        x++;
                        y++;
                    } 
                    while (x != line.Item3 && y != line.Item4);
                    tableau[x, y]++;

                }
                else if (line.Item1 > line.Item3 && line.Item2 > line.Item4)
                {
                    // bas/droite vers gauche/haut
                    int x = line.Item1;
                    int y = line.Item2;
                    do
                    {
                        tableau[x, y]++;
                        x--;
                        y--;
                    }
                    while (x != line.Item3 && y != line.Item4);
                    tableau[x, y]++;
                }
                else if (line.Item1 < line.Item3 && line.Item2 > line.Item4)
                {
                    // gauche/bas vers droit/haut
                    int x = line.Item1;
                    int y = line.Item2;
                    do
                    {
                        tableau[x, y]++;
                        x++;
                        y--;
                    }
                    while (x != line.Item3 && y != line.Item4);
                    tableau[x, y]++;
                }
                else if (line.Item1 > line.Item3 && line.Item2 < line.Item4)
                {
                    // droit/haut vers gauche/bas
                    int x = line.Item1;
                    int y = line.Item2;
                    do
                    {
                        tableau[x, y]++;
                        x--;
                        y++;
                    }
                    while (x != line.Item3 && y != line.Item4);
                    tableau[x, y]++;

                }
            }

            for (int i = 0; i < max + 1; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < max + 1; j++)
                {
                    if (tableau[j, i] > 0)
                    {
                        sb.Append(tableau[j, i]);
                    }
                    else
                    {
                        sb.Append(".");
                    }
                }
                Console.WriteLine(sb);
            }

            int count = 0;
            for (int i = 0; i < max + 1; i++)
            {
                for (int j = 0; j < max + 1; j++)
                {
                    if (tableau[i, j] >= 2)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine($"result = {count}");
        }
    }
}
