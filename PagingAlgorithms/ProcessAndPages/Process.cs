using System.Collections.Generic;

namespace PagingAlgoritms
{
    public class Process
    {
        public int ProcessId { get; }
        public string ProcessName { get; }
        public int ArrivalTime { get; private set; }
        public int BurstTime { get; private set; }
        public int RemainingTime { get; set; }
        public int EndTime { get; set; }
        public float WaitingTime { get; set; }

        public readonly int PageNumbers;
        public readonly List<Page> Pages;


        public Process(int arrivalTime, int processId, string processName, int burstTime, int pageNumbers)
        {
            ArrivalTime = arrivalTime;
            ProcessId = processId;
            ProcessName = processName;
            BurstTime = RemainingTime = burstTime;
            WaitingTime = float.MinValue;
            EndTime = int.MinValue;
            PageNumbers = pageNumbers;
            Pages = new List<Page>();
            for (int i = 0; i < PageNumbers; i++)
            {
                Pages.Add(new Page(processName, i));
            }
        }


        public Page GetPage(int i)
        {
            Page temp = new Page(ProcessName, i);
            return Pages.Find(temp.Equals);
        }


        protected bool Equals(Process other)
        {
            return ProcessId == other.ProcessId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Process) obj);
        }

        public override int GetHashCode()
        {
            return ProcessId;
        }
    }
}