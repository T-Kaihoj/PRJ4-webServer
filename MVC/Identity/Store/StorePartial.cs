using System.Diagnostics.CodeAnalysis;
using Common;

namespace MVC.Identity
{
    public partial class Store : IStore
    {
        private readonly IFactory _factory;

        public Store(IFactory factory)
        {
            _factory = factory;
        }

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            // We do not care about dispose, as all work is done in using clauses with the unit of work.
        }
    }
}