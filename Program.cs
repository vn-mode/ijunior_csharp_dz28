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

            bool isOpen = true;

            string[] dossier = new string[0];
            string[] position = new string[0];

            while (isOpen)
            {
                Console.WriteLine("Доступные команды:\n");
                Console.WriteLine("1 - Добавить досье");
                Console.WriteLine("2 - Показать все досье");
                Console.WriteLine("3 - Удалить выбранное досье");
                Console.WriteLine("4 - Поиск досье по фамилии");
                Console.WriteLine("5 - Выход из программы");

                Console.Write("Введите номер команды: ");

                switch (Console.ReadLine())
                {
                    case CommandAddDossier:
                        AddDossier(ref dossier, ref position);
                        break;
                    case CommandShowAllDossiers:
                        ShowDossier(dossier, position);
                        break;
                    case CommandDeleteDossier:
                        DeleteDossier(ref dossier, ref position);
                        break;
                    case CommandSearchDossier:
                        FindDossier(dossier, position);
                        break;
                    case CommandExit:
                        isOpen = false;
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Вы вышли из программы.");
        }

        static string[] ExpansionArray(ref string[] array)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i];
            }

            array = tempArray;
            return array;
        }

        static string[] CompressionArray(ref string[] array, int indexForCompressed)
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
            return array;
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
                    Console.WriteLine((i + 1) + ". " + dossier[i] + " - " + position[i]);
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
            int indexForDeleating = Convert.ToInt32(Console.ReadLine());
            CompressionArray(ref dossier, indexForDeleating);
            CompressionArray(ref position, indexForDeleating);
            ShowMessage("Досье успешно удалено.");
        }

        static void FindDossier(string[] dossier, string[] position)
        {
            Console.WriteLine("Введите фамилию для поиска:");
            string userInput = Console.ReadLine();

            for (int i = 0; i < dossier.Length; i++)
            {
                string fullName = dossier[i];
                string[] surnameFind = fullName.Split(' ');

                if (userInput.ToLower() == surnameFind[0].ToLower())
                {
                    Console.WriteLine((i + 1) + ". " + dossier[i] + " - " + position[i]);
                }
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
