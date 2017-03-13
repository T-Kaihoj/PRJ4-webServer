using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC.Tests.ViewModels
{
    public abstract class ValidationHelper
    {
        protected ValidationContext Context;
        protected List<ValidationResult> Results;

        protected void Setup()
        {
            Results = new List<ValidationResult>();
        }

        protected IEnumerable<string> GetErrors
        {
            get
            {
                return Results.Select(r => r.ErrorMessage);
            }
        }
    }
}
