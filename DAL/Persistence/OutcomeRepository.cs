using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Persistence
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