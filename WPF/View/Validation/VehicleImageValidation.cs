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
    public class VehicleImageValidation : ValidationRule
    {
        private const string pattern = @"^../../../Resources/Images/.*$";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(s)) return new ValidationResult(false, "../../../Resources/Images/(name.jpg) must be the path");
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
