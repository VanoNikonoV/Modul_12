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
        readonly List<Client> clients;

        public List<Client> GetClients() { return clients; }

        public ClientsRepository(string path = "data.csv")  
        {
            clients = LoadData(path);

            //clients = new List<Client>();
                
            //GetClientsRep(100);
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

        public void AddClient(Client newClient)
        {
            clients.Add(newClient);
        }

        /// <summary>
        /// Загружает данные о клиентах из файла data.csv
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        private List<Client> LoadData(string path)
        {
            List<Client> tempClients = new List<Client>();
            
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
                                                dateTime: Convert.ToDateTime(line[6]))); 
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

        #region Автогенерация данных
        private void GetClientsRep(int count)
        {
            long telefon = 79020000000;
            long passport = 6650565461;

            Random random = new Random();

            for (long i = 0; i < count; i++)
            {
                telefon += i;

                passport += random.Next(1, 500);

                clients.Add(new Client(firstNames[ClientsRepository.randomize.Next(ClientsRepository.firstNames.Length)],
                    middleNames[ClientsRepository.randomize.Next(ClientsRepository.middleNames.Length)],
                    secondNames[ClientsRepository.randomize.Next(ClientsRepository.secondNames.Length)], 
                    telefon.ToString(), 
                    passport.ToString()));
            }
            
        }

        static readonly string[] firstNames;

        static readonly string[] middleNames;

        static readonly string[] secondNames;

        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        static Random randomize;

        /// <summary>
        /// Статический конструктор, в котором "хранятся"
        /// данные о именах и фамилиях баз данных firstNames и lastNames
        /// </summary>
        static ClientsRepository()
        {
            randomize = new Random(); 

            firstNames = new string[] {
                "Агата",
                "Агнес",
                "Мария",
                "Аделина",
                "Ольга",
                "Людмила",
                "Аманда",
                "Татьяна",
                "Вероника",
                "Жанна",
                "Крестина",
                "Анжела",
                "Маргарита"
            };

            middleNames = new string[]
            {
                "Ивановна",
                "Петровна",
                "Васильевна",
                "Сергеевна",
                "Дмитриевна",
                "Владимировна",
                "Александровна",
                "Тимофеевна"
                
            };

            secondNames = new string[]
            {
                "Иванова",
                "Петрова",
                "Васильева",
                "Кузнецова",
                "Ковалёва",
                "Попова",
                "Пономарёва",
                "Дьячкова",
                "Коновалова",
                "Соколова",
                "Лебедева",
                "Соловьёва",
                "Козлова",
                "Волкова",
                "Зайцева",
                "Ершова",
                "Карпова",
                "Щукина",
                "Виноградова",
                "Цветкова",
                "Калинина"
            };

        }
        #endregion
    }
}
