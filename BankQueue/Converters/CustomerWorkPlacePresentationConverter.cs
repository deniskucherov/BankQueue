using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Bank.Common;

namespace BankQueue.Converters
{
    public sealed class CustomerWorkPlacePresentationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var customer = value as Customer;
                if (customer == null)
                    return "-";

                var b = new StringBuilder();
                b.Append(customer.Name);
                b.Append(" (");
                b.Append(customer.Age.Value);
                b.Append(customer.Sex);
                b.Append(")");
                return b.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("CustomerWorkPlacePresentationConverter error.", ex);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
