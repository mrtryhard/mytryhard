using MyTryHard.FiltersAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTryHard.Models.Tracker
{
    [AllowSqlAutoFill]
    public class TrackerEntry
    {
        public int EntryId { get; set; }

        public Guid UserId { get; set; }

        public int SportId { get; set; }

        /// <summary>
        /// Distance in meters.
        /// </summary>
        public int Distance { get; set; }

        public DateTime DateTimeStart { get; set; }

        public DateTime DateTimeEnd { get; set; }

        public string SportName { get; set; }
    }
}
