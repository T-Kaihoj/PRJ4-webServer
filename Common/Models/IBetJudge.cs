using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    interface IBetJudge
    {
        //Preconditon:
        //Et outcome er sat af Judge
        //Postcondition
        //Vindere har fået deres del af bettets Pot
        void ConcludeBet(Outcome winnerOutcome);

        //Preconditon:
        //Et outcome er sat af Judge
        //Postcondition
        //Vindere har fået deres del af bettets Pot
        

    }
}
