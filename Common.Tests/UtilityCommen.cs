using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
    public static class UtilityCommen
    {
        public static string[] validCharacters =
        {
            "a", "z", "æ", "ø", "å",
            "A", "Z", "Æ", "Ø", "Å",
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

        public static string[] invalidCharacters =
        {
            @"'",
            @"[",
            @"]"
        };


    }
}
