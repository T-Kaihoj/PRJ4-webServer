using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontendMVC.Business.UserLogic
{
    
    public interface IUserinfo
    {
        //PREcondition:
        //Modtager string der er SQL-safe og ét af de to ens validerede passwords.
        //Postcondition:
        //True, hvis en user er blevet tilføjet i databasen. False, hvis Username er optaget.
        bool createUser(string firstName, string lastName, string email, string username, string password );

        


        // INFO ___________________________________________
        //Preconditon:
        //Modtager et username
        //Postcondition
        //Returner hash af user og salt, hvis user eksisterer ellers null.
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


        // USER EDIT________________________________________
        //Ændre users 
        //Postcondition:
        //Retunerer true, hvis update af first name lykkedes, eller false.
        bool updateFirstName(string currentUsername, string firstName);
        //Postcondition:
        //Retunerer true, hvis update af last name lykkedes, eller false.
        bool updateLastName(string currentUsername, string LastName);
        //Postcondition:
        //Retunerer true, hvis update af email lykkedes, eller false.
        bool updateEmail(string currentUsername, string email);
        //Postcondition:
        //Retunerer true, hvis update af password lykkedes, eller false.
        bool updatePassword(string currentUsername, string password);
        //Postcondition:
        //Retunerer true, hvis delete af user lykkedes, eller false.
        bool deleteUSer(string currentUsername);


    }
}
