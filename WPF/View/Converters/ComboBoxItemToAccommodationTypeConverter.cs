using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace BookingApp.WPF.View.Converters
{
    public class ComboBoxItemToAccommodationTypeConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var t = (Domain.Model.Type)value;
                return t.ToString();
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var item = (ComboBoxItem)value;
                return Accommodation.GetType(item.Content.ToString());
            }
            catch
            {
                return null;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
