using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UI.Converts
{
    class RadioButtonConverter : IValueConverter
    {
        private readonly string _optionValue;

        public RadioButtonConverter(string optionValue)
        {
            _optionValue = optionValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString() == _optionValue;

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (bool)value ? _optionValue : null;
    }
}
