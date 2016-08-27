using System.Collections.Generic;

namespace MyTryHard.Models.Tracker
{
    public class PieChart
    {
        public PieChart()
        {
            this.Slices = new List<PieChartSlice>();
        }

        public List<PieChartSlice> Slices { get; set; }

        public class PieChartSlice
        {
            public double Value { get; set; }
            public string DisplayValue { get; set; }
            public string Label { get; set; }
            public string Color { get; set; }
        }

        public static string GetColor(int id)
        {
            switch(id)
            {
                case 0: return "#ccc";
                case 1: return "#AEC6CF";
                case 2: return "#77DD77";
                case 3: return "#DD09FF";
                case 4: return "#FF6961";
                case 5: return "#333366";
                case 6: return "#4323FF";
                case 7: return "#09FFAA";
                case 8: return "#DEADFF";
                case 9: return "#FDADAF";
                default: return "#ccc";
            }
        }
    }
}