using System;
using System.Collections.Generic;

namespace it.example.dotnetcore5.webapi.Helpers
{
    /// <summary>
    /// Helper for Querystring
    /// </summary>
    public static class QuerystringHelper
    {

        /// <summary>
        /// Retrieve int value from querystring
        /// </summary>
        /// <param name="dict">Querystring dictionary</param>
        /// <param name="key">Querystring key to retrieve</param>
        /// <param name="defaultValue">Default value if key don't exists or parse doesn't work</param>
        /// <returns></returns>
        public static int GetIntValueOrDefault(Dictionary<string, Microsoft.Extensions.Primitives.StringValues> dict, string key, int defaultValue)
        {
            if (dict.ContainsKey(key))
            {
                if (Int32.TryParse(dict[key].ToString(), out int result))
                {
                    return result;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Retrieve string value from querystring
        /// </summary>
        /// <param name="dict">Querystring dictionary</param>
        /// <param name="key">Querystring key to retrieve</param>
        /// <param name="defaultValue">Default value if key don't exists or parse doesn't work</param>
        /// <returns></returns>
        public static string GetStringValueOrDefault(Dictionary<string, Microsoft.Extensions.Primitives.StringValues> dict, string key, string defaultValue)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key].ToString();
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
