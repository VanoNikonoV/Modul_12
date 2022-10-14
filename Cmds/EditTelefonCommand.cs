
using System;

namespace Modul_12.Cmds
{
    internal class EditTelefonCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true; //(parameter as string) != null; 

        public override void Execute(object parameter)
        {
            var values = parameter; //(Tuple<string, Client>)

            //string newNumber = values.Item1;

            //Client _client = values.Item2;

            //bool flag = !String.IsNullOrWhiteSpace(newNumber);

            //if (newNumber.Length == 11 && flag)
            //{
            //    _clients.EditClient(_clients.IndexOf(client), _consultant.ChangeTelefonClient(client, newNumber));
            //}
            //else ShowStatusBarText("Номер долже содержать 11 символов");
        }
    }
}
