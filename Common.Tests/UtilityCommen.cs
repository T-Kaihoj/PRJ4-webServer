using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Tests
{
    [ExcludeFromCodeCoverage]
    public static class UtilityCommen
    {
        public static User[] ValidUsers = {new User(), null};

        public static Bet[] ValidBets = {new Bet(), null};
        
        public static string[] ValidCharacters =
        {
            "a", "z", "æ", "ø", "å",
            "A", "Z", "Æ", "Ø", "Å",
            @"",
            @" ",
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
            @"§",
            null
        };

        public static string[] InvalidCharacters =
        {
            @"'", @"[", @"]"
        };

        public static long[] ValidIds =
        {
            -1, 0, 1, long.MaxValue, long.MinValue
        };
    }
}
