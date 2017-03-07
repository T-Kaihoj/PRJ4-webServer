using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Core.Model;

namespace Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var unitOfWork = new UnitOfWork(new Context(new DbContextOptions<Context>())))
            {
                User user = new User();

                user.Username = "Thomas123";
                user.FirstName = "Thomas";
                user.LastName = "Nielsen";

                unitOfWork.User.Add(user);

                unitOfWork.Complete();
            }
        }
    }
}
