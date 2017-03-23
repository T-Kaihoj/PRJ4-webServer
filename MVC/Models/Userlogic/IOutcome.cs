using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models.Userlogic
{
    interface IOutcome
    {
        //Preconditon:
        //Outcome har en description
        //Postcondition
        //Returner en string med Outcomets description.
        string getOutcomeDescription();

        //Preconditon:
        //Modtager 
        //Postcondition
        //Returner Bet deskribtion.
        string getDescribtion(long betID);
    }
}
