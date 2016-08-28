using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;

namespace MyTryHard.Models.Chart
{
    public static class HtmlChart
    {
        public static IHtmlContent PieChartFor(this IHtmlHelper helper, ChartData data, string id = "", int width = 255, int height = 400)
        {
            ViewDataDictionary vdd = new ViewDataDictionary(helper.ViewContext.ViewData);
            vdd.Add("width", width);
            vdd.Add("height", height);
            vdd.Add("id", id);

            return helper.Partial("Chart/PieChart", data, vdd);
        }
    }
}
