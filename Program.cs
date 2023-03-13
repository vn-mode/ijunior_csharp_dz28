using System;

namespace vn_mode_csharp_dz28
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddDossier = "1";
            const string CommandShowAllDossiers = "2";
            const string CommandDeleteDossier = "3";
            const string CommandSearchDossier = "4";
            const string CommandExit = "5";
            const string CommandError = "Такой команды не существует, попробуйте ещё раз.";

            bool isOpen = true;

            string[] dossiers = new string[0];
            string[] positions = new string[0];

            while (isOpen)
            {
                Console.WriteLine("Доступные команды:\n");
                Console.WriteLine($"{CommandAddDossier} - Добавить досье");
                Console.WriteLine($"{CommandShowAllDossiers} - Показать все досье");
                Console.WriteLine($"{CommandDeleteDossier} - Удалить выбранное досье");
                Console.WriteLine($"{CommandSearchDossier} - Поиск досье по фамилии");
                Console.WriteLine($"{CommandExit} - Выход из программы");

                Console.Write("Введите номер команды: ");

                switch (Console.ReadLine())
                {
                    case CommandAddDossier:
                        AddDossier(ref dossiers, ref positions);
                        break;

                    case CommandShowAllDossiers:
                        ShowDossier(dossiers, positions);
                        break;

                    case CommandDeleteDossier:
                        DeleteDossier(ref dossiers, ref positions);
                        break;

                    case CommandSearchDossier:
                        FindDossier(dossiers, positions);
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

        static void AddDossier(ref string[] dossier, ref string[] position)
        {
            Console.WriteLine("Заполните ФИО сотрудника:");
            ExpansionArray(ref dossier);
            dossier[dossier.Length - 1] = Console.ReadLine();
            Console.WriteLine("На какой должности сотрудник работает?");
            ExpansionArray(ref position);
            position[position.Length - 1] = Console.ReadLine();
            ShowMessage("Досье успешно добавлено.");
        }

        static void ShowDossier(string[] dossier, string[] position)
        {
            Console.WriteLine();
            Console.WriteLine("Список всех досье:");

            for (int i = 0; i < dossier.Length; i++)
            {
                if (dossier[i] != "" && position[i] != "")
                {
                    int indexPosition = i + 1;
                    Console.WriteLine(indexPosition + ". " + dossier[i] + " - " + position[i]);
                }
            }

            if (dossier.Length == 0)
            {
                ShowMessage("Отсутствуют досье", ConsoleColor.DarkRed);
            }
        }

        static void DeleteDossier(ref string[] dossier, ref string[] position)
        {
            Console.WriteLine("Досье под каким номером нужно удалить?");

            try
            {
                int indexForDeleating = Convert.ToInt32(Console.ReadLine());
                if (dossier.Length > 0 && position.Length > 0)
                {
                    CompressionArray(ref dossier, indexForDeleating);
                    CompressionArray(ref position, indexForDeleating);
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

        static void FindDossier(string[] dossier, string[] position)
        {
            Console.WriteLine("Введите фамилию для поиска:");
            string userInput = Console.ReadLine();

            for (int i = 0; i < dossier.Length; i++)
            {
                char separator = ' ';
                string fullName = dossier[i];
                string[] surnameFind = fullName.Split(separator);

                if (userInput.ToLower() == surnameFind[0].ToLower())
                {
                    int indexPosition = i + 1;
                    Console.WriteLine(indexPosition + ". " + dossier[i] + " - " + position[i]);
                    ShowMessage("Досье не найдено.", ConsoleColor.DarkRed);
                }
            }

            if (dossier.Length <= 0)
            {
                ShowMessage("Досье не найдено", ConsoleColor.DarkRed);
            }
        }

        static void ShowMessage(string message, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
