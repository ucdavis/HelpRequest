﻿using System.Collections;

namespace HelpRequest.Controllers.Helpers
{
    public static class StringExtension
    {
        /// <summary>
        /// Returns the index of all occurrences of a specific string
        /// </summary>
        /// <remarks>
        /// Code taken from: http://www.dijksterhuis.org/manipulating-strings-in-csharp-finding-all-occurrences-of-a-string-within-another-string/
        /// </remarks>
        /// <param name="source"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static IEnumerable IndexOfAll(this string source, string searchTerm)
        {
            int pos, offset = 0;

            while ((pos = source.IndexOf(searchTerm)) > 0)
            {
                source = source.Substring(pos + searchTerm.Length);
                offset += pos;
                yield return offset;
            }
        }
    }
}