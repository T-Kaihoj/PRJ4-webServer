using Common;
using DAL.Data;

namespace DAL
{
    public class Factory : IFactory
    {
        private Factory _instance;

        public Factory Instance
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
