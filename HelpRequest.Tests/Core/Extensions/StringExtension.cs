﻿using System.Text;

namespace HelpRequest.Tests.Core.Extensions
{
    static class StringExtension
    {
        /// <summary>
        /// Repeats string x times.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="times">The times.</param>
        /// <returns></returns>
        public static string RepeatTimes(this string source, int times)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < times; i++)
            {
                sb.Append(source);
            }
            return sb.ToString();
        }

        public static string ByteArrayToString(this byte[] source)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sb.Append(source[i].ToString());
            }
            return sb.ToString();
        }
    }
}
