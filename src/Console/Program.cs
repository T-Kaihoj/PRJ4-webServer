using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Core.Model;
using Models.Core.Repositories;
using Models.Persistence.Repositories;

namespace Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            using (var unitOfWork = new UnitOfWork(new Context(new DbContextOptions<Context>())))
            {
                User user = new User();
                //user.Username = "Thomas12";
                user.Balance = 12;
                user.Email = "email@MyEmail.com";
                user.FirstName = "Thomas";
                user.LastName = "Hansen";
                
                //Context db = new Context();
                //db.Users.Add(user);

                unitOfWork.User.Add(user);

                //db.SaveChanges();
                unitOfWork.Complete();
            }
            
        }
    }
}
