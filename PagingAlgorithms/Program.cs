using System;
using System.Collections.Generic;
using System.Linq;

namespace PagingAlgoritms
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = new Process(10, 1000, "a", 100, 14);
            int[] listOfPagesReq = new[]
                {1, 2, 3, 4, 5, 2, 1, 3, 3, 2, 3, 4, 5, 4};


            OPT opt = new OPT(3, p, listOfPagesReq.ToList());
            opt.Start();
            opt.PrintAllchanges();

            Console.WriteLine(
                "\n\n-----------------------------------------------------------------------------------------\n");


            LRU lru = new LRU(3, p, listOfPagesReq.ToList());
            lru.Start();
            lru.PrintAllchanges();

            Console.WriteLine(
                "\n\n-----------------------------------------------------------------------------------------\n");

            Clock clock = new Clock(3, p, listOfPagesReq.ToList());
            clock.Start();
            clock.PrintAllchanges();
        }
    }
}