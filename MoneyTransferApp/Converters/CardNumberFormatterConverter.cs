using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransferApp.Converters
{
    public class CardNumberFormatterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string cardNumber)
            {
                // Format the card number to show only the last four digits
                if (cardNumber.Length == 16)
                {
                    return $"**** **** **** {cardNumber.Substring(12)}";
                }
            }

            return value;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
