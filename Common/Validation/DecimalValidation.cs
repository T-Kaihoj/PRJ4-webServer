using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Common.Validation
{
    public class DecimalValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var v = value as string;

            if (v == null)
            {
                return new ValidationResult("Value cannot be null");
            }

            decimal d;

            // Attempt parsing using normal locale.
            if (!TryParse(v, out d))
            { 
                return new ValidationResult("Not a valid date");
            }

            return ValidationResult.Success;
        }

        public static decimal Parse(string input)
        {
            decimal d;

            TryParse(input, out d);

            return d;
        }

        public static bool TryParse(string input, out decimal output)
        {
            var ci = new CultureInfo("en-US");

            if (!decimal.TryParse(input, NumberStyles.Float, ci.NumberFormat, out output))
            {
                return false;
            }

            return true;
        }
    }
}
