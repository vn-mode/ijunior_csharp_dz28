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

            const string addDossierMessage = CommandAddDossier + " - Добавить досье";
            const string showAllDossiersMessage = CommandShowAllDossiers + " - Показать все досье";
            const string deleteDossierMessage = CommandDeleteDossier + " - Удалить выбранное досье";
            const string searchDossierMessage = CommandSearchDossier + " - Поиск досье по фамилии";
            const string exitProgramMessage = CommandExit + " - Выход из программы";

            const string availableCommandsMessage = "Доступные команды:\n" + addDossierMessage + "\n" + showAllDossiersMessage + "\n" + deleteDossierMessage + "\n" + searchDossierMessage + "\n" + exitProgramMessage;
            const string enterCommandMessage = "Введите номер команды: ";

            Database database = new Database();

            bool isProgramOpen = true;

            while (isProgramOpen)
            {
                Console.WriteLine(availableCommandsMessage);

                Console.Write(enterCommandMessage);
                string command = Console.ReadLine();

                switch (command)
                {
                    case CommandAddDossier:
                        database.AddDossier();
                        break;

                    case CommandShowAllDossiers:
                        database.ShowAllDossiers();
                        break;

                    case CommandDeleteDossier:
                        database.DeleteDossier();
                        break;

                    case CommandSearchDossier:
                        database.SearchDossier();
                        break;

                    case CommandExit:
                        isProgramOpen = false;
                        break;

                    default:
                        database.ShowMessage(Database.CommandErrorMessage, ConsoleColor.DarkRed);
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine(Database.ExitProgramFinalMessage);
        }
    }

    class Database
    {
        private string[] _fullNames = new string[0];
        private string[] _positions = new string[0];

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

        private void ResizeArray(ref string[] array, string newValue = null)
        {
            string[] tempArray = newValue != null ? new string[array.Length + 1] : new string[array.Length - 1];
            Array.Copy(array, tempArray, array.Length);
            if (newValue != null) tempArray[^1] = newValue;
            array = tempArray;
        }

        private void RemoveAt(ref string[] array, int index)
        {
            string[] newArray = new string[array.Length - 1];
            Array.Copy(array, 0, newArray, 0, index);
            Array.Copy(array, index + 1, newArray, index, array.Length - index - 1);
            array = newArray;
        }

        public void AddDossier()
        {
            Console.WriteLine(EnterFullNameMessage);
            string fullName = Console.ReadLine();

            Console.WriteLine(EnterPositionMessage);
            string position = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(position))
            {
                ShowMessage("ФИО и должность не могут быть пустыми.", ConsoleColor.DarkRed);
                return;
            }

            ResizeArray(ref _fullNames, fullName);
            ResizeArray(ref _positions, position);

            ShowMessage(DossierAddedMessage);
        }

        public void ShowAllDossiers()
        {
            if (_fullNames.Length == 0)
            {
                ShowMessage(NoDossiersMessage, ConsoleColor.DarkRed);
                return;
            }

            Console.WriteLine("\nСписок всех досье:");

            for (int i = 0; i < _fullNames.Length; i++)
            {
                int indexPosition = i + 1;
                Console.WriteLine($"{indexPosition}. {_fullNames[i]} - {_positions[i]}");
            }
        }

        public void DeleteDossier()
        {
            Console.WriteLine(DossierToDeleteMessage);

            int indexForDeleting = Convert.ToInt32(Console.ReadLine()) - 1;

            if (indexForDeleting >= 0 && indexForDeleting < _fullNames.Length)
            {
                RemoveAt(ref _fullNames, indexForDeleting);
                RemoveAt(ref _positions, indexForDeleting);
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

            for (int i = 0; i < _fullNames.Length; i++)
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
