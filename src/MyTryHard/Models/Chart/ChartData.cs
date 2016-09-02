using System.Collections.Generic;

namespace MyTryHard.Models.Chart
{
    public class ChartData<AxisXT, AxisYT>
    {
        public ChartData() : this("", new List<ChartItem<AxisXT, AxisYT>>())
        { }

        public ChartData(string title, List<ChartItem<AxisXT, AxisYT>> items) : this(title, "", items)
        { }

        public ChartData(string title, string caption, List<ChartItem<AxisXT, AxisYT>> items)
        {
            Title = title;
            Items = items;
            Caption = caption;
            TitlePosition = ChartPosition.Top;
        }

        public ChartPosition TitlePosition { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public List<ChartItem<AxisXT, AxisYT>> Items { get; set; }
    }
}
