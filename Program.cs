using System;

namespace vn_mode_csharp_dz28
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isOpen = true;
            string[] fullNameArray = { "Бабин Василий Петрович - СТРОИТЕЛЬ", "Нагибин Виталий Павлович - МУЗЫКАНТ", "Нагибин Павел Александрович - ВОДИТЕЛЬ"};
            string userFirstName, userLastName, userPatronymic, userPositionAtWork;
            bool isSearch = false;
            int counter = 0;

            while (isOpen)
            {
                Console.WriteLine("Доступные команды:\n1 - Добавить досье\n2 - Вывести все досье\n3 - Удалить досье\n4 - Поиск по фамилии\n5 - Выход");
                Console.Write("\nВведите команду :");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Alert("Добавить досье:");
                        Console.Write("Введите фамилию: ");
                        userLastName = Console.ReadLine();
                        Console.Write("Введите имя: ");
                        userFirstName = Console.ReadLine();
                        Console.Write("Введите очество: ");
                        userPatronymic = Console.ReadLine();
                        Console.Write("Введите должность: ");
                        userPositionAtWork = Console.ReadLine();
                        AddDossier(ref fullNameArray, userFirstName, userLastName, userPatronymic, userPositionAtWork);
                        Alert("Вы успешно добавили новое досье.");
                        break;
                    case 2:
                        Alert("Все досье:");
                        AllDossiers(ref fullNameArray);
                        break;
                    case 3:
                        Alert("Вы выбрали команду 'Удалить досье'.");
                        Console.Write("Введите желаемую фамилию: ");
                        userLastName = Console.ReadLine();
                        DeleteDossier(ref fullNameArray, userLastName, out isSearch);
                        if (!isSearch)
                        {
                            isSearch = false;
                            Alert("Ничего не найдено", ConsoleColor.DarkRed);
                        }
                        break;
                    case 4:
                        Alert("Вы выбрали команду 'Поиск по фамилии'.");
                        Console.Write("Введите фамилию: ");
                        userLastName = Console.ReadLine();
                        SearchLastName(ref fullNameArray, userLastName, ref counter, out isSearch);
                        if (!isSearch)
                        {
                            isSearch = false;
                            Alert("Ничего не найдено", ConsoleColor.DarkRed);
                        }
                        break;
                    case 5:
                        Alert("Вы вышли из программы.");
                        isOpen = false;
                        Console.ReadKey();
                        break;
                    default:
                        Alert("Вы ввели не существующую команду.", ConsoleColor.DarkRed);
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }

            static void AddDossier(ref string[] fullNameArray, string userFirstName, string userLastName, string userPatronymic, string userPositionAtWork)
            {
                string[] tempFullNameArray = new string[fullNameArray.Length + 1];

                for (int i = 0; i < fullNameArray.Length; i++)
                {
                    tempFullNameArray[i] = fullNameArray[i];
                }

                tempFullNameArray[tempFullNameArray.Length - 1] = userLastName + " " + userFirstName + " " + userPatronymic + " - " + userPositionAtWork;
                fullNameArray = tempFullNameArray;
            }

            static void SearchLastName(ref string[] fullNameArray, string lastName, ref int counter, out bool isSearch)
            {
                isSearch = false;
                counter = 0;
                foreach (string dossier in fullNameArray)
                {
                    string[] tempFullNameArray = dossier.Split(' ');
                    if (tempFullNameArray[0].ToLower() == lastName.ToLower())
                    {
                        counter = counter + 1;
                        Console.WriteLine(counter + ") " + dossier);
                        isSearch = true;
                    }
                }
            }

            static void DeleteDossier(ref string[] fullNameArray, string lastName, out bool isSearch)
            {
                isSearch = false;
                foreach (var fullName in fullNameArray)
                {
                    string[] tempFullNameArray = fullName.Split(' ');
                    if (tempFullNameArray[0].ToLower() == lastName.ToLower())
                    {
                        Console.WriteLine(fullName);
                        isSearch = true;
                        string[] arrayForDelete = new string[1];
                        arrayForDelete[0] = fullName;
                        Alert("Вы хотите удалить это досье? (y/n): ", ConsoleColor.DarkYellow);
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            string[] tempArrayForDelete = new string[fullNameArray.Length];
                            for (int i = 0; i < tempArrayForDelete.Length; i++)
                            {
                                if (fullNameArray[i].ToLower() != arrayForDelete[0].ToLower())
                                {
                                    tempArrayForDelete[i] = fullNameArray[i];
                                }
                            }

                            fullNameArray = tempArrayForDelete;
                            Alert("Вы успешно удалили досье", ConsoleColor.DarkGreen);
                        }
                    }
                }

            }

            static void Alert(string message, ConsoleColor color = ConsoleColor.DarkGreen)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }

            static void AllDossiers(ref string[] fullNameArray)
            {
                int j = 0;
                for (int i = 0; i < fullNameArray.Length; i++)
                {
                    if (fullNameArray[i] != null)
                    {
                        j++;
                        Console.WriteLine(j + ") " + fullNameArray[i]);
                    }
                }

            }
        }
    }
}
