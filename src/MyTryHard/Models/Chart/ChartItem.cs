namespace MyTryHard.Models.Chart
{
    public class ChartItem<AxisXT, AxisYT>
    {
        public string LabelY { get; set; }
        public AxisYT ValueY { get; set; }
        public string LabelX { get; set; }
        public AxisXT ValueX { get; set; }
        public ChartColor Color { get; set; }
    }
}
