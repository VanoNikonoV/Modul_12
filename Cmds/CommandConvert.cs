using Modul_12.Models;
using System;
using System.Globalization;
using System.Windows.Data;


namespace Modul_12.Cmds
{
    internal class CommandConver : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Tuple<string, Client, int> tuple = new Tuple<string, Client, int> ((string)values[0], (Client)values[1], (int)values[2]);

            return  tuple;
        }
         
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
