using Microsoft.Win32;
using Modul_12.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;


namespace Modul_12.Cmds
{
    internal class SaveCommand : CommandBase
    {
        public override bool CanExecute(object parameter) //saveData.CollectionChanged - для CanExecute
        {
            if ((parameter as Clients) != null)
            {
                foreach (var c in parameter as Clients)
                {
                    if (c.IsChanged == true) { return true; }
                }
            }

            return false;
        }
       

        public override void Execute(object parameter)
        {
            ObservableCollection<Client> saveData = parameter as ObservableCollection<Client>;

            if (saveData.Count != 0)
            {
                var saveDlg = new SaveFileDialog { Filter = "Text files|*.csv" };

                if (true == saveDlg.ShowDialog())
                {
                    string fileName = saveDlg.FileName;

                    using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
                    {
                        foreach (var emp in saveData)
                        {
                            sw.WriteLine(emp.ToString());
                        }
                    }
                }
            }

            foreach (Client client in saveData)
            {
                client.IsChanged = false;
            }
        }

    }
}
