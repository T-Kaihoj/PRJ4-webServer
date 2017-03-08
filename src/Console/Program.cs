using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Models.Core.Domain;

namespace Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new Context()))
            {
                Bet bet = new Bet();

                bet.BuyIn = 50;
                bet.Description = "The best one";
                bet.Name = "LocalBetMasters";

                unitOfWork.Bet.Add(bet);

                unitOfWork.Complete();
            }
        }
    }
}
