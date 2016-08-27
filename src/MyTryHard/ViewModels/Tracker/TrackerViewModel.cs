using MyTryHard.Models.Tracker;
using System.Collections.Generic;

namespace MyTryHard.ViewModels.Tracker
{
    public class TrackerViewModel
    {
        public Dictionary<int, string> SportsList { get; set; }
        public List<TrackerEntry> Entries { get; set; }
    }
}
