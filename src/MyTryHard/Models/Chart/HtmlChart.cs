using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyTryHard.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MyTryHard.Models.Chart
{
    public static class HtmlChart
    {
        const double radius = 250;
        const double radOffset = 255;

        private static TagBuilder generateBaseSVG(string id, int width, int height, SVGViewBox viewBox, string aspectRatio)
        {
            TagBuilder builder = new TagBuilder("svg");

            if (!string.IsNullOrWhiteSpace(id))
            {
                builder.GenerateId(id, "");
            }

            builder.MergeAttribute("height", height.ToString());
            builder.MergeAttribute("width", width.ToString());
            builder.MergeAttribute("viewBox", viewBox.ToString());
            builder.MergeAttribute("preserveAspectRatio", "xMinYMin meet");

            return builder;
        }

        // TODO : rethink this. it just doesn't make sense at all. 
        public static IHtmlContent PieChartFor<U, T>(this IHtmlHelper helper, ChartData<U, T> data, string id = "", int width = 255, int height = 400)
        {
            TagBuilder svgBuilder = generateBaseSVG(id,
                width, height,
                new SVGViewBox(0, 0, 800, 610),
                "xMinYMin meet");

            const int textAlign = 555;
            const int lastTextOffset = 40;

            int lastTextHeight = 55;
            int heightOffset = 0;

            // TODO: try to improve this. quite slow to convert everytime.
            double totalValues = data.Items.Sum(x => Convert.ToDouble(x.ValueY));
            double anglePerValue = 360.0d / totalValues;

            double startAngle = 0, endAngle = 0;
            string css = "<style type=\"text/css\">" +
                ".slice:hover {" +
                "filter: brightness(120%) drop-shadow(0.4em 0.4em 2px rgba(0, 0, 0, 0.5));" +
                "stroke: #00B2EE;" +
                "stroke-width: 2;" +
                "}" +
                "text {" +
                "font-family: \"Open Sans\", Tahoma, serif;" +
                "}" +
                "g text {" +
                "font-size: 1.4rem;" +
                "}" +
                "text.title {" +
                "fill: black;" +
                "font-size: 2rem;" +
                "font-weight: bold;" +
                "}" +
                "</style>";
            svgBuilder.InnerHtml.AppendHtml(css);

            if(!string.IsNullOrEmpty(data.Title))
            {
                if(data.TitlePosition == ChartPosition.Top)
                {
                    heightOffset = 70;
                    int titleX = (500 - data.Title.Length * 5) / 2;
                    TagBuilder titleBuilder = new TagBuilder("text");
                    titleBuilder.AddCssClass("title");
                    titleBuilder.MergeAttribute("x", titleX.ToString());
                    titleBuilder.MergeAttribute("y", 35.ToString());
                    titleBuilder.InnerHtml.Append(data.Title);
                    svgBuilder.InnerHtml.AppendHtml(titleBuilder);
                }
            }

            lastTextHeight += heightOffset;

            // TODO add css
            foreach (ChartItem<U, T> item in data.Items)
            {
                string d = GetDForPath(ref startAngle, ref endAngle, heightOffset, anglePerValue, Convert.ToDouble(item.ValueY));
                TagBuilder itemBuilder = generateItem(item, d, textAlign, lastTextHeight, totalValues);
                svgBuilder.InnerHtml.AppendHtml(itemBuilder);
                lastTextHeight += lastTextOffset;
            }

            return svgBuilder;
        }

        private static string GetDForPath(ref double startAngle, ref double endAngle, double heightOffset, double anglePerValue, double value)
        {
            startAngle = endAngle;
            endAngle = startAngle + value * anglePerValue;

            var startAngleRad = Math.PI * startAngle / 180;
            var endAngleRad = Math.PI * endAngle / 180;

            // First coord of arc.
            var x1 = radOffset + radius * Math.Cos(startAngleRad);
            var y1 = radOffset + heightOffset + radius * Math.Sin(startAngleRad);

            // Second coord of arc
            var x2 = radOffset + radius * Math.Cos(endAngleRad);
            var y2 = radOffset + heightOffset + radius * Math.Sin(endAngleRad);

            int bigArc = (endAngle - startAngle) > 180 ? 1 : 0;

            return
                string.Format("M{0},{1} L{2},{3} A250,250 0 {4},1 {5},{6} Z L{7},{8}",
                radOffset, (250 + heightOffset), x1.NumString(), y1.NumString(), bigArc, x2.NumString(), y2.NumString(), x2.NumString(), y2.NumString());
        }

        private static TagBuilder generateItem<U, T>(ChartItem<U, T> item, string d, int textAlign, int lastTextHeight, double totalValues)
        {
            TagBuilder gBuilder = new TagBuilder("g");
            TagBuilder titleBuilder = new TagBuilder("title");
            titleBuilder.InnerHtml.AppendFormat("{0} ({1})", item.LabelY, item.ValueY);

            TagBuilder bulletBuilder = generateTextBullet("■", item.Color, textAlign - 25, lastTextHeight - 1);
            TagBuilder labelBuilder = generateLabel(
                string.Format("{0} - {1} ({2})", (Convert.ToDouble(item.ValueY) / totalValues).ToString("0.00 %"), item.LabelY, item.ValueY),
                textAlign, lastTextHeight);
            TagBuilder pathBuilder = generatePath(d, item.Color);

            gBuilder.InnerHtml.AppendHtml(titleBuilder);
            gBuilder.InnerHtml.AppendHtml(bulletBuilder);
            gBuilder.InnerHtml.AppendHtml(labelBuilder);
            gBuilder.InnerHtml.AppendHtml(pathBuilder);
            return gBuilder;
        }

        private static TagBuilder generatePath(string d, ChartColor color)
        {
            TagBuilder builder = new TagBuilder("path");
            builder.AddCssClass("slice");
            builder.MergeAttribute("d", d);
            builder.MergeAttribute("stroke-linejoin", "miter");
            builder.MergeAttribute("stroke", "black");
            builder.MergeAttribute("stroke-width", "1");
            builder.MergeAttribute("fill", color.HexCode());
            builder.TagRenderMode = TagRenderMode.SelfClosing;

            return builder;
        }

        private static TagBuilder generateLabel(string value, int x, int y)
        {
            TagBuilder builder = new TagBuilder("text");
            builder.MergeAttribute("x", x.ToString());
            builder.MergeAttribute("y", y.ToString());
            builder.InnerHtml.Append(value);
            return builder;
        }

        private static TagBuilder generateTextBullet(string bullet, ChartColor color, int x, int y)
        {
            TagBuilder builder = new TagBuilder("text");
            builder.MergeAttribute("x", x.ToString());
            builder.MergeAttribute("y", y.ToString());
            builder.MergeAttribute("fill", color.HexCode());
            builder.MergeAttribute("stroke", "black");
            builder.MergeAttribute("style", "font-size: 2rem;");
            builder.MergeAttribute("stroke-width", "2");
            builder.InnerHtml.Append(bullet);

            return builder;
        }

        protected class SVGViewBox
        {
            public SVGViewBox(int x, int y, int width, int height)
            {
                X = x;
                Y = y;
                Width = width;
                Height = height;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

            public override string ToString()
            {
                return string.Format("{0}, {1}, {2}, {3}", X, Y, Width, Height);
            }
        }
    }
}
