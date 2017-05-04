using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Common.Models;
using Common.Repositories;
using DAL.Data;

namespace DAL.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User GetByEmail(string email)
        {
            List<User> list = OurContext.Users.Where(u => u.Email == email).ToList();

            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public User Get(string username)
        {
            return _context.Set<User>().Find(username);
        }

        public User GetEager(string username)
        {
            return OurContext.Users
                .Where(b => b.Username == username)
                .Include(b => b.Outcomes.Select(s => s.bet))
                .Include(b => b.Bets.Select(s => s.Outcomes))
                .Include(b => b.BetsJudged)
                .Include(b => b.BetsOwned)
                .Include(b => b.Friendlist)
                .Include(b => b.InvitedToLobbies)
                    .SingleOrDefault();
        }

        [ExcludeFromCodeCoverage]
        public Context OurContext
        {
            get { return _context as Context; }
        }
    }
}
