using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Jour13_2020 : IJour
    {
        int timestamp;
        List<int> bus_lines = new List<int>();

        Dictionary<int, int> schedule = new Dictionary<int, int>();

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            timestamp = int.Parse(lines[0]);
            var tmp = lines[1].Split(',');
            int i = 0;
            foreach(string s in tmp)
            {
                if (s == "x")
                {
                    i++;
                    continue;
                }
                bus_lines.Add(int.Parse(s));
                schedule[int.Parse(s)] = i;
                i++;
            }
        }

        private bool FindBus(int ts, out int bus_id)
        {
            foreach (int i in bus_lines)
            {
                if (ts % i == 0)
                {
                    bus_id = i;
                    return true;
                }
            }
            bus_id = 0;
            return false;
        }

        public void ResolveFirstPart()
        {
            int ts = timestamp;
            int result;
            while (true)
            {
                if (FindBus(ts, out int bus_id))
                {
                    result = (ts - timestamp) * bus_id;
                    break;
                }
                ts++;
            }
            Console.WriteLine($"Resultat = {result}");
        }

        private const long blocksize = 100000000000;

        private long DoBlock(int id_block, int ts_ref, int inc)
        {
            long ts = id_block*inc* blocksize;
            Console.WriteLine($"Block {id_block} : Starting at {ts}");
            bool found = false;
            while (!found && ts<= (id_block+1) * inc * blocksize)
            {
                found = true;
                foreach (var e in schedule.Keys)
                {
                    if ((ts + schedule[e] - ts_ref) % e != 0)
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return ts - ts_ref;
                }
                ts += inc;
            }
            return 0;
        }

        private async void AsyncLaunch(int i, int ts_ref, int inc)
        {
            long result = await Task.Run(() => DoBlock(i, ts_ref, inc));
            Console.WriteLine($"Resultat Block {i} : {result}");
        }

        public void ResolveSecondPart()
        {
            // Result in about 30mn...
            var inc = schedule.Keys.Max();
            var ts_ref = schedule[inc];

            for (int i=0;i<24;i++)
            {
                AsyncLaunch(i, ts_ref, inc);
            }
        }
    }
}
