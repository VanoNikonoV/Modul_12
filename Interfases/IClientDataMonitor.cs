using Modul_12.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul_12.Interfases
{
    public interface IClientDataMonitor
    {
        ObservableCollection<Client> ViewClientsData(ObservableCollection<Client>clients);

        Client EditeClient(Client client, string newData);
    }
}
