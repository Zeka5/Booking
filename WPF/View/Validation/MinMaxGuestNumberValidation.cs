using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Validation
{
    public class MinMaxGuestNumberValidation : ValidationRule
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is int)
            {
                int i = (int)value;
                if (i < Min) return new ValidationResult(false, $"Min guests: {Min}.");
                else if (i > Max) return new ValidationResult(false, $"Max guests {Max}.");
                return new ValidationResult(true, null);
            }
            else return new ValidationResult(false, "Enter an integer.");
        }
    }
}
