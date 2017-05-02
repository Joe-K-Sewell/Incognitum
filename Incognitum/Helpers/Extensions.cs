using System;
using System.Collections.Generic;
using System.Text;

namespace Incognitum.Helpers
{
    internal static class Extensions
    {
        internal static void AddStringIfHasValue<T>(this IDictionary<String, String> dict, String key, T? value) where T : struct
        {
            if (value.HasValue)
            {
                dict.Add(key, value.Value.ToString());
            }
        }

        internal static void AddStringIfNotNull<T>(this IDictionary<String, String> dict, String key, T value) where T : class
        {
            if (value != null)
            {
                dict.Add(key, value.ToString());
            }
        }
    }
}
