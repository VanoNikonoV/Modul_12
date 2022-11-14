using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;

namespace Modul_12.Models
{
    public class Clients : ObservableCollection<Client>, INotifyCollectionChanged
    {
        public Clients() {  }

        public Clients(string path = "data.csv")  
        {
            //LoadData(path);
             GetClients(50);
        }

        /// <summary>
        /// Возвращает копию коллекции
        /// </summary>
        /// <returns>Копия Clients</returns>
        public Clients Clone()
        {
            var rep =  new Clients();

            foreach (var item in this) 

               {  rep.Add(item); }

            return rep; 
        }

        /// <summary>
        /// Заменяет клиента по указанному индексу
        /// </summary>
        /// <param name="index">Индекс (с нуля) элемента, который требуется заменить</param>
        /// <param name="editClient">Отредактируемый клиент по указанному индексу</param>
        public void EditClient(int index, Client editClient) { SetItem(index, editClient);}

        /// <summary>
        /// Загружает данные о клиентах из файла data.csv
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        private void LoadData(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        while (!reader.EndOfStream)
                        {
                            string[] line = reader.ReadLine().Split('\t');

                            this.Add(new Client(firstName: line[1],
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

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), caption: "Не удалось получить данные");
            }

        }

        private void GetClients(int count)
        {
            long telefon = 79020000000;
            long passport = 6650565461;

            Random random = new Random();

            for (long i = 0; i < count; i++)
            {
                telefon += i;

                passport += random.Next(1, 500);

                this.Add(new Client(firstNames[Clients.randomize.Next(Clients.firstNames.Length)],
                    middleNames[Clients.randomize.Next(Clients.middleNames.Length)],
                    secondNames[Clients.randomize.Next(Clients.secondNames.Length)], 
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
        static Clients()
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

    }
}
