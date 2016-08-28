using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTryHard.Models.Chart
{
    public class ChartData
    {
        public ChartData() : this("", new List<ChartItem>())
        { }

        public ChartData(string title, List<ChartItem> items) : this(title, "", items)
        { }

        public ChartData(string title, string caption, List<ChartItem> items)
        {
            Title = title;
            Items = items;
            Caption = caption;
            TitlePosition = ChartPosition.Top;
        }

        public ChartPosition TitlePosition { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public List<ChartItem> Items { get; set; }
    }
}
