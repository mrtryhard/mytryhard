using MyTryHard.Helpers;
using System;
using System.Text;

namespace MyTryHard.Models.Chart
{
    public enum ChartColor
    {
        Blue = 0x0000ff,
        Aqua = 0x008dc3,
        Green = 0x00ab0c,
        Cyan = 0x00ffff,
        DarkPurple = 0x5a007a,
        PastelBlue = 0x71bbff,
        PastelGreen = 0x7cff71,
        PastelPurple = 0xa071ff,
        DarkRed = 0xc30000,
        LightGray = 0xcccccc,
        PastelYellow = 0xefff71,
        Red = 0xff0000,
        PastelRed = 0xff7171,
        Orange = 0xffa700,
        PastelOrange = 0xffc771,
        Magenta = 0xff00ff,
        Yellow = 0xffff00,
        White = 0xffffff
    }

    public static class ChartColorExtension
    {
        public static string HexCode(this ChartColor color)
        {
            return "#" + color.ToString("X").Substring(2);
        }
    }
}

