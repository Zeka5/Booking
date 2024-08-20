using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Validation
{
    public class DriverDelayValidation : ValidationRule
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is int)
            {
                int i = (int)value;
                if (i < Min) return new ValidationResult(false, $"Minimum minutes: {Min}");
                else if (i > Max) return new ValidationResult(false, $"If you are late more than {Max} minutes,\nplease contanct the customer.");
                return new ValidationResult(true, null);
            }
            else return new ValidationResult(false, "Enter number of minutes.");
        }
    }
}
