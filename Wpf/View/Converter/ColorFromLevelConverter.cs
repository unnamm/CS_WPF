using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace View.Converter
{
    internal class ColorFromLevelConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LogLevel level)
            {
                var color = level switch
                {
                    LogLevel.Warning => Brushes.Orange,
                    LogLevel.Error => Brushes.Red,
                    LogLevel.Critical => Brushes.OrangeRed,
                    _ => Brushes.Black
                };
                return color;
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
