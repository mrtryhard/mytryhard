using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyTryHard.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="maxLength">Without the '...'</param>
        /// <param name="breakWord">Must we break in the middle of a word. True by default.</param>
        /// <returns></returns>
        public static string GetEllipsis(this string original, int maxLength, bool breakWord = true)
        {
            if (maxLength < 0)
                throw new ArgumentOutOfRangeException("maxLength can't be < 0");

            if (string.IsNullOrWhiteSpace(original) || original.Length < maxLength)
                return original;

            // If we can break the word or we found a separator.
            if (breakWord || char.IsSeparator(original[maxLength]))
                return original.Slice(0, maxLength)
                    .Insert(maxLength, "...");

            // Otherwise we must find a separator.
            int endIndex = maxLength;
            for (int i = endIndex; i > 0; --i)
            {
                if (char.IsSeparator(original[i]))
                {
                    endIndex = i;
                    break;
                }
            }

            return original.Slice(0, endIndex).Insert(endIndex, "...");
        }

        /// <summary>
        /// Converts the string to a SEO URL-friendly (aka Slugify)
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        /// <remarks>
        /// Code taken from http://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c 
        /// </remarks>
        public static string ToSEOUrl(this string original)
        {
            string str = original.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        /// <summary>
        /// Removes accent, for SEO URLs.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        /// <remarks>
        /// Code taken from http://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c 
        /// </remarks>
        private static string RemoveAccent(this string original)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(original);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// String slicing
        /// </summary>
        /// <param name="original"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string Slice(this string original, int start, int end)
        {
            if (start < 0 || start >= original.Length)
                throw new ArgumentOutOfRangeException("start", "Parameter must be >= 0 or <= string length");

            if (end < 0 || end >= original.Length)
                throw new ArgumentOutOfRangeException("end", "Parameter must be >= 0 or <= string length");

            if (end <= start)
                throw new ArgumentException("End value must be greater than start value.");

            StringBuilder sb = new StringBuilder(end - start);

            for (int i = start; i != end; ++i)
                sb.Append(original[i]);

            return sb.ToString();
        }

        /// <summary>
        /// Array slicing for object
        /// </summary>
        /// <param name="original"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static object[] Slice(this object[] original, int start, int end)
        {
            if (start < 0 || start >= original.Length)
                throw new ArgumentOutOfRangeException("start", "Parameter must be >= 0 or <= array length");

            if (end < 0 || end >= original.Length)
                throw new ArgumentOutOfRangeException("end", "Parameter must be >= 0 or <= array length");

            if (end <= start)
                throw new ArgumentException("End value must be greater than start value.");

            object[] lst = new object[end - start];

            for (int i = 0; start != end; ++i, ++start)
                lst[i] = original[start];

            return lst;
        }

        /// <summary>
        /// Array for IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> original, int start, int end)
        {
            int count = original.Count();
            if (start < 0 || start >= count)
                throw new ArgumentOutOfRangeException("start", "Parameter must be >= 0 or <= collection length");

            if (end < 0 || end > count)
                throw new ArgumentOutOfRangeException("end", "Parameter must be >= 0 or <= collection length");

            if (end <= start)
                throw new ArgumentException("End value must be greater than start value.");

            List<T> lstOriginal = original.ToList();
            List<T> lstT = new List<T>(lstOriginal.GetRange(start, end - start));

            return lstT.AsEnumerable();
        }

        /// <summary>
        /// Verify if two date times are different by at least the given timespan.
        /// </summary>
        /// <param name="dtFirst"></param>
        /// <param name="dtSecond"></param>
        /// <param name="tsDiff"></param>
        /// <returns>True if different</returns>
        public static bool AreDiffrentBy(this DateTime dtFirst, DateTime dtSecond, TimeSpan tsDiff)
        {
            TimeSpan tsDiffed = (dtFirst - dtSecond);

            if (tsDiffed > tsDiff || tsDiffed < -tsDiff)
                return true;

            return false;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/21608362/missing-addwithvalue-from-dbcommand-parameters
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        public static void AddParameterWithValue(this DbCommand command, string key, object value)
        {
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = key;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
