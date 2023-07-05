using System;

namespace vn_mode_csharp_dz28
{
    class Program
    {
        const string CommandAddDossier = "1";
        const string CommandShowAllDossiers = "2";
        const string CommandDeleteDossier = "3";
        const string CommandSearchDossier = "4";
        const string CommandExit = "5";
        const string CommandError = "Такой команды не существует, попробуйте ещё раз.";

        static string[] fullNames = new string[0];
        static string[] positions = new string[0];
        static bool isOpen = true;

        static void Main(string[] args)
        {
            while (isOpen)
            {
                Console.WriteLine("Доступные команды:\n");
                Console.WriteLine($"{CommandAddDossier} - Добавить досье");
                Console.WriteLine($"{CommandShowAllDossiers} - Показать все досье");
                Console.WriteLine($"{CommandDeleteDossier} - Удалить выбранное досье");
                Console.WriteLine($"{CommandSearchDossier} - Поиск досье по фамилии");
                Console.WriteLine($"{CommandExit} - Выход из программы");

                Console.Write("Введите номер команды: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case CommandAddDossier:
                        AddDossier();
                        break;

                    case CommandShowAllDossiers:
                        ShowAllDossiers();
                        break;

                    case CommandDeleteDossier:
                        DeleteDossier();
                        break;

                    case CommandSearchDossier:
                        SearchDossier();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        ShowMessage(CommandError, ConsoleColor.DarkRed);
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Вы вышли из программы.");
        }

        static void ExpansionArray(ref string[] array)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i];
            }

            array = tempArray;
        }

        static void CompressionArray(ref string[] array, int indexForCompressed)
        {
            string[] tempArray = new string[array.Length - 1];

            for (int i = 0; i < indexForCompressed - 1; i++)
            {
                tempArray[i] = array[i];
            }

            for (int i = indexForCompressed; i < array.Length; i++)
            {
                tempArray[i - 1] = array[i];
            }

            array = tempArray;
        }

        static void AddDossier()
        {
            Console.WriteLine("Заполните ФИО сотрудника:");
            ExpansionArray(ref fullNames);
            fullNames[fullNames.Length - 1] = Console.ReadLine();

            Console.WriteLine("На какой должности сотрудник работает?");
            ExpansionArray(ref positions);
            positions[positions.Length - 1] = Console.ReadLine();

            ShowMessage("Досье успешно добавлено.");
        }

        static void ShowAllDossiers()
        {
            Console.WriteLine();
            Console.WriteLine("Список всех досье:");

            for (int i = 0; i < fullNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(fullNames[i]) && !string.IsNullOrEmpty(positions[i]))
                {
                    int indexPosition = i + 1;
                    Console.WriteLine($"{indexPosition}. {fullNames[i]} - {positions[i]}");
                }
            }

            if (fullNames.Length == 0)
            {
                ShowMessage("Отсутствуют досье", ConsoleColor.DarkRed);
            }
        }

        static void DeleteDossier()
        {
            Console.WriteLine("Досье под каким номером нужно удалить?");

            try
            {
                int indexForDeleting = Convert.ToInt32(Console.ReadLine());
                if (indexForDeleting >= 1 && indexForDeleting <= fullNames.Length)
                {
                    CompressionArray(ref fullNames, indexForDeleting);
                    CompressionArray(ref positions, indexForDeleting);
                    ShowMessage("Досье успешно удалено.");
                }
                else
                {
                    ShowMessage("Досье под таким номером не существует.", ConsoleColor.DarkRed);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Досье под таким номером не существует.", ConsoleColor.DarkRed);
            }
        }

        static void SearchDossier()
        {
            Console.WriteLine("Введите фамилию для поиска:");
            string userInput = Console.ReadLine();

            for (int i = 0; i < fullNames.Length; i++)
            {
                string fullName = fullNames[i];
                string[] nameParts = fullName.Split(' ');

                if (nameParts.Length > 0 && string.Equals(userInput, nameParts[0], StringComparison.OrdinalIgnoreCase))
                {
                    int indexPosition = i + 1;
                    Console.WriteLine($"{indexPosition}. {fullNames[i]} - {positions[i]}");
                    return;
                }
            }

            ShowMessage("Досье не найдено", ConsoleColor.DarkRed);
        }

        static void ShowMessage(string message, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
