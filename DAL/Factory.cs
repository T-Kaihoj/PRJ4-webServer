using Common;
using DAL.Data;

namespace DAL
{
    public class Factory : IFactory
    {
        private static Factory _instance;

        public static Factory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Factory();




                }

                return _instance;
            }
        }

        public IUnitOfWork GetUOF()
        {
            return new UnitOfWork(new Context());
        }
    }
}
