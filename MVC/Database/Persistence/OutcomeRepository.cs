using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVC.Database.Data;
using MVC.Database.Models;
using MVC.Database.Repositories;

namespace MVC.Database.Persistence
{
    public class OutcomeRepository : Repository<Outcome>, IOutcomeRepository
    {
        public OutcomeRepository(DbContext context) : base(context)
        {
        }

        public Context OurContext
        {
            get { return _context as Context; }
        }
    }
}