using Microsoft.Win32;
using Modul_12.Cmds;
using Modul_12.Models;
using Modul_12.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Modul_12
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }

        #region Команды
        private ICommand _saveCommand = null;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new SaveCommand());

        //private ICommand _editTelefonCommand = null;
        //public ICommand EditTelefonCommand => _editTelefonCommand ?? (_editTelefonCommand = new EditTelefonCommand());

        private RelayCommand<Client> _editTelefonCommand = null;

        public RelayCommand<Client> EditTelefonCommand 
            
            => _editTelefonCommand ?? (_editTelefonCommand = new RelayCommand<Client>(EditTelefon, CanEditTelefon));

        #endregion

        public MainWindow()
        {
            ViewModel = ViewModel ?? new MainWindowViewModel();

            InitializeComponent();

            DataClients.ItemsSource = ViewModel.Consultant.ViewClientsData(ViewModel.Clients.Clone());

            #region Сокрытие не функциональных кнопок

            //EditName_Button.IsEnabled = false;
            //EditMiddleName_Button.IsEnabled = false;
            //EditSecondName_Button.IsEnabled = false;
            //EditSeriesAndPassportNumber_Button.IsEnabled = false;
            NewClient_Button.IsEnabled = false;
            #endregion
        }

        /// <summary>
        /// Метод удаляющий текст сообщения в StatusBar
        /// </summary>
        /// <param name="message">Текст информационного сообщения</param>
        private void ShowStatusBarText(string message)
        {
            StatusBarText.Text = message;

            var timer = new System.Timers.Timer();

            timer.Interval = 2000;

            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Stop();
                //удалите текст сообщения о состоянии с помощью диспетчера, поскольку таймер работает в другом потоке
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    StatusBarText.Text = "";
                }));
            };
            timer.Start();
        }

        private void CloseWindows(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Выбор функицонала консультант / менаджер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccessLevel_ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (AccessLevel_ComboBox.SelectedIndex)
            {
                case 0: //консультант

                    #region Сокрытие не функциональных кнопок
                    EditName_Button.IsEnabled = false;
                    EditMiddleName_Button.IsEnabled = false;
                    EditSecondName_Button.IsEnabled = false;
                    EditSeriesAndPassportNumber_Button.IsEnabled = false;
                    NewClient_Button.IsEnabled = false;
                    #endregion

                    DataClients.ItemsSource = ViewModel.Consultant.ViewClientsData(ViewModel.Clients.Clone());

                    break;

                case 1: //менждер

                    #region Активация функциональных кнопок   
                    EditName_Button.IsEnabled = true;
                    EditMiddleName_Button.IsEnabled = true;
                    EditSecondName_Button.IsEnabled = true;
                    EditSeriesAndPassportNumber_Button.IsEnabled = true;
                    NewClient_Button.IsEnabled = true;
                    #endregion

                    DataClients.ItemsSource = ViewModel.Meneger.ViewClientsData(ViewModel.Clients);

                    break;

                default:
                    break;

            }
        }

        #region Редактирование данных о клиенте

        /// <summary>
        /// Метод редактирования номера телефона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void EditTelefon_Button(object sender, RoutedEventArgs e)
        //{
        //    var client = DataClients.SelectedItem as Client;

        //    string whatChanges = string.Format(client.Telefon + @" на " + EditTelefon_TextBox.Text.Trim());

        //    if (client != null)
        //    {
        //        //изменения в коллекции клиентов
        //        ViewModel.Consultant.EditeTelefonClient(client, EditTelefon_TextBox.Text.Trim());

        //        if (client.Error == String.Empty)
        //        {
        //            //изменения в коллекции банка, по ID клиента
        //            Client editClient = ViewModel.Clients.First(i => i.ID == client.ID);

        //            editClient.Telefon = EditTelefon_TextBox.Text.Trim();

        //            switch (AccessLevel_ComboBox.SelectedIndex)
        //            {
        //                case 0: //консультант

        //                    editClient.InfoChanges.Add(new InformationAboutChanges(DateTime.Now, whatChanges, "замена", nameof(Consultant)));

        //                    break;

        //                case 1: //менждер

        //                    editClient.InfoChanges.Add(new InformationAboutChanges(DateTime.Now, whatChanges, "замена", nameof(Meneger)));

        //                    break;

        //                default:
        //                    break;
        //            }

        //            isDirty = true;
        //        }
        //        else { ShowStatusBarText("Исправте не корректные данные"); }
        //    }
        //    else ShowStatusBarText("Выберите клиента");
        //}

        /// <summary>
        /// Метод редактирования имени клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void EditName_Button_Clik(object sender, RoutedEventArgs e)
        //{
        //    var client = DataClients.SelectedItem as Client;

        //    if (client != null)
        //    {
        //        Client changedClient = ViewModel.Meneger.EditNameClient(client, EditName_TextBox.Text.Trim());

        //        ViewModel.Clients.EditClient(ViewModel.Clients.IndexOf(client), changedClient);

        //        isDirty = true;
        //    }

        //    else ShowStatusBarText("Выберите клиента");
        //}

        ///// <summary>
        ///// Метод редактирования отчества клиента
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void EditMiddleName_Button_Clik(object sender, RoutedEventArgs e)
        //{
        //    var client = DataClients.SelectedItem as Client;

        //    if (client != null)
        //    {
        //        Client changedClient = ViewModel.Meneger.EditMiddleNameClient(client, EditMiddleName_TextBox.Text.Trim());

        //        ViewModel.Clients.EditClient(ViewModel.Clients.IndexOf(client), changedClient);

        //        isDirty = true;
        //    }

        //    else ShowStatusBarText("Выберите клиента");
        //}

        //private void EditSecondName_Button_Clik(object sender, RoutedEventArgs e)
        //{
        //    var client = DataClients.SelectedItem as Client;

        //    if (client != null)
        //    {
        //        Client changedClient = ViewModel.Meneger.EditSecondNameClient(client, EditSecondName_TextBox.Text.Trim());

        //        ViewModel.Clients.EditClient(ViewModel.Clients.IndexOf(client), changedClient);

        //        isDirty = true;
        //    }

        //    else ShowStatusBarText("Выберите клиента");
        //}

        //private void EditSeriesAndPassportNumber_Button_Clik(object sender, RoutedEventArgs e)
        //{
        //    var client = DataClients.SelectedItem as Client;

        //    if (client != null)
        //    {
        //        Client changedClient = ViewModel.Meneger.EditSeriesAndPassportNumberClient(client, EditSeriesAndPassportNumber_TextBox.Text.Trim());

        //        ViewModel.Clients.EditClient(ViewModel.Clients.IndexOf(client), changedClient);

        //        isDirty = true;
        //    }

        //    else ShowStatusBarText("Выберите клиента");
        //}
        #endregion

        private bool CanEditTelefon(Client client)
        {
            if (client != null) { return true; } 
            
            return false;   
        }

        private void EditTelefon(Client client)
        {
            string whatChanges = string.Format(client.Telefon + @" на " + EditTelefon_TextBox.Text.Trim());

            //изменения в коллекции клиентов
            ViewModel.Consultant.EditeTelefonClient(EditTelefon_TextBox.Text.Trim(), client);

            if (client.Error == String.Empty)
            {
                //изменения в коллекции банка, по ID клиента
                Client editClient = ViewModel.Clients.First(i => i.ID == client.ID);

                editClient.Telefon = EditTelefon_TextBox.Text.Trim();

                switch (AccessLevel_ComboBox.SelectedIndex)
                {
                    case 0: //консультант

                        editClient.InfoChanges.Add(new InformationAboutChanges(DateTime.Now, whatChanges, "замена", nameof(Consultant)));

                        break;

                    case 1: //менждер

                        editClient.InfoChanges.Add(new InformationAboutChanges(DateTime.Now, whatChanges, "замена", nameof(Meneger)));

                        break;

                    default:
                        break;
                }

                SaveCommand.CanExecute(ViewModel.Clients);

            }

            else { ShowStatusBarText("Исправте не корректные данные"); }
         
        }


     

        /// <summary>
        /// Метод заполняющий панель данными выбранного клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientViewSelection(object sender, SelectionChangedEventArgs e)
        {
            Client temp = DataClients.SelectedItem as Client;

            if (temp != null)
            {
                PanelInfo.DataContext = temp;
            }
        }

        /// <summary>
        /// Метод добавления нового клиенита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewClient_Button_Click(object sender, RoutedEventArgs e)
        {
            NewClientWindow _windowNewClient = new NewClientWindow();

            _windowNewClient.Owner = this;

            _windowNewClient.ShowDialog();

            if (_windowNewClient.DialogResult == true)
            {
                ViewModel.Clients.Add(_windowNewClient.NewClient);

                SaveCommand.CanExecute(null);
            }
        }
    }
}
