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
        public ObservableCollection<Client> Clients { get; set; }

        readonly ClientsRepository clientRepository;

        public Consultant Consultant { get; set; } 
        
        public Meneger Meneger { get; set; } 

        public MainWindowViewModel()
        {
            this.clientRepository = new ClientsRepository("data.csv"); //path

            Clients = new ObservableCollection<Client>(clientRepository.GetClients());

            Consultant = new Consultant();

            Meneger = new Meneger();
        }
    }
}
