using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace MVC.Models.Userlogic
{
    interface IBetinfo
    {
        // INFO ___________________________________________
        //Preconditon:
        //Modtager et BetId
        //Postcondition
        //Returner et Bet navn.
        string getBetName(long betID);

        //Preconditon:
        //Modtager et BetId
        //Postcondition
        //Returner Bet deskribtion.
        string getDescribtion(long betID);

        //Precondition:
        //Modtager et BetId
        //Postcondition:
        //Returner Bets Judge
        User getJudge(long betID);

        //Precondition:
        //Modtager et BetId
        //Postcondition:
        //Returner Bettets startdato
        string getStartDate(long betID);

        //Precondition:
        //Modtager et BetId
        //Postcondition:
        //Returner Bettets startdato
        string getEndDate(long betID);




    }
}
