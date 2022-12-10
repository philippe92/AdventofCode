using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Jour14_2020 : IJour
    {
        Dictionary<ulong, ulong> memory;

        private class Op
        {
            public Op(string mask, ulong addr, ulong value)
            {
                Addr = addr;
                Value = value;
                Mask = mask;

                highMask = 0;
                lowMask = 0;
                for (int i = 0; i < 36; i++)
                {
                    if (mask[i] == '0')
                    {
                        lowMask = SetBit(lowMask, 35 - i);
                    }
                    if (mask[i] == '1')
                    {
                        highMask = SetBit(highMask, 35 - i);
                    }
                    
                }
                lowMask = ~lowMask;
            }
            private ulong highMask;
            private ulong lowMask;
            private string Mask;
            public ulong Addr { get; private set; }
            public ulong Value { get; private set; }

            public ulong CalculateValue()
            {
                ulong result = Value;
                result |= highMask;
                result &= lowMask;
                return result;
            }

            private bool GetBit(int  i, int bitNumber)
            {
                return (i & (1 << bitNumber)) != 0;
            }

            private void PrintBinary(ulong u)
            {
                char[] b = new char[36];
                int pos = 35;
                int i = 0;

                while (i < 36)
                {
                    if ((u & ((ulong)1 << i)) != 0)
                    {
                        b[pos] = '1';
                    }
                    else
                    {
                        b[pos] = '0';
                    }
                    pos--;
                    i++;
                }
                Console.WriteLine(b);
            }

            private ulong SetBit(ulong u, int bitNumber)
            {
                return u | ((ulong)1 << bitNumber);
            }

            private ulong UnsetBit(ulong u, int bitNumber)
            {
                return u & ~ ((ulong)1 << bitNumber);
            }

            public ulong[] CalculateAddresses()
            {
                var tmp = Addr |= highMask;
                var count = Mask.ToCharArray().Count(p => p == 'X');
                var positions = new int[count];
                int c = 0;
                for (int i = 0; i<36;i++)
                {
                    if (Mask[35-i] == 'X')
                    {
                        positions[c] = i;
                        c++;
                    }
                }
                int nb_combi = 1 << count;
                ulong[] result = new ulong[nb_combi];
                //Console.WriteLine("------");
                for (int i = 0; i < nb_combi; i++)
                {
                    ulong u = tmp;
                    for (int j=0; j<count; j++)
                    {
                        if (GetBit(i, j))
                        {
                            u = SetBit(u, positions[j]);
                        }
                        else
                        {
                            u = UnsetBit(u, positions[j]);
                        }
                    }
                    //Console.WriteLine(u);
                    result[i] = u;
                }
                return result;
            }
        }

        List<Op> ops = new List<Op>();

        public void Init(string inputfile)
        {
            var rx = new Regex(@"mask = ([X|0|1]+)", RegexOptions.Compiled);
            var rx2 = new Regex(@"mem\[(\d+)\] = (\d+)", RegexOptions.Compiled);
            string[] lines = File.ReadAllLines(inputfile);

            string mask = string.Empty;
            foreach (var line in lines)
            {
                var r = rx.Match(line);
                if (r.Success)
                {
                    mask = r.Groups[1].Value;
                    continue;
                }

                r = rx2.Match(line);
                if (!r.Success)
                {
                    Console.WriteLine("Error!!!!!");
                    continue;
                }
                ops.Add(new Op(mask, ulong.Parse(r.Groups[1].Value), ulong.Parse(r.Groups[2].Value)));
            }
        }

        public void ResolveFirstPart()
        {
            memory = new Dictionary<ulong, ulong>();
            foreach (var op in ops)
            {
                memory[op.Addr] = op.CalculateValue();
            }

            ulong result = 0;
            foreach (var e in memory.Values)
            {
                result += e;
            }
            Console.WriteLine($"Resultat = {result}");
        }

        public void ResolveSecondPart()
        {
            memory = new Dictionary<ulong, ulong>();
            foreach (var op in ops)
            {
                var adresses = op.CalculateAddresses();
                foreach (var a in adresses)
                {
                    memory[a] = op.Value;
                }
            }

            ulong result = 0;
            foreach (var e in memory.Values)
            {
                result += e;
            }
            Console.WriteLine($"Resultat = {result}");
        }
    }
}
