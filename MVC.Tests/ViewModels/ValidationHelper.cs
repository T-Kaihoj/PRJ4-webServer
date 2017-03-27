using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MVC.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
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
