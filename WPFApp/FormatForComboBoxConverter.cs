using System;
using System.Globalization;
using System.Windows.Data;
using QuickType;

namespace WPFApp
{
    public class FormatForComboBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TeamResults team)
                return team.FormatForComboBox();
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 