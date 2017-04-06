using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    interface IBetLogic
    {

        //Preconditon:
        //Et outcome er sat af Judge
        //Postcondition
        //Vindere har fået deres del af bettets Pot
        bool ConcludeBet( User user, Outcome outcome);

        //Preconditon:
        //Et outcome er sat af Judge
        //Postcondition
        //Vindere har fået deres del af bettets Pot
        


    }
}
