using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Utility
    {
        public static string DatabaseSecure(string toDB)
        {
            if (toDB == null)
                return null;
            //todo db
            if (toDB.Contains("'")||toDB.Contains("[")||toDB.Contains("]"))
            {
                throw new InvalidDataException("sqlInjection");
            }
            return toDB;
        }
    }
}
