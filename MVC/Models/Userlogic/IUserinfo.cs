using System.Collections.Generic;

namespace MVC.Models.Userlogic
{

    public interface IUserinfo
    {
        //PREcondition:
        //Modtager string der er SQL-safe og ét af de to ens validerede passwords.
        //Postcondition:
        //True, hvis en user er blevet tilføjet i databasen. False, hvis Username er optaget.
        bool createUser(string firstName, string lastName, string email, string username, string password);



        // INFO ___________________________________________
        //Preconditon:
        //Modtager et username
        //Postcondition
        //Returner hash og salt af user, hvis user eksisterer ellers null.
        string getHash(string username, out string salt);

        //Precondition:
        //Modtager dele eller hele usernames
        //Postcondition:
        //Returner liste af mulige users
        List<string> autoSuggestUser(string username);

        //Precondition:
        //Modtager et username
        //Postcondition
        //Returner User som ikke er forbundet til database, hvis username eksisterer, ellers null.
        User getUser(string username);


    }
}
