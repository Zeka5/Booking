using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using Xceed.Wpf.Toolkit.Primitives;

namespace BookingApp.WPF.View.Converters
{
    public class DateOnlyToDateTimeConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var date = (DateTime)value;
                return DateOnly.FromDateTime(date);
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var date = (DateOnly)value;
                return date.ToDateTime(TimeOnly.MinValue);
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
