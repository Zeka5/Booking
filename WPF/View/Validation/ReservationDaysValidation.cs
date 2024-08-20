using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BookingApp.WPF.View.Validation
{
    public class ReservationDaysValidation : ValidationRule
    {
        public int Min { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is int)
            {
                int n = (int)value;
                if (n < Min) return new ValidationResult(false, "Value must be higher than " + Min + ".");
                else return new ValidationResult(true, null);
            }
            else return new ValidationResult(false, "Enter an integer.");
        }
    }
}
