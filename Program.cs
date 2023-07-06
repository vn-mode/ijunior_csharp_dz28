using System;

namespace vn_mode_csharp_dz28
{
    class Program
    {
        const string AvailableCommandsMessage = "Доступные команды:\n";
        const string AddDossierMessage = "1 - Добавить досье";
        const string ShowAllDossiersMessage = "2 - Показать все досье";
        const string DeleteDossierMessage = "3 - Удалить выбранное досье";
        const string SearchDossierMessage = "4 - Поиск досье по фамилии";
        const string ExitProgramMessage = "5 - Выход из программы";
        const string EnterCommandMessage = "Введите номер команды: ";
        const string CommandAddDossier = "1";
        const string CommandShowAllDossiers = "2";
        const string CommandDeleteDossier = "3";
        const string CommandSearchDossier = "4";
        const string CommandExit = "5";

        static void Main(string[] args)
        {
            DossierManager dossierManager = new DossierManager();

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine(AvailableCommandsMessage);
                Console.WriteLine(AddDossierMessage);
                Console.WriteLine(ShowAllDossiersMessage);
                Console.WriteLine(DeleteDossierMessage);
                Console.WriteLine(SearchDossierMessage);
                Console.WriteLine(ExitProgramMessage);

                Console.Write(EnterCommandMessage);
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
                        isOpen = false;
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

        private void ModifyArray(ref string[] array, string newValue = null)
        {
            string[] tempArray = newValue != null ? new string[array.Length + 1] : new string[array.Length - 1];

            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i];
            }

            if (newValue != null) tempArray[^1] = newValue;

            array = tempArray;
        }

        public void AddDossier()
        {
            Console.WriteLine(EnterFullNameMessage);
            ModifyArray(ref _fullNames, Console.ReadLine());

            Console.WriteLine(EnterPositionMessage);
            ModifyArray(ref _positions, Console.ReadLine());

            ShowMessage(DossierAddedMessage);
        }

        public void ShowAllDossiers()
        {
            Console.WriteLine();
            Console.WriteLine("Список всех досье:");

            for (int i = 0; i < _fullNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(_fullNames[i]) && !string.IsNullOrEmpty(_positions[i]))
                {
                    int indexPosition = i + 1;
                    Console.WriteLine($"{indexPosition}. {_fullNames[i]} - {_positions[i]}");
                }
            }

            if (_fullNames.Length == 0)
            {
                ShowMessage(NoDossiersMessage, ConsoleColor.DarkRed);
            }
        }

        public void DeleteDossier()
        {
            Console.WriteLine(DossierToDeleteMessage);

            int indexForDeleting = Convert.ToInt32(Console.ReadLine());
            if (indexForDeleting >= 1 && indexForDeleting <= _fullNames.Length)
            {
                ModifyArray(ref _fullNames);
                ModifyArray(ref _positions);
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

            bool found = false;
            for (int i = 0; i < _fullNames.Length; i++)
            {
                string fullName = _fullNames[i];
                string[] nameParts = fullName.Split(nameDelimiter);

                if (nameParts.Length > 0 && string.Equals(userInput, nameParts[0], StringComparison.OrdinalIgnoreCase))
                {
                    int indexPosition = i + 1;
                    Console.WriteLine($"{indexPosition}. {_fullNames[i]} - {_positions[i]}");
                    found = true;
                }
            }

            if (!found)
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
