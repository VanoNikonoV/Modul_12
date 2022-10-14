using Microsoft.Win32;
using Modul_12.Models;
using System.IO;
using System.Text;


namespace Modul_12.Cmds
{
    internal class SaveCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => (parameter as Clients) != null;

        public override void Execute(object parameter)
        {
            if (parameter is Clients)
            {
                var saveDlg = new SaveFileDialog { Filter = "Text files|*.csv" };

                if (true == saveDlg.ShowDialog())
                {
                    string fileName = saveDlg.FileName;

                    using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
                    {
                        foreach (var emp in (Clients)parameter)
                        {
                            sw.WriteLine(emp.ToString());
                        }
                    }
                }
            }

            
        }
    }
}
