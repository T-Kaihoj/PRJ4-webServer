using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using DAL;
using DAL.Data;

namespace TestApp
{
    
    class Program
    {
        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            var context = new Context();
            using (var unitOfWork = new UnitOfWork(context))
            {
                var enLobby = unitOfWork.Lobby.Get(1);
                unitOfWork.Lobby.Remove(enLobby);

                unitOfWork.Complete();
            }
        }
    }
}
