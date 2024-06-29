using System;
using System.Reflection;

namespace PersonnelAccounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandCreateFile = "1";
            const string ButtonReadFiles = "2";
            const string ButtonDeleteFile = "3";
            const string ButtonSearchLastName = "4";
            const string ButtonExit = "exit";

            bool isRunning = true;

            string[] fullNames = { "Азаренко Артемий Романович", "Михновец Александр Романович", "Куватов Арсений Романович", "Коновалов Алексей Романович" };
            string[] jobsTitles = { "С# ментор", "С# ментор", "С# ментор", "С# ментор" };

            while (isRunning)
            {
                Console.WriteLine($"Введите {CommandCreateFile}, чтобы создать запись.\n" +
                    $"Введите {ButtonReadFiles}, чтобы просмотреть досье.\n" +
                    $"Введите {ButtonDeleteFile}, чтобы удалить досье.\n" +
                    $"Введите {ButtonSearchLastName}, чтобы найти досье.\n" +
                    $"Введите {ButtonExit}, чтобы выйти.");
                string userInput = Console.ReadLine();

                Console.Clear();

                switch (userInput)
                {
                    case CommandCreateFile:
                        CreateFile(ref fullNames, ref jobsTitles);
                        break;

                    case ButtonReadFiles:
                        ReadFiles(fullNames, jobsTitles);
                        break;

                    case ButtonDeleteFile:
                        DeleteFile(ref fullNames, ref jobsTitles);
                        break;

                    case ButtonSearchLastName:
                        SearchLastName(fullNames, jobsTitles);
                        break;

                    case ButtonExit:
                        isRunning = false;
                        break;

                    default:
                        WriteMessageError();
                        break;
                }
            }

            Console.Clear();

            Console.WriteLine("Вы вышли из программы. Спасибо, что воспользовались нашей программой учёта досье.");
        }

        static void CreateFile(ref string[] fullNames, ref string[] jobsTitles)
        {
            fullNames = ExpandArray(fullNames);
            jobsTitles = ExpandArray(jobsTitles);

            int lastIndexFullNames = fullNames.Length - 1;
            int lastIndexJobsTitles = jobsTitles.Length - 1;

            bool isRunning = true;

            while (isRunning)
            {
                string buttonAccept = "1";
                string buttonCansel = "2";

                string name = ReadFullName();
                string jobTitle = ReadJobName();

                Console.WriteLine($"Нового сотрудника зовут:\n" +
                    $"{name}.\n" +
                    $"Он будет работать в должности:\n" +
                    $"{jobTitle}.\n" +
                    $"Если данные верны, нажмите {buttonAccept}.\n" +
                    $"Если вы передумали брать его на работу, нажмите {buttonCansel}");
                string userInput = Console.ReadLine();

                if (userInput == buttonAccept)
                {
                    fullNames[lastIndexFullNames] = name;
                    jobsTitles[lastIndexJobsTitles] = jobTitle;

                    isRunning = false;
                }
                else if (userInput == buttonCansel)
                {
                    isRunning = false;
                }
                else
                {
                    Console.WriteLine($"Введите всё заново.");
                }
            }
        }

        static string ReadFullName()
        {
            string lastNameInput = null;
            string firstNameInput = null;
            string surnameInput = null;
            string separator = " ";

            Console.WriteLine("Введите фамилию:");
            lastNameInput = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Введите имя:");
            firstNameInput = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Введите отчество:");
            surnameInput = Console.ReadLine();
            Console.Clear();

            string name = $"{lastNameInput}{separator}{firstNameInput}{separator}{surnameInput}";

            return name;
        }

        static string ReadJobName()
        {
            Console.WriteLine("Введите должность:");
            string jobTitle = Console.ReadLine();

            return jobTitle;
        }

        static void ReadFiles(string[] fullNames, string[] jobsTitles)
        {
            string separator = "-";

            for (int i = 0; i < jobsTitles.Length; i++)
            {
                int index = i + 1;
                Console.WriteLine(index + separator + fullNames[i] + separator + jobsTitles[i]);
            }
        }

        static void WriteMessageError()
        {
            Console.WriteLine("Такого варианта нет, пожалуйста попробуйте ввести команду снова.\n" +
                "Или позовите человека на должности Работник, чтобы он всё починил.");
        }

        static void DeleteFile(ref string[] fullNames, ref string[] jobsTitles)
        {
            ReadFiles(fullNames, jobsTitles);

            int index = ReadIndex(fullNames, jobsTitles);

            fullNames = ReduceArray(fullNames, index);
            jobsTitles = ReduceArray(jobsTitles, index);
        }

        static int ReadIndex(string[] fullNames, string[] jobsTitles)
        {
            bool isRunning = true;

            int index = 0;

            while (isRunning)
            {
                Console.WriteLine("Чьё досье хотите удалить? Введите порядковый номер:");
                string userInput = Console.ReadLine();

                bool isSuccess = int.TryParse(userInput, out index);

                index -= 1;

                if (isSuccess && index >= 0 && index < fullNames.Length)
                    isRunning = false;
                else
                    Console.WriteLine("Такого порядкового номера нет.");
            }

            return index;
        }

        static void SearchLastName(string[] fullNames, string[] jobsTitles)
        {
            Console.WriteLine("Введите фамилию:");
            string userInput = Console.ReadLine();

            bool isCoincidence = false;

            for (int i = 0; i < fullNames.Length; i++)
            {
                char separator = ' ';

                string[] tempArray = fullNames[i].Split(separator);

                if (userInput == tempArray[0])
                {
                    Console.WriteLine($"По вашему запросу найдено:\n" +
                        $"{fullNames[i]} - {jobsTitles[i]}\n");
                    isCoincidence = true;
                }

            }

            if (isCoincidence == false)
                Console.WriteLine("Во вашему запросу ничего не найдено.");
        }

        static string[] ExpandArray(string[] array)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                tempArray[i] = array[i];

            array = tempArray;

            return array;
        }

        static string[] ReduceArray(string[] array, int index)
        {
            string[] tempArray = new string[array.Length - 1];

            for (int i = 0; i < index; i++)
                tempArray[i] = array[i];

            for (int i = index + 1; i < array.Length; i++)
                tempArray[i - 1] = array[i];

            array = tempArray;

            return array;
        }
    }
}
