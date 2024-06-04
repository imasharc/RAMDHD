using System.Globalization;

namespace RAMDHD.Converters
{
    public class ActivePageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int activePage && parameter is string parameterString && int.TryParse(parameterString, out int currentPage))
            {
                return activePage == currentPage ? Colors.White : Colors.WhiteSmoke;
            }
            return Colors.WhiteSmoke; // Default color if something goes wrong
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
