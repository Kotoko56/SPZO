using SPZO.Model;
using System.Globalization;
using System.Windows.Data;

namespace SPZO.Commands
{
    public class Converters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Client client && value != null)
            {
                return $"{client.Name} {client.Surname}";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
