using System;
using Common;
using Microsoft.AspNet.Identity;

namespace MVC.Identity
{
    public partial class Store : IStore
    {
        private readonly IFactory _factory;

        public Store(IFactory factory)
        {
            _factory = factory;
        }

        public void Dispose()
        {
            // We do not care about dispose, as all work is done in using clauses with the unit of work.
        }
    }
}