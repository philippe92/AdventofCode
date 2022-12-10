using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Jour16_2021 : IJour
    {
        string input;
        Packet root;

        private class Packet
        {
            public Packet()
            {
                Children = new List<Packet>();
            }
            public int Version { get; set; }
            public int Type { get; set; }
            public ulong Value { get; set; }
            public List<Packet> Children { get; set; }

            public int GetVersionSum()
            {
                return Version + Children.Select(p => p.GetVersionSum()).Sum();
            }

            public ulong Calculate()
            {
                ulong result = 0;
                switch (Type)
                {
                    case 0:
                        result = Children.Select(p => p.Calculate()).Aggregate((a, b) => a + b);
                        break;

                    case 1:
                        result = Children.Select(p => p.Calculate()).Aggregate((a, b) => a * b);
                        break;
                    case 2:
                        result = Children.Select(p => p.Calculate()).Min();
                        break;
                    case 3:
                        result = Children.Select(p => p.Calculate()).Max();
                        break;
                    case 4:
                        result = Value;
                        break;
                    case 5:
                        result = Children[0].Calculate() > Children[1].Calculate() ? (ulong)1: 0;
                        break;
                    case 6:
                        result = Children[0].Calculate() < Children[1].Calculate() ? (ulong)1 : 0;
                        break;
                    case 7:
                        result = Children[0].Calculate() == Children[1].Calculate() ? (ulong)1 : 0;
                        break;

                }
                return result;
            }
        }

        public void Init(string inputfile)
        {
            input = File.ReadLines(inputfile).First();
            CalculatePackets();
        }

        private bool GetBit(long i, int bitNumber)
        {
            return (i & (1 << bitNumber)) != 0;
        }

        private int ReadInt(bool[] data, int offset, int len)
        {
            int result = 0;
            for (int i = 0; i < len; i++)
            {
                if (data[offset + len - 1 - i])
                {
                    result += 1 << i;
                }
            }
            return result;
        }

        private ulong ReadBlocks(bool[] data, int offset, out int size)
        {
            ulong result = 0;
            int len = 0;
            while (data[offset + len * 5])
            {
                len++;
            }
            len++;
            for (int i = 0; i < len; i++)
            {
                var blockoffset = offset + (len - 1 - i) * 5;
                for (int j = 0; j < 4; j++)
                {
                    if (data[blockoffset + (5 - j - 1)])
                    {
                        result += (ulong)1 << j + 4 * i;
                    }
                }
            }
            size = len * 5;
            return result;
        }

        private int ReadPacket(bool[] data, int offset, Packet packet)
        {
            packet.Version = ReadInt(data, offset, 3);
            packet.Type = ReadInt(data, offset + 3, 3);
            int size = 6;
            if (packet.Type == 4)
            {
                packet.Value = ReadBlocks(data, offset + 6, out var blocksSize);
                size += blocksSize;
            }
            else
            {
                size++;
                if (data[offset + 6])
                {
                    var subpacketnb = ReadInt(data, offset + 7, 11);
                    size += 11;
                    int currentnb = 0;
                    while (currentnb < subpacketnb)
                    {
                        var subpacket = new Packet();
                        size += ReadPacket(data, offset + size, subpacket);
                        packet.Children.Add(subpacket);
                        currentnb++;
                    }
                }
                else
                {
                    var subpacketsize = ReadInt(data, offset + 7, 15);
                    size += 15;
                    int currentsize = 0;
                    while (currentsize < subpacketsize)
                    {
                        var subpacket = new Packet();
                        currentsize += ReadPacket(data, offset + size + currentsize, subpacket);
                        packet.Children.Add(subpacket);
                    }
                    if (subpacketsize != currentsize)
                    {
                        Console.WriteLine("bizarre, le subpacket ne correspond pas à la taille!!!");
                    }
                    size += subpacketsize;
                }
            }
            return size;
        }

        void CalculatePackets()
        {
            var size = input.Length * 4;
            bool[] data = new bool[size];
            for (int i = 0; i < input.Length; i++)
            {
                int val = int.Parse(input.Substring(i, 1), System.Globalization.NumberStyles.HexNumber);
                for (int j = 0; j < 4; j++)
                {
                    data[i * 4 + j] = GetBit(val, 3 - j);
                }
            }

            root = new Packet();
            ReadPacket(data, 0, root);
        }

        public void ResolveFirstPart()
        {
            var result = root.GetVersionSum();
            Console.WriteLine($"Result : {result}");
        }

        public void ResolveSecondPart()
        {
            var result = root.Calculate();
            Console.WriteLine($"Result : {result}");
        }
    }
}
