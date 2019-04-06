using System;
using System.Globalization;

namespace ChatWpf.ValueConverter
{
    public class BooleanToBorderThicknessConverter : BaseValueConverter<BooleanToBorderThicknessConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? 2 : 0;
            else
                return (bool)value ? 0 : 2;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
