using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Validation
{
    public class TourDuraionValidation : ValidationRule
    {
        public double Min { get; set; }
        public double Max { get; set; } 
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = (string)value;
                double result;
                if (double.TryParse(s, out result))
                {
                    if (result < Min)
                    {
                        return new ValidationResult(false, "Minimum hours is " + Min);
                    }
                    else if(result > Max) 
                    {
                        return new ValidationResult(false, "Maximum hours is " + Max);
                    }
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter number");
            }
            catch
            {
                return new ValidationResult(false, "Unkown error occured");
            }

        }
    }
}
