using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Infrastructure.Exts
{
    public static class StringExt
    {
        public static string Join(this IList<string> list, string separator)
        {
            var result = string.Join(separator, list.Select(x => x.Trim()));
            return result;
        }
    }
}
