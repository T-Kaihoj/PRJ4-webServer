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
        //Modtager et ÚserID
        //Postcondition
        //Sætter en User som vinder.
        void chooseWinner(long userID);

    }
}
