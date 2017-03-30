using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;

namespace MVC.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public abstract class ValidationHelper
    {
        protected ValidationContext Context;
        protected List<ValidationResult> Results;

        [SetUp]
        protected void ValidationHelperSetup()
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
