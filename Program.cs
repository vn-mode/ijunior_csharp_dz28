using System;

namespace vn_mode_csharp_dz28
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isOpen = true;
            string[] fullNames = { "Бабин Василий Петрович - СТРОИТЕЛЬ", "Нагибин Виталий Павлович - МУЗЫКАНТ", "Марук Юрий Семёнович - ВОДИТЕЛЬ"};
            string[] positions = {"СТРОИТЕЛЬ", "МУЗЫКАНТ", "ВОДИТЕЛЬ"};
            string userFirstName, userLastName, userPatronymic, userPositionAtWork;
            bool isSearch = false;
            int counter = 0;

            while (isOpen)
            {
                Console.WriteLine("Доступные команды:\n1 - Добавить досье\n2 - Вывести все досье\n3 - Удалить досье\n4 - Поиск по фамилии\n5 - Выход");
                Console.Write("\nВведите команду :");
                switch (Console.ReadLine())
                {
                    case "1":
                        showMessage("Добавить досье:");
                        Console.Write("Введите фамилию: ");
                        userLastName = Console.ReadLine();
                        Console.Write("Введите имя: ");
                        userFirstName = Console.ReadLine();
                        Console.Write("Введите очество: ");
                        userPatronymic = Console.ReadLine();
                        Console.Write("Введите должность: ");
                        userPositionAtWork = Console.ReadLine();
                        AddDossier(ref fullNames, ref positions, userFirstName, userLastName, userPatronymic, userPositionAtWork);
                        showMessage("Вы успешно добавили новое досье.");
                        break;
                    case "2":
                        showMessage("Все досье:");
                        showAllDossiers(fullNames, positions);
                        break;
                    case "3":
                        showMessage("Вы выбрали команду 'Удалить досье'.");
                        Console.Write("Введите желаемую фамилию: ");
                        userLastName = Console.ReadLine();
                        DeleteDossier(ref fullNames, ref positions, userLastName, out isSearch);
                        if (!isSearch)
                        {
                            isSearch = false;
                            showMessage("Ничего не найдено", ConsoleColor.DarkRed);
                        }
                        break;
                    case "4":
                        showMessage("Вы выбрали команду 'Поиск по фамилии'.");
                        Console.Write("Введите фамилию: ");
                        userLastName = Console.ReadLine();
                        SearchLastName(fullNames, userLastName, ref counter, out isSearch);
                        if (!isSearch)
                        {
                            isSearch = false;
                            showMessage("Ничего не найдено", ConsoleColor.DarkRed);
                        }
                        break;
                    case "5":
                        showMessage("Вы вышли из программы.");
                        isOpen = false;
                        Console.ReadKey();
                        break;
                    default:
                        showMessage("Вы ввели не существующую команду.", ConsoleColor.DarkRed);
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }

            
        }

        static void AddDossier(ref string[] fullNames, ref string[] positions, string userFirstName, string userLastName, string userPatronymic, string userPositionAtWork)
        {
            string[] tempFullNames = new string[fullNames.Length + 1];

            for (int i = 0; i < fullNames.Length; i++)
            {
                tempFullNames[i] = fullNames[i];
            }

            tempFullNames[tempFullNames.Length - 1] = userLastName + " " + userFirstName + " " + userPatronymic + " - ";
            fullNames = tempFullNames;

            string[] tempPositions = new string[positions.Length + 1];

            for (int i = 0; i < positions.Length; i++)
            {
                tempPositions[i] = positions[i];
            }

            tempPositions[tempPositions.Length - 1] = userPositionAtWork;
            positions = tempPositions;
        }

        static void SearchLastName(string[] fullNames, string lastName, ref int index, out bool isSearch)
        {
            isSearch = false;
            index = 0;
            foreach (string dossier in fullNames)
            {
                string[] tempFullNames = dossier.Split(' ');
                if (tempFullNames[0].ToLower() == lastName.ToLower())
                {
                    index = index + 1;
                    Console.WriteLine(index + ") " + dossier);
                    isSearch = true;
                }
            }
        }

        static void DeleteDossier(ref string[] fullNames,ref string[] position, string lastName, out bool isSearch)
        {
            isSearch = false;
            foreach (var fullName in fullNames)
            {
                string[] tempFullNames = fullName.Split(' ');
                if (tempFullNames[0].ToLower() == lastName.ToLower())
                {
                    Console.WriteLine(fullName);
                    isSearch = true;
                    string[] arrayFullNameForDelete = new string[1];
                    arrayFullNameForDelete[0] = fullName;
                    string[] arrayPositionForDelete = new string[1];
                    arrayPositionForDelete = position;
                    showMessage("Вы хотите удалить это досье? (y/n): ", ConsoleColor.DarkYellow);
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        string[] tempArrayForDelete = new string[fullNames.Length];
                        string[] tempArrayPositionForDelete = new string[position.Length];
                        for (int i = 0; i < tempArrayForDelete.Length; i++)
                        {
                            if (fullNames[i].ToLower() != arrayFullNameForDelete[0].ToLower())
                            {
                                tempArrayForDelete[i] = fullNames[i];
                                tempArrayPositionForDelete[i] = position[i];
                            }
                        }

                        fullNames = tempArrayForDelete;
                        position = tempArrayPositionForDelete;
                        showMessage("Вы успешно удалили досье", ConsoleColor.DarkGreen);
                    }
                }
            }

        }

        static void showMessage(string message, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void showAllDossiers(string[] fullNames, string[] positions)
        {
            int indexDossier = 0;
            for (int i = 0; i < fullNames.Length; i++)
            {
                if (fullNames[i] != null)
                {
                    indexDossier++;
                    Console.WriteLine(indexDossier + ") " + fullNames[i] + positions[i]);
                }
            }

        }
    }
}
