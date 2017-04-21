using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace MVC.Models.Userlogic
{
    interface IBetJudge
    {

        //Preconditon:
        //Modtager et BetId
        //Postcondition
        //Sætter det Bet som skal konkluderes.
        void setCurrentBet(long betID);

        //Preconditon:
        //Modtager et ÚserID
        //Postcondition
        //Sætter en User som vinder.
        void chooseWinner(long userID);


        //Preconditon:
        //Modtager en pot og en liste af users(vindere)
        //Postcondition
        //Tilføjer præmiedel til users balance.
        void payout(decimal pot, List<User> winners);
    }
}
