using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Validation
{
    public class AccommodationNameValidation : ValidationRule
    {
        private const string pattern = @"^[A-Z].*$";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                Regex regex = new Regex(pattern);
                if (s == "") return new ValidationResult(false, "Name cannot be empty.");
                if (!regex.IsMatch(s)) return new ValidationResult(false, "Name must start with capital letter.");
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
