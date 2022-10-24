
using Modul_12.Models;
using Modul_12.ViewModels;
using System;

namespace Modul_12.Cmds
{
    internal class EditTelefonCommand : CommandBase
    {
        public MainWindowViewModel ViewModel { get; set; } = new MainWindowViewModel();

        public override bool CanExecute(object parameter)
        {
            return true;
            //Tuple<string, Client> tuple = parameter as Tuple<string, Client>;

            //string newNumber = tuple.Item1;

            //Client _client = tuple.Item2;

            //if (newNumber.Length == 0)
            //{
            //    return false;
            //}

            //else return true;
        }

        public override void Execute(object parameter)
        {
            Tuple<string, Client, Clients, int> tuple = parameter as Tuple<string, Client, Clients, int>;

            string newNumber = tuple.Item1;

            Client client = tuple.Item2;

            Clients clients = tuple.Item3;

            bool flag = !String.IsNullOrWhiteSpace(newNumber);

            Clients Clients = ViewModel.Clients;

            Consultant consultant = ViewModel.Consultant;

            if (newNumber.Length == 11 && flag)
            {
                int x = clients.IndexOf(client);

                Clients.EditClient(x, consultant.EditeTelefonClient(client, newNumber));
            }
            //else ShowStatusBarText("Номер долже содержать 11 символов");
        }
    }
}
