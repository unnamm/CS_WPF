using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace View.Converter
{
    /// <summary>
    /// bool -> Brush (연결 상태 표시용)
    /// </summary>
    internal class BoolToBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is true ? Brushes.LimeGreen : Brushes.Gray;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
