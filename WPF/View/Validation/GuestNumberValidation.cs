using BookingApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.View.Validation
{
    public class GuestNumberValidation : ValidationRule
    {
        public double Max { get; set; }
        public double Min { get; set; } 
        public bool Error { get; set; } 
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = (string)value;
                double result;
                if (double.TryParse(s, out result))
                {
                    if (result > Max)
                    {
                        Error = true;
                        return new ValidationResult(false, "Maximum is " + Max);
                    }
                    else if (result < Min) { 
                        Error = true;
                        return new ValidationResult(false, "Minimum is " + Min);
                    }
                    Error = false;
                    return new ValidationResult(true, null);
                }
                Error = true;
                return new ValidationResult(false, "Please enter a valid number value.");
            }
            catch
            {
                return new ValidationResult(false, "Unkown error occured");
            }

        }

    }
}
