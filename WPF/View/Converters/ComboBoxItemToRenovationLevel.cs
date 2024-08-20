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
    public class ComboBoxItemToRenovationLevel : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int valueInt) {
                return $"Level {valueInt}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is ComboBoxItem selectedItem)
                {
                    string content = selectedItem.Content.ToString();
                    if (int.TryParse(content.Substring(content.Length - 1), out int level))
                    {
                        return level;
                    }
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
