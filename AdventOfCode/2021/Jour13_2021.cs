using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Jour13_2021 : IJour
    {
        bool[,] page;
        int x;
        int y;

        List<Tuple<bool, int>> fold_instr = new List<Tuple<bool, int>>();

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            x = 0;
            y = 0;
            int i= 0;
            while (lines[i].Length > 0)
            {
                var values = lines[i].Split(',');
                var a = int.Parse(values[0]);
                var b = int.Parse(values[1]);
                x = Math.Max(a, x);
                y = Math.Max(b, y);
                i++;
            }

            page = new bool[x+1, y+1];

            i = 0;
            while (lines[i].Length > 0)
            {
                var values = lines[i].Split(',');
                var a = int.Parse(values[0]);
                var b = int.Parse(values[1]);
                page[a, b] = true;
                i++;
            }
            i++;
            while(i<lines.Length)
            {
                var dir = lines[i][11] == 'x';
                var pos = int.Parse(lines[i].Substring(13, lines[i].Length - 13));
                fold_instr.Add(new Tuple<bool, int>(dir, pos));
                i++;
            } 
        }

        public void ResolveFirstPart()
        {
            var page2 = new bool[x + 1, y + 1];
            Array.Copy(page, page2, x+1*y+1);
            var x2 = x;
            var y2 = y;
            var f = fold_instr[0].Item2;
            if (fold_instr[0].Item1)
            {
                // pli vertical
                for (int i = 0; i < y2 + 1; i++)
                {
                    for (int j = 0; j < f; j++)
                    {
                        page2[j, i] |= page2[x2 - j, i];
                    }
                }
                x2 = f;
            }
            else
            {
                // pli horizontal
                for (int i = 0; i < x2 + 1; i++)
                {
                    for (int j = 0; j < f; j++)
                    {
                        page2[i, j] |= page2[i, y2 - j];
                    }
                }
                y2 = f;
            }

            int result = 0;
            for (int i=0;i<y2+1;i++)
            {
                for (int j=0;j<x2+1;j++)
                {
                    if (page2[j, i])
                    {
                        result++;
                    }
                }
            }
            Console.WriteLine($"Resultat : {result}");
        }

        public void ResolveSecondPart()
        {
            foreach (var fold in fold_instr)
            {
                var f = fold.Item2;
                if (fold.Item1)
                {
                    // pli vertical
                    for (int i = 0; i < y + 1; i++)
                    {
                        int r = 1;
                        while (r + f < x + 1 && f - r >= 0)
                        {
                            page[ f - r, i] |= page[f+r, i];
                            r++;
                        }
                    }
                    x = f-1;
                }
                else
                {
                    // pli horizontal
                    for (int i = 0; i < x + 1; i++)
                    {
                        int r = 1;
                        while (r + f < y + 1 && f -r >= 0)
                        {
                            page[i, f-r] |= page[i, f+r];
                            r++;
                        }
                    }
                    y = f-1;
                }
            }


            for (int i = 0; i < y + 1; i++)
            {
                for (int j = 0; j < x + 1; j++)
                {
                    Console.Write(page[j, i] ? '#' : ' ');
                }
                Console.WriteLine();
            }
        }
    }
}
