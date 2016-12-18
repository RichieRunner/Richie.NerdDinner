using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Richie.NerdDinner.Extensions
{
    public static class DBNullExtensions
    {
        /// <summary>
        /// Attemps to retrieve value from nullable type, if the type 
        /// does not contain a value then DBNull is returned.
        /// 
        /// <typeparam name="T">type that is being passed to extension method"/></typeparam>
        /// <param name="value">item to retrieve value from</param>
        /// <returns> returns either value of item or dbnull value</returns>
        /// </summary>
        /// 
        public static object ToValueOrDBNull<T>(this Nullable<T> value) where T : struct
        {
            if (value.HasValue)
            {
                return value.Value;
            }

            return DBNull.Value;

        }

        public static object ToValueOrDBNull(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return DBNull.Value;
        }
    }
}