using System;

namespace PagingAlgoritms
{
    public class Page
    {

        public readonly string ProcessName;
        public readonly int PageNumber;
        public bool R { get; set; }
        public bool M { get; set; }

        public Page(string processName, int pageNumber)
        {
            ProcessName = processName;
            PageNumber = pageNumber;
        }

        public void PageRecentlyUsed()
        {
            this.R = true;
        }

        public void PageNotRecentlyUsed()
        {
            this.R = false;
        }
        
        
        
        
        
        

        protected bool Equals(Page other)
        {
            return ProcessName == other.ProcessName && PageNumber == other.PageNumber;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Page) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ProcessName != null ? ProcessName.GetHashCode() : 0) * 397) ^ PageNumber;
            }
        }
    }
}