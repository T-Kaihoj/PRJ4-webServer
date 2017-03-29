using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
    public static class UtilityCommen
    {
        public static string[] validNames =
        {
            "asdhjjh",
            "ØÆÅ",
            @"<",
            @">",
            @"\",
            @",",
            @";",
            @".",
            @":",
            @"-",
            @"_",
            @"*",
            @"¨",
            @"^",
            @"~",
            @"´",
            @"`",
            @"!",
            @"@",
            @"#",
            @"£",
            @"¤",
            @"$",
            @"%",
            @"&",
            @"/",
            @"{",
            @"(",
            @")",
            @"}",
            @"+",
            @"?",
            @"½",
            @"§"
        };

        public static string[] invalidNames =
        {
            @"'",
            @"[",
            @"]"
        };
    }
}
