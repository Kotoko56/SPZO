using SPZO.Model;
using System.Globalization;
using System.Windows.Data;

namespace SPZO.Commands
{
    public class Converters : IValueConverter
    {
        //This class inherits IValueConverter, that allows me to combine two values (strings in this case), to display directly in XAML
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Client client && value != null)
            {
                return $"{client.Name} {client.Surname}";
            }

            return string.Empty;
        }

        //Revert is not implementen. It's not needed.
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
