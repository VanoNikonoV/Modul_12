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
            Tuple<string, Client, Clients, int> tuple = 

                new Tuple<string, Client, Clients, int> 

                ((string)values[0], (Client)values[1], (Clients)values[2], (int)values[3]);

            return  tuple;
        }
         
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
