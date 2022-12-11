using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            /*ProcessTask(new Jour3_2020(), 3);
            ProcessTask(new Jour4_2020(), 4);
            ProcessTask(new Jour5_2020(), 5);
            ProcessTask(new Jour6_2020(), 6);
            ProcessTask(new Jour7_2020(), 7);
            ProcessTask(new Jour8_2020(), 8);
            ProcessTask(new Jour9_2020(), 9);
            ProcessTask(new Jour10_2020(), 10);
            ProcessTask(new Jour11_2020(), 11);
            ProcessTask(new Jour12_2020(), 12);
            ProcessTask(new Jour13_2020(), 13);
            ProcessTask(new Jour14_2020(), 14, 0);
            ProcessTask(new Jour15_2020(), 15, 0);
            ProcessTask(new Jour16_2020(), 16, 0);
            ProcessTask(new Jour17_2020(), 17, 0);
            ProcessTask(new Jour18_2020(), 18, 0);
            ProcessTask(new Jour19_2020(), 19, 0);
            ProcessTask(new Jour20_2020(), 19, 0);*/

            //ProcessTask(new Jour1_2021(), 1);
            //ProcessTask(new Jour2_2021(), 2);
            //ProcessTask(new Jour3_2021(), 3);
            //ProcessTask(new Jour4_2021(), 4);
            //ProcessTask(new Jour5_2021(), 5);
            //ProcessTask(new Jour6_2021(), 6);
            //ProcessTask(new Jour7_2021(), 7);
            //ProcessTask(new Jour8_2021(), 8);
            //ProcessTask(new Jour9_2021(), 9);
            //ProcessTask(new Jour10_2021(), 10);
            //ProcessTask(new Jour11_2021(), 11);
            //ProcessTask(new Jour12_2021(), 12);
            //ProcessTask(new Jour13_2021(), 13);
            //ProcessTask(new Jour14_2021(), 14);
            //ProcessTask(new Jour15_2021(), 15);
            //ProcessTask(new Jour16_2021(), 16);
            //ProcessTask(new Jour17_2021(), 17);
            //ProcessTask(new Jour18_2021(), 18, 1);

            //ProcessTask(new Jour1_2022(), 1);
            //ProcessTask(new Jour2_2022(), 2);
            //ProcessTask(new Jour3_2022(), 3);
            //ProcessTask(new Jour4_2022(), 4);
            //ProcessTask(new Jour5_2022(), 5);
            //ProcessTask(new Jour6_2022(), 6);
            //ProcessTask(new Jour7_2022(), 7);
            ProcessTask(new Jour8_2022(), 8);

            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();
        }

        private static void ProcessTask(IJour jour, int day, int test = 0)
        {
            Console.WriteLine( "*****************************");
            Console.WriteLine($"*  Executing Day {day} !!!!     *");
            Console.WriteLine( "*****************************");
            jour.Init($@"..\..\2022\input-day{day}{(test > 0 ? "test" + test : "")}.txt");

            Console.WriteLine("-- Resolving First Part --");
            jour.ResolveFirstPart();
            Console.WriteLine("-- Resolving Second Part --");
            jour.ResolveSecondPart();
        }
    }
}

