using Modul_12.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul_12.ViewModels
{
    public class MainWindowViewModel
    {
        public ClientsRepository ClientsRepository { get; set; }

        public Consultant Consultant { get; set; }

        public Meneger Meneger { get; set; }     

        public MainWindowViewModel()
        {
            ClientsRepository = new ClientsRepository("data.csv"); 

            Consultant = new Consultant();

            Meneger = new Meneger();
        }

       


    }
}
