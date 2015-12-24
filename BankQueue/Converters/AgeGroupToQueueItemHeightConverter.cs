using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Bank.Common.Value;

namespace BankQueue.Converters
{
    internal sealed class AgeGroupToQueueItemHeightConverter : IValueConverter
    {
        private static int DefaultHeight = 20;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ageGroup = (AgeGroup) value;
            return DefaultHeight*ageGroup.AgeFactor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
