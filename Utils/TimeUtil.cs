using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Utils
{
    public class TimeUtil
    {
        public static string convertTimestampToyyyyMMdd(long? timestamp)
        {
            if (timestamp == null)
            {
                return "";
            }
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dt = startTime.AddMilliseconds(Convert.ToDouble(timestamp));
            //System.Console.WriteLine(dt.ToString("yyyy/MM/dd HH:mm:ss:ffff"));
            return dt.ToString("yyyy/MM/dd");
        }
    }
}
