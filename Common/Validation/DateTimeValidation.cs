using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Common.Validation
{
    public class DateTimeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var v = value as string;

            if (v == null)
            {
                return new ValidationResult("Value cannot be null");
            }

            DateTime dt;

            // Attempt parsing using normal locale.
            if (!TryParse(v, out dt))
            { 
                return new ValidationResult("Not a valid date");
            }

            return ValidationResult.Success;
        }

        public static DateTime Parse(string input)
        {
            DateTime dt;

            TryParse(input, out dt);

            return dt;
        }

        public static bool TryParse(string input, out DateTime output)
        {
            if (!DateTime.TryParse(input, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.NoCurrentDateDefault, out output))
            {
                // Attempt parsing using specific locale.
                if (!DateTime.TryParse(
                    input,
                    CultureInfo.CreateSpecificCulture("da-DK"),
                    DateTimeStyles.NoCurrentDateDefault,
                    out output))
                {
                    return false;
                }
            }

            if (output.Year == 1 && output.Month == 1 && output.Day == 1)
            {
                return false;
            }

            return true;
        }
    }
}
