using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stimulsoft.Report.StiRecentConnections;

namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class ListExtensions
    {
        public static string JoinWithComma(this IList<string> value)
        {
            if (value==null || value.Count==0)
            {
                return null;
            }
            else
            {
                string result = string.Join(",", value.ToList());
                return result;
            }


        }

   

    }
}
