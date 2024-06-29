﻿using System;

namespace PersonnelAccounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ButtonCreateFile = "1";
            const string ButtonReadFiles = "2";
            const string ButtonDeleteFile = "3";
            const string ButtonSearchLastName = "4";
            const string ButtonExit = "exit";

            bool isRunning = true;

            string[] fullNames = { "Азаренко Артемий Романович", "Михновец Александр Романович", "Куватов Арсений Романович", "Коновалов Алексей Романович" };
            string[] jobsTitles = { "С# ментор", "С# ментор", "С# ментор", "С# ментор" };

            while (isRunning)
            {
                Console.WriteLine($"Введите {ButtonCreateFile}, чтобы создать запись.\n" +
                    $"Введите {ButtonReadFiles}, чтобы просмотреть досье.\n" +
                    $"Введите {ButtonDeleteFile}, чтобы удалить досье.\n" +
                    $"Введите {ButtonSearchLastName}, чтобы найти досье.\n" +
                    $"Введите {ButtonExit}, чтобы выйти.");
                string userInput = Console.ReadLine();

                Console.Clear();

                switch (userInput)
                {
                    case ButtonCreateFile:
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
            fullNames = MakeExpandedArray(fullNames);
            jobsTitles = MakeExpandedArray(jobsTitles);

            int lastIndexFullNames = fullNames.Length - 1;
            int lastIndexJobsTitles = jobsTitles.Length - 1;

            bool isRunning = true;

            while (isRunning)
            {
                string buttonAccept = "1";
                string buttonCansel = "2";

                string name = InputNames();
                string jobTitle = InputJobTitle();

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
                    isRunning = false;
                else
                    Console.WriteLine($"Введите всё заново.");
            }
        }

        static string InputNames()
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

            string name = lastNameInput + separator + firstNameInput + separator + surnameInput;

            return name;
        }

        static string InputJobTitle()
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

            bool isRunning = true;

            while (isRunning)
            {
                int index;

                Console.WriteLine("Чьё досье хотите удалить? Введите порядковый номер.");
                string userInput = Console.ReadLine();

                bool isSuccess = int.TryParse(userInput, out index);

                if (isSuccess)
                {
                    for (int i = index; i < fullNames.Length; i++)
                    {
                        fullNames[i - 1] = fullNames[i];
                        jobsTitles[i - 1] = jobsTitles[i];
                    }

                    fullNames = MakeReducedArray(fullNames);
                    jobsTitles = MakeReducedArray(jobsTitles);

                    isRunning = false;
                }
                else
                    Console.WriteLine("Такого порядкового номера нет.");
            }
        }

        static void SearchLastName(string[] fullNames, string[] jobsTitles)
        {
            Console.WriteLine("Введите фамилию:");
            string userInput = Console.ReadLine();

            for (int i = 0; i < fullNames.Length; i++)
            {
                char separator = ' ';

                string[] tempArray = fullNames[i].Split(separator);

                if (userInput == tempArray[0])
                    Console.WriteLine($"По вашему запросу найдено:\n" +
                        $"{fullNames[i]} - {jobsTitles[i]}\n");
            }
        }

        static string[] MakeExpandedArray(string[] array)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                tempArray[i] = array[i];

            array = tempArray;

            return array;
        }

        static string[] MakeReducedArray(string[] array)
        {
            string[] tempArray = new string[array.Length - 1];

            for (int i = 0; i < array.Length - 1; i++)
                tempArray[i] = array[i];

            array = tempArray;

            return array;
        }
    }
}
