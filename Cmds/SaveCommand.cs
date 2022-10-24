using Microsoft.Win32;
using Modul_12.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Controls;


namespace Modul_12.Cmds
{
    internal class SaveCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true;//(parameter as Clients) != null;

        public override void Execute(object parameter)
        {
            var saveData = parameter as Clients;

          

            //if (saveData.Count != 0)
            //{
            //    var saveDlg = new SaveFileDialog { Filter = "Text files|*.csv" };

            //    if (true == saveDlg.ShowDialog())
            //    {
            //        string fileName = saveDlg.FileName;

            //        using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
            //        {
            //            foreach (var emp in (Clients)parameter)
            //            {
            //                sw.WriteLine(emp.ToString());
            //            }
            //        }
            //    }
            //}

            //foreach (var client in ClientsBank)
            //{
            //    client.IsChanged = false;
            //}
            //// нужно как то обновить данные для консультанта
            //isDirty = false;


        }


    }
}
