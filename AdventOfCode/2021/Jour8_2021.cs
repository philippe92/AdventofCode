using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Jour8_2021 : IJour
    {
        List<List<string>> lines;

        public void Init(string inputfile)
        {
            lines = new List<List<string>>();
            var rx = new Regex(@"([a-g]+) ([a-g]+) ([a-g]+) ([a-g]+) ([a-g]+) ([a-g]+) ([a-g]+) ([a-g]+) ([a-g]+) ([a-g]+) \| ([a-g]+) ([a-g]+) ([a-g]+) ([a-g]+)", RegexOptions.Compiled);
            string[] input = File.ReadAllLines(inputfile);
            foreach (var line in input)
            {
                var r = rx.Match(line);
                if (!r.Success)
                {
                    Console.WriteLine("Erreur de donnée !!!");
                    continue;
                }
                var item = new List<string>();
                for (int i=1;i<15;i++)
                {
                    item.Add(r.Groups[i].Value);
                }
                lines.Add(item);
            }
        }

        public void ResolveFirstPart()
        {
            List<int> uniques = new List<int> { 2, 4, 3, 7 };
            int count = 0;
            for (int i=0;i<lines.Count;i++)
            {
                for (int j=10; j<14; j++)
                {
                    if (uniques.Contains(lines[i][j].Length))
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine($"Resulat = {count}");
        }

        private readonly List<char> all_segments = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
        bool[,] rmatrice;

        private void AddCombination(string s, params int[] positions)
        {
            // Gere les segments actifs : tous les autres elements de la string sont impossible dessus
            for (int i=0; i < positions.Length; i++)
            {
                for (int j=0;j<7;j++)
                {
                    if (s.IndexOf(all_segments[j]) == -1)
                    {
                        rmatrice[positions[i],j] = true;
                    }
                }
            }

            // Gere les segments inactifs : tous les elements de la string sont impossible dessus
            for (int i = 0; i < 7; i++)
            {
                if (Array.IndexOf(positions, i) == -1)
                {
                    for (int j = 0; j < positions.Length; j++)
                    {
                        rmatrice[i, (s[j]-'a')] = true;                        
                    }
                }
            }

        }

        //  aaaa  
        // b    c 
        // b    c 
        //  dddd  
        // e    f 
        // e    f 
        //  gggg  

        private readonly Dictionary<string, int> mapping = new Dictionary<string, int>
        {
            { "abcefg", 0 },
            { "cf" , 1},
            { "acdeg", 2 },
            { "acdfg", 3 },
            { "bcdf", 4 },
            { "abdfg", 5 },
            { "abdefg", 6 },
            { "acf", 7 },
            { "abcdefg", 8 },
            { "abcdfg", 9 }
        };

        private void DumpMatriceTable()
        {
            for (int x = 0; x < 7; x++)
            {
                Console.Write("{");
                for (int y = 0; y < 7; y++)
                {
                    Console.Write(rmatrice[x, y] ? '1' : '0');
                    if ( y < 6)
                    {
                        Console.Write(',');
                    }
                }
                Console.Write("},");
            }
        }

        private void DumpMatrice()
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    Console.Write(rmatrice[x, y] ? '1' : '0');
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }


        private string GetNotCommons(List<string> list)
        {
            string ret = string.Empty;
            foreach (var s in list)
            {
                foreach (var c in s)
                {
                    bool iscommon = true;
                    foreach(var s2 in list)
                    {
                        if (s2==s)
                        {
                            continue;
                        }
                        if (!s2.Contains(c))
                        {
                            iscommon = false;
                            break;
                        }
                    }
                    if (!iscommon && !ret.Contains(c))
                    {
                        ret = ret + c;
                    }
                }
            }
            return ret;
        }

        private string Transpose(string s, Dictionary<char, char> mapping)
        {
            StringBuilder ret = new StringBuilder();
            foreach(var c in s)
            {
                ret.Append(mapping[c]);
            }
            return ret.ToString();
        }

        public void ResolveSecondPart()
        {
            int global_result = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                rmatrice = new bool[7, 7];
                List<string> chars_list = lines[i].Take(10).ToList();

                var string2 = chars_list.First(p => p.Length == 2);
                // C'est le nombre 1, les segments 3 et 6
                AddCombination(string2, 2, 5);

                var string3 = chars_list.First(p => p.Length == 3);
                // C'est le nombre 7
                AddCombination(string3, 0, 2, 5);

                var string4 = chars_list.First(p => p.Length == 4);
                // C'est le nombre 4
                AddCombination(string4, 1, 2, 3, 5);

                var strings5 = chars_list.Where(p => p.Length == 6).ToList();
                // il est censé en avoir 3, le 0, 6 et 9
                var d = GetNotCommons(strings5);
                AddCombination(d, 2, 3, 4);

                Dictionary<char ,char> final_mapping = new Dictionary<char, char>();
                for (int x=0; x<7; x++)
                {
                    int found = -1;
                    for (int y=0;y<7;y++)
                    {
                        if (!rmatrice[x,y])
                        {
                            found = y;
                            break;
                        }
                    }
                    if (found == -1)
                    {
                        Console.WriteLine("erreur, la matrice a foiré!");
                    }
                    else
                    {
                        final_mapping.Add((char)(97+ found), (char)(97 + x));
                    }
                }

                var values = lines[i].Skip(10).Take(4).ToList();
                values.Reverse();
                int result = 0;
                int factor = 1;
                foreach (var e in values)
                {
                    var transposed = Transpose(e, final_mapping);
                    var sorted = String.Concat(transposed.OrderBy(c => c));
                    result += mapping[sorted] * factor;
                    factor *= 10;
                }
                global_result += result;
            }
            Console.WriteLine($"result : {global_result}");
        }
    }
}
