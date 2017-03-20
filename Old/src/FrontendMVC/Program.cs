using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FrontendMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
            
            /*
            using (var unitOfWork = new UnitOfWork(new Context()))
            {
                User user = new User();

                user.Username = "Thomas123";
                user.FirstName = "Thomas";
                user.LastName = "Nielsen";

                unitOfWork.User.Add(user);

                unitOfWork.Complete();
            }
            */
        }
        
    }
}
