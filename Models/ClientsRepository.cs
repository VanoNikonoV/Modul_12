using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;

namespace Modul_12.Models
{
    public class ClientsRepository
    {
        #region InfoChanges
        private ObservableCollection<InformationAboutChanges> infoChanges = new ObservableCollection<InformationAboutChanges>();

        public ObservableCollection<InformationAboutChanges> InfoChanges
        {
            get { return this.infoChanges; }
            set
            {
                this.infoChanges = value;
            }
        }
        #endregion

        readonly List<Client> clients;

        public ClientsRepository(string pathDataFile = "data.csv")  
        {
            clients = LoadData(pathDataFile);
        }

        /// <summary>
        /// Возвращает копию коллекции
        /// </summary>
        /// <returns>Копия Clients</returns>
        public List<Client> Clone()
        {
            List<Client> tempClients = new List<Client>();

            foreach (var item in clients) 

               {  tempClients.Add(item); }

            return tempClients;
        }

        /// <summary>
        /// Заменяет клиента
        /// </summary>
        /// <param name="curent">Редактируемый клиент</param>
        /// <param name="editClient">Отредактированный клиент</param>
        public void ReplaceClient(Client curent, Client editClient) //EditeClient
        {
            int index = clients.IndexOf(curent);

            clients.RemoveAt(index);

            clients.Insert(index, editClient);
        }  

        /// <summary>
        /// Загружает данные о клиентах из файла data.csv
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        private List<Client> LoadData(string path)
        {
            List <Client> tempClients = new List<Client>();

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] line = reader.ReadLine().Split('\t');

                        tempClients.Add(new Client(firstName: line[1],
                                                middleName: line[2],
                                                secondName: line[3],
                                                    telefon: line[4],
                                    seriesAndPassportNumber: line[5],
                                                    dateTime: Convert.ToDateTime(line[6]),
                                                    isChanged: false)); 
                    }
                }
                   
            }
            else
            {
                MessageBox.Show("Не найден файл с данными",
                caption: "Ощибка в чтении данных",
                MessageBoxButton.OK,
                icon: MessageBoxImage.Error);
            }
            return tempClients;
        }

        static List<Client> GetClients(int count)
        {
            List<Client> tempClients = new List<Client>();

            long telefon = 79020000000;
            long passport = 6650565461;

            Random random = new Random();

            for (long i = 0; i < count; i++)
            {
                telefon += i;

                passport += random.Next(1, 500);

                tempClients.Add(new Client($"Имя_{i}", $"Отчество_{i}", $"Фамилия_{i}", telefon.ToString(), passport.ToString()));
            }
            return tempClients;
        }

    }
}
