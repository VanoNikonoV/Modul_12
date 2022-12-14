using Microsoft.Win32;
using Modul_12.Cmds;
using Modul_12.Models;
using Modul_12.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Modul_12
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }

        ICollectionView collectionView { get; set; }

        private bool isDirty = false;

        #region Команды

        private RelayCommand<Client> _editTelefonCommand = null;
        public RelayCommand<Client> EditTelefonCommand
            => _editTelefonCommand ?? (_editTelefonCommand = new RelayCommand<Client>(EditTelefon, CanEditTelefon));


        private RelayCommand<Client> editNameCommand = null;
        public RelayCommand<Client> EditNameCommand =>
            editNameCommand ?? (editNameCommand = new RelayCommand<Client>(EditName, CanEdit));


        private RelayCommand<Client> editMiddleNameCommand = null;
        public RelayCommand<Client> EditMiddleNameCommand =>
            editMiddleNameCommand ?? (editMiddleNameCommand = new RelayCommand<Client>(EditMiddleName, CanEdit));


        private RelayCommand<Client> editSecondNameCommand = null;
        public RelayCommand<Client> EditSecondNameCommand =>
            editSecondNameCommand ?? (editSecondNameCommand = new RelayCommand<Client>(EditSecondName, CanEdit));


        private RelayCommand<Client> editSeriesAndPassportNumberCommand = null;
        public RelayCommand<Client> EditSeriesAndPassportNumberCommand =>
            editSeriesAndPassportNumberCommand ?? (editSeriesAndPassportNumberCommand
            = new RelayCommand<Client>(EditSeriesAndPassportNumber, CanEdit));

        private RelayCommand newClientAddCommand = null;
        public RelayCommand NewClientAddCommand => newClientAddCommand ?? (newClientAddCommand = new RelayCommand(NewClient, CanAddClient));


        private RelayCommand<Client> deleteClientCommand = null;
        public RelayCommand<Client> DeleteClientCommand => deleteClientCommand ?? (deleteClientCommand = new RelayCommand<Client>(DeleteClient, CanEdit));

        #endregion

        public MainWindow()
        {
            ViewModel = ViewModel ?? new MainWindowViewModel();

            InitializeComponent();

            collectionView = CollectionViewSource.GetDefaultView(ViewModel.ClientsRepository);
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

                    DataClients.ItemsSource = ViewModel.Consultant.ViewClientsData(ViewModel.ClientsRepository);//.Clone()

                    break;

                case 1: //менждер

                    collectionView.SortDescriptions.Clear();

                    DataClients.ItemsSource = ViewModel.Meneger.ViewClientsData(ViewModel.ClientsRepository);

                    break;

                default:
                    break;

            }
        }

        #region Редактирование данных о клиенте
        private bool CanEditTelefon(Client client)
        {
            if (client != null) { return true; }

            return false;
        }
        /// <summary>
        /// Метод редактирования номера телефона
        /// </summary>
        /// <param name="client"></param>
        private void EditTelefon(Client client)
        {
            string whatChanges = string.Format(client.Telefon + @" на " + EditTelefon_TextBox.Text.Trim());

            //изменения в коллекции клиентов
            ViewModel.Consultant.EditeTelefonClient(EditTelefon_TextBox.Text.Trim(), client);

            if (client.Error == String.Empty)
            {
                //изменения в коллекции банка, по ID клиента
                Client editClient = ViewModel.ClientsRepository.First(i => i.ID == client.ID);

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
                isDirty = true;

            }
            else { ShowStatusBarText("Исправте не корректные данные"); }

        }

        private bool CanEdit(Client client)
        {
            if (client != null && AccessLevel_ComboBox.SelectedIndex == 1) { return true; }

            return false;
        }

        private bool CanAddClient()
        {
            if (AccessLevel_ComboBox.SelectedIndex == 1) { return true; }

            return false;
        }

        /// <summary>
        /// Метод редактирования имени клиента
        /// </summary>
        /// <param name="client"></param>
        private void EditName(Client client)
        {
            if (client != null)
            {
                Client changedClient = ViewModel.Meneger.EditNameClient(client, EditName_TextBox.Text.Trim());

                int index = ViewModel.ClientsRepository.IndexOf(client);

                ViewModel.ClientsRepository.ReplaceClient(index, changedClient);

                isDirty = true;
            }
            else ShowStatusBarText("Выберите клиента");
        }

        /// <summary>
        /// Метод редактирования отчества клиента
        /// </summary>
        /// <param name="client"></param>
        private void EditMiddleName(Client client)
        {
            if (client != null)
            {
                Client changedClient = ViewModel.Meneger.EditMiddleNameClient(client, EditMiddleName_TextBox.Text.Trim());

                int index = ViewModel.ClientsRepository.IndexOf(client);

                ViewModel.ClientsRepository.ReplaceClient(index, changedClient);

                isDirty = true;
            }
            else ShowStatusBarText("Выберите клиента");
        }

        private void EditSecondName(Client client)
        {
            if (client != null)
            {
                Client changedClient = ViewModel.Meneger.EditSecondNameClient(client, EditSecondName_TextBox.Text.Trim());

                int index = ViewModel.ClientsRepository.IndexOf(client);

                ViewModel.ClientsRepository.ReplaceClient(index, changedClient);

                isDirty = true;
            }
            else ShowStatusBarText("Выберите клиента");
        }

        private void EditSeriesAndPassportNumber(Client client)
        {
            if (client != null)
            {
                Client changedClient = ViewModel.Meneger.EditSeriesAndPassportNumberClient(client, EditSeriesAndPassportNumber_TextBox.Text.Trim());

                int index = ViewModel.ClientsRepository.IndexOf(client);

                ViewModel.ClientsRepository.ReplaceClient(index, changedClient);

                isDirty = true;
            }
            else ShowStatusBarText("Выберите клиента");
        }
        #endregion

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
        private void NewClient( )
        {
            NewClientWindow _windowNewClient = new NewClientWindow();

            _windowNewClient.Owner = this;

            _windowNewClient.ShowDialog();

            if (_windowNewClient.DialogResult == true)
            {
                ViewModel.ClientsRepository.Add(_windowNewClient.NewClient);

                isDirty = true;
            }
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            СhangesClient.Visibility = Visibility.Collapsed;
            ListChanges_Label.Visibility = Visibility.Visible;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            СhangesClient.Visibility = Visibility.Visible;
            ListChanges_Label.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Производит сортировку по алфавиту по имени клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sort_Button_Click(object sender, RoutedEventArgs e)
        {
            collectionView.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));

            DataClients.ItemsSource = collectionView;
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="client">Удаляемый клиент</param>
        private void DeleteClient(Client client)
        {
            ViewModel.ClientsRepository.Remove(client);
        }

        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (isDirty)
            {
                e.CanExecute = true;
            }
            else e.CanExecute = false;

        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var saveDlg = new SaveFileDialog { Filter = "Text files|*.csv" };

            if (true == saveDlg.ShowDialog())
            {
                string fileName = saveDlg.FileName;

                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
                {
                    foreach (var emp in DataClients.ItemsSource)
                    {
                        sw.WriteLine(emp.ToString());
                    }
                }

                foreach (var client in ViewModel.ClientsRepository)
                {
                    client.IsChanged = false;
                }
                isDirty = false;
            }
            
        }
    }
}
