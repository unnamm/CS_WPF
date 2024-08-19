using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UI.ConvertManage
{
    internal class BoolToColorConvert : IValueConverter
    {
        //viewmodel -> ui
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool)
                throw new Exception();

            bool data = (bool)value;

            if (data == true)
            {
                return Brushes.Green;
            }
            else
            {
                return Brushes.Red;
            }
        }

        //ui -> viewmodel
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
