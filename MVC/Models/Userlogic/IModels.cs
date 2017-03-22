using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVC.Models
{
    public interface IModels
    {
        //Gemmer ændringerne
        void Persist();

        //Sletter current user
        void Delete();


    }
}