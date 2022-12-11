using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Jour7_2022 : IJour
    {
        private class Node
        {
            public bool IsDirectory { get; set; }
            public string Name { get; set; }
            public int Size { get; set; }
            public List<Node> Children { get; set; }
            public int Total_size { get; set; }
        }

        string[] lines;
        Node root_node;
        Regex rxfile;
        Regex rxcd;

        public void Init(string inputfile)
        {
            lines = File.ReadAllLines(inputfile);
            rxfile = new Regex(@"([0-9]+) ([a-z\.]+)", RegexOptions.Compiled);
            rxcd = new Regex(@"\$ cd ([a-z\.]+)", RegexOptions.Compiled);
            int i = 2;
            bool stop = false;
            var children = ProcessFolder(ref i, ref stop);
            root_node = new Node { IsDirectory = true, Name = "/", Children = children };
            CalculateSize(root_node);
        }

        private List<Node> ProcessFolder(ref int i, ref bool stop)
        {
            List<Node> nodes = new List<Node>();
            while (!stop && lines[i] != "$ cd ..")
            {
                var rfile = rxfile.Match(lines[i]);
                var rcd = rxcd.Match(lines[i]);
                if (i < lines.Length - 1)
                {
                    i++;
                }
                else
                {
                    stop = true;
                }
                if (rfile.Success)
                {
                    var node = new Node { IsDirectory = false, Name = rfile.Groups[2].Value, Size = int.Parse(rfile.Groups[1].Value) };
                    nodes.Add(node);
                }
                else if (rcd.Success)
                {
                    var node = new Node { IsDirectory = true, Name = rcd.Groups[1].Value, Children = ProcessFolder(ref i, ref stop) };
                    nodes.Add(node);
                }
            } 
            if (i < lines.Length-1)
            {
                i++;
            }
            return nodes;
        }

        private int CalculateSize(Node node)
        {
            int size = 0;
            foreach(var child in node.Children)
            {
                if (child.IsDirectory)
                {
                    size += CalculateSize(child);
                }
                else
                {
                    size += child.Size;
                }
            }
            node.Total_size = size;
            return size;
        }

        private List<Node> selectednodes;

        private void AggregateSmallNodes(Node node)
        {
            if (node.Children == null)
            {
                return;
            }
            foreach (var child in node.Children)
            {
                if (child.Total_size <= 100000)
                {
                    selectednodes.Add(child);
                }
                AggregateSmallNodes(child);
            }
        }

        public void ResolveFirstPart()
        {
            selectednodes = new List<Node>();
            AggregateSmallNodes(root_node);
            int resultat = 0;
            selectednodes.ForEach(e => resultat += e.Total_size);

            Console.WriteLine($"Resultat = {resultat}");
        }

        int sizetofree;

        private void AggregateEligibleNodes(Node node)
        {
            if (node.Children == null)
            {
                return;
            }
            foreach (var child in node.Children)
            {
                if (child.Total_size > sizetofree)
                {
                    selectednodes.Add(child);
                }
                AggregateEligibleNodes(child);
            }
        }

        public void ResolveSecondPart()
        {
            selectednodes.Clear();
            int freespace = 70000000 - root_node.Total_size;
            sizetofree = 30000000 - freespace;
            AggregateEligibleNodes(root_node);
            if (root_node.Total_size > sizetofree)
            {
                selectednodes.Add(root_node);
            }
            selectednodes.Sort((a, b) => a.Total_size.CompareTo(b.Total_size));
            Console.WriteLine($"Resultat = {selectednodes[0].Total_size}");
        }
    }
}
