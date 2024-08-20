using BookingApp.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.View.Validation
{
    public class DaysToStayValidation : ValidationRule
    {
        public double Min { get; set; } 

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = (string)value;
                double result;
                if (double.TryParse(s, out result)) {
                    if (result < Min) {
                        return new ValidationResult(false, "Minimum is " + Min + " days");
                    }
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false,"Please enter a valid number value.");
            }
            catch
            {
                return new ValidationResult(false,"Unkown error occured");
            }

        }
    }
}
