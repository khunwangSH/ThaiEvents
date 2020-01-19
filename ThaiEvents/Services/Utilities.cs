using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaiEvents.Services
{
    public static class Utilities
    {
        public static DateTime ConvertStringToDateTime(string stringDate)
        {
            var result = DateTime.MinValue;
            //DateTime.TryParseExact(stringDate,"MM/dd/yyyy HH:mm", out result);
            if (!String.IsNullOrEmpty(stringDate))
                result = DateTime.ParseExact(stringDate, "dd/MM/yyyy HH:mm", null);
            return result;
        }
    }
}
