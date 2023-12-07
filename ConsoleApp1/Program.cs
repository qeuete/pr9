using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace disp
{
    public static class ProcessManager
    {
        public static void ShowProcesses()
        {
            Console.WriteLine("Диспетчер задач");
            Console.WriteLine("Список процессов:");
            Console.WriteLine("Для продолжения нажмите «Enter»");
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {

                try
                {
                    Console.WriteLine("   " + $"ID: {process.Id},  Название: {process.ProcessName},  Память: {process.WorkingSet64}");

                }
                catch (Exception e)
                {

                    Console.WriteLine("   " + $"Error: {e.Message}");
                }

            }
            Menu menu = new Menu();
            menu.Show();
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Нажмите «Escape», чтобы вернуться в меню выбора");
                Console.WriteLine("Нажмите «Backspace», чтобы завершить процесс");
                Console.WriteLine("Нажмите «Delete», чтобы завершить все процессы с таким же именем.");

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    KillProcess();
                }
                else if (key.Key == ConsoleKey.Delete)
                {
                    KillProcessByName();
                }
            }
            static void KillProcess()
            {
                try
                {
                    Console.WriteLine("Введите ID выбранного процесса:");
                    int selectedProcessId = int.Parse(Console.ReadLine());

                    Process selectedProcess = Process.GetProcessById(selectedProcessId);
                    selectedProcess.Kill();
                    Console.WriteLine("Процесс завершен.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("ID не найден.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Некорректный ввод ID. Введитe число.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");

                }
            }
            static void KillProcessByName()
            {
                try
                {
                    Console.WriteLine("Введите название процесса для завершения всех процессов с таким  же названием:");
                    string processName = Console.ReadLine();

                    Process[] processes = Process.GetProcessesByName(processName);
                    foreach (Process process in processes)
                    {
                        process.Kill();
                    }
                    Console.WriteLine($"Процессы с названием {processName}  завершены.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");

                }
            }
        }
        internal class Menu
        {
            public int minStrolochka;
            public int maxStrolochka;
            public int Show()
            {
                int pos = 3;
                ConsoleKeyInfo key;
                do
                {
                    Console.SetCursorPosition(0, pos);
                    Console.WriteLine("=>");
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, pos);
                    Console.WriteLine("  ");
                    if (key.Key == ConsoleKey.UpArrow && pos != minStrolochka)
                        pos--;
                    else if (key.Key == ConsoleKey.DownArrow & pos != maxStrolochka)
                        pos++;
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        return -1;
                    }

                } while (key.Key != ConsoleKey.Enter);
                return pos;
            }

        }

    }
    class Program
    {
        static void Main()
        {
            while (true)
            {
                ProcessManager.ShowProcesses();
                Console.Clear();
            }
        }
    }
}