using System;

namespace Lorenzo.WorkWatcher.Core.Common
{
    public static class MethodExtensions
    {
        //using extension method:
        public static DateTime Trim(this DateTime date, long roundTicks)
        {
            return new DateTime(date.Ticks - date.Ticks % roundTicks);
        }
    }
}
