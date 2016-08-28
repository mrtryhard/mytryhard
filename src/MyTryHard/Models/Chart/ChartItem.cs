using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTryHard.Models.Chart
{
    public class ChartItem
    {
        public double Value { get; set; }
        public string DisplayValue { get; set; }
        public string Label { get; set; }
        public ChartColor Color { get; set; }

        public static ChartColor GetColorFromId(int id)
        {
            switch (id)
            {
                case 0: return ChartColor.PastelBlue;
                case 1: return ChartColor.PastelGreen;
                case 2: return ChartColor.PastelOrange;
                case 3: return ChartColor.PastelPurple;
                case 4: return ChartColor.PastelRed;
                case 5: return ChartColor.PastelYellow;
                case 6: return ChartColor.DarkPurple;
                case 7: return ChartColor.DarkRed;
                case 8: return ChartColor.LightGray;
                case 9: return ChartColor.Aqua;
                default: goto case 0;
            }
        }
    }
}
