using System.IO;

namespace Common.Models
{
    public class Utility : IUtility
    {
        private static Utility _instance;

        public static Utility Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Utility();
                }

                return _instance;
            }
        }

        public string DatabaseSecure(string toDB)
        {
            if (toDB == null)
                return null;
           
            if (toDB.Contains("'") || toDB.Contains("[") || toDB.Contains("]"))
            {
                throw new InvalidDataException("sqlInjection");
            }

            return toDB;
        }
    }
}
