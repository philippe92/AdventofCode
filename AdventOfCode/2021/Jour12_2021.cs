using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Jour12_2021 : IJour
    {
        List<Tuple<string, string>> links;

        HashSet<string> pathes = new HashSet<string>();

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            links = new List<Tuple<string, string>>();
            foreach(var line in lines)
            {
                var nodes = line.Split('-');
                if (nodes.Length != 2)
                {
                    Console.WriteLine("error!!!");
                    continue;
                }
                links.Add(new Tuple<string, string>(nodes[0], nodes[1]));
            }
        }

        private void GoToNode(List<string> path, string next, bool cangotwice)
        {
            var path_added = new List<string>(path) { next };
            Discover(new List<string>(path_added), cangotwice);
        }

        private void Discover(List<string> path, bool cangotwice)
        {
            var node = path.Last();
            var possible_steps = links.Where(p => p.Item1 == node || p.Item2 == node);
            foreach(var link in possible_steps)
            {
                string next;
                if (node==link.Item1)
                {
                    next = link.Item2;
                }
                else
                {
                    next = link.Item1;
                }
                if (next=="start")
                {
                    //Cannot go back to start
                    continue;
                }
                if (next=="end")
                {
                    var tmp = new List<string>(path);
                    tmp.Add(next);
                    var str = tmp.Aggregate((a, b) => a + b);
                    pathes.Add(str);
                    continue;
                }
                if (next == next.ToLower())
                {
                    //small cave.
                    if (!cangotwice && path.Contains(next))
                    {
                        // path not possible
                        continue;
                    }
                    if (cangotwice && path.Count(p => p == next) == 2)
                    {
                        continue;
                    }

                    var newcangotwice = cangotwice;
                    if (cangotwice && path.Contains(next))
                    {
                        newcangotwice = false;
                    }
                    GoToNode(path, next, newcangotwice);
                }
                else
                {
                    GoToNode(path, next, cangotwice);
                }
            }
        }

        public void ResolveFirstPart()
        {
            List<string> path = new List<string>();
            GoToNode(path, "start", false);
            Console.WriteLine($"Result : {pathes.Count}");
        }

        public void ResolveSecondPart()
        {
            List<string> path = new List<string>();
            GoToNode(path, "start", true);
            Console.WriteLine($"Result : {pathes.Count}");
        }
    }
}
