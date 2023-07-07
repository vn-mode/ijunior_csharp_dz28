using System;
using System.Collections.Generic;

namespace vn_mode_csharp_dz28
{
    class Program
    {
        const string CommandAddDossier = "1";
        const string CommandShowAllDossiers = "2";
        const string CommandDeleteDossier = "3";
        const string CommandSearchDossier = "4";
        const string CommandExit = "5";

        static void Main(string[] args)
        {
            string addDossierMessage = CommandAddDossier + " - Добавить досье";
            string showAllDossiersMessage = CommandShowAllDossiers + " - Показать все досье";
            string deleteDossierMessage = CommandDeleteDossier + " - Удалить выбранное досье";
            string searchDossierMessage = CommandSearchDossier + " - Поиск досье по фамилии";
            string exitProgramMessage = CommandExit + " - Выход из программы";

            string availableCommandsMessage = "Доступные команды:\n" + addDossierMessage + "\n" + showAllDossiersMessage + "\n" + deleteDossierMessage + "\n" + searchDossierMessage + "\n" + exitProgramMessage;
            string enterCommandMessage = "Введите номер команды: ";

            DossierManager dossierManager = new DossierManager();

            bool isProgramOpen = true;

            while (isProgramOpen)
            {
                Console.WriteLine(availableCommandsMessage);

                Console.Write(enterCommandMessage);
                string command = Console.ReadLine();

                switch (command)
                {
                    case CommandAddDossier:
                        dossierManager.AddDossier();
                        break;

                    case CommandShowAllDossiers:
                        dossierManager.ShowAllDossiers();
                        break;

                    case CommandDeleteDossier:
                        dossierManager.DeleteDossier();
                        break;

                    case CommandSearchDossier:
                        dossierManager.SearchDossier();
                        break;

                    case CommandExit:
                        isProgramOpen = false;
                        break;

                    default:
                        dossierManager.ShowMessage(DossierManager.CommandErrorMessage, ConsoleColor.DarkRed);
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine(DossierManager.ExitProgramFinalMessage);
        }
    }

    class DossierManager
    {
        private List<string> _fullNames = new List<string>();
        private List<string> _positions = new List<string>();

        public const string DossierAddedMessage = "Досье успешно добавлено.";
        public const string DossierDeletedMessage = "Досье успешно удалено.";
        public const string DossierNotFoundMessage = "Досье не найдено";
        public const string NoDossiersMessage = "Отсутствуют досье";
        public const string DossierNumberNotExistsMessage = "Досье под таким номером не существует.";
        public const string EnterFullNameMessage = "Заполните ФИО сотрудника:";
        public const string EnterPositionMessage = "На какой должности сотрудник работает?";
        public const string DossierToDeleteMessage = "Досье под каким номером нужно удалить?";
        public const string EnterSurnameMessage = "Введите фамилию для поиска:";
        public const string CommandErrorMessage = "Такой команды не существует, попробуйте ещё раз.";
        public const string ExitProgramFinalMessage = "Вы вышли из программы.";

        private void AddToArrays(string fullName, string position)
        {
            _fullNames.Add(fullName);
            _positions.Add(position);
        }

        public void AddDossier()
        {
            Console.WriteLine(EnterFullNameMessage);
            string fullName = Console.ReadLine();

            Console.WriteLine(EnterPositionMessage);
            string position = Console.ReadLine();

            for (int i = 0; i < _fullNames.Count; i++)
            {
                if (_fullNames[i] == fullName && _positions[i] == position)
                {
                    ShowMessage("Досье с таким именем и позицией уже существует.", ConsoleColor.DarkRed);
                    return;
                }
            }

            AddToArrays(fullName, position);
            ShowMessage(DossierAddedMessage);
        }

        public void ShowAllDossiers()
        {
            if (_fullNames.Count == 0)
            {
                ShowMessage(NoDossiersMessage, ConsoleColor.DarkRed);
                return;
            }

            Console.WriteLine("\nСписок всех досье:");

            for (int i = 0; i < _fullNames.Count; i++)
            {
                if (string.IsNullOrEmpty(_fullNames[i]) || string.IsNullOrEmpty(_positions[i]))
                {
                    continue;
                }

                int indexPosition = i + 1;
                Console.WriteLine($"{indexPosition}. {_fullNames[i]} - {_positions[i]}");
            }
        }

        public void DeleteDossier()
        {
            Console.WriteLine(DossierToDeleteMessage);

            int indexForDeleting = Convert.ToInt32(Console.ReadLine()) - 1;

            if (indexForDeleting >= 0 && indexForDeleting < _fullNames.Count)
            {
                _fullNames.RemoveAt(indexForDeleting);
                _positions.RemoveAt(indexForDeleting);
                ShowMessage(DossierDeletedMessage);
            }
            else
            {
                ShowMessage(DossierNumberNotExistsMessage, ConsoleColor.DarkRed);
            }
        }

        public void SearchDossier()
        {
            Console.WriteLine(EnterSurnameMessage);
            string userInput = Console.ReadLine();
            string nameDelimiter = " ";

            bool isFound = false;

            for (int i = 0; i < _fullNames.Count; i++)
            {
                string fullName = _fullNames[i];
                string[] fullNameParts = fullName.Split(nameDelimiter);

                if (fullNameParts.Length > 0 && string.Equals(userInput, fullNameParts[0], StringComparison.OrdinalIgnoreCase))
                {
                    int indexPosition = i + 1;
                    Console.WriteLine($"{indexPosition}. {_fullNames[i]} - {_positions[i]}");
                    isFound = true;
                }
            }

            if (isFound == false)
            {
                ShowMessage(DossierNotFoundMessage, ConsoleColor.DarkRed);
            }
        }

        public void ShowMessage(string message, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
