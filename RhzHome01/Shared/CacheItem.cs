using System;
using System.Collections.Generic;
using System.Text;

namespace RhzHome01.Shared
{
    public class CacheItem
    {
        public DateTimeOffset CreatedOn { get; set; }
        public int Age
        {
            get
            {
                var currentDate = DateTimeOffset.UtcNow;
                long txs = currentDate.Ticks - CreatedOn.Ticks;

                double seconds = TimeSpan.FromTicks(txs).TotalSeconds;
                int age = (int)seconds;

                return age;
            }

        }
        public int MaxAge { get; set; }
        public object Item { get; set; }
    }
}
