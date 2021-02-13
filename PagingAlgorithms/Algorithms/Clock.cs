using System;
using System.Collections.Generic;
using System.Linq;

namespace PagingAlgoritms
{
    public class Clock
    {
        public readonly int FrameNumbers;
        public readonly Process process;
        private int swapPointer = 0;

        public Page[] frames { get; set; }
        public int?[,] AllFrameUpdate { get; set; }
        public bool[] Pagefaults { get; private set; }

        public List<int> PageNeeded { get; set; }

        public Clock(int frameNumbers, Process process, List<int> pageNeeded)
        {
            FrameNumbers = frameNumbers;
            this.process = process;
            PageNeeded = pageNeeded;

            this.frames = new Page[FrameNumbers];
            this.AllFrameUpdate = new int?[frameNumbers, this.PageNeeded.Count];
            this.Pagefaults = new bool[pageNeeded.Count];
        }

        public void Start()
        {
            ClockAlgoritms();
        }




        private void ClockAlgoritms()
        {
            int pageToOut;
            Page inComingPage;
            var PageNeededArray = PageNeeded.ToArray();
            for (int i = 0; i < PageNeeded.Count; i++)
            {
                inComingPage = new Page(process.ProcessName, PageNeededArray[i]);
                if (!frames.Contains(inComingPage))
                {
                    Pagefaults[i] = true;
                    pageToOut = finedLastIndex();
                    frames[pageToOut] = inComingPage;
                    frames[pageToOut].R = true;
                }
                else
                {
                    frames.ToList().Find(inComingPage.Equals).R = true;
                }
                UpdateAllFrame(i,frames);
            }
        }


        private int finedLastIndex()
        {
            int pageToOut;
            while (true)
            {
                if (frames[swapPointer] == null || frames[swapPointer].R == false )
                {
                    pageToOut = swapPointer;
                    swapPointer = (swapPointer + 1) % FrameNumbers;
                    return pageToOut;
                }
                else
                {
                    frames[swapPointer].R = false;
                    swapPointer = (swapPointer + 1) % FrameNumbers;

                }
            }
        }
        
        
        private void UpdateAllFrame(int n, Page[] frames)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                if (frames[i] != null)
                {
                    this.AllFrameUpdate[i, n] = frames[i].PageNumber;
                }
            }
        }


        public void PrintAllchanges()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\npaging in \"CLOCK\" algoritms is : \n\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("REQ : \t");
            Console.ForegroundColor = ConsoleColor.White;


            Console.ForegroundColor = ConsoleColor.DarkCyan;
            foreach (var pageNumber in PageNeeded)
            {
                Console.Write("{0}\t", pageNumber);
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n");

            for (int i = 0; i < FrameNumbers; i++)
            {
                Console.Write("  \t");
                for (int j = 0; j < PageNeeded.Count; j++)
                {
                    if (j == 0 || AllFrameUpdate[i, j] != AllFrameUpdate[i, j - 1])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("{0}\t", this.AllFrameUpdate[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write("{0}\t", this.AllFrameUpdate[i, j]);
                    }
                }

                Console.WriteLine();
            }

            foreach (var fault in Pagefaults)
            {
                if (fault)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\t{0}", "Fault");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("\t{0}", "safe");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            Console.WriteLine();
        }
    }
}