using System;
using System.Collections.Generic;
using System.IO;

namespace CLIApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Summary of button presses");
                Console.WriteLine("2. Summary of rounds recorded");
                Console.WriteLine("3. Summary of lap times");
                Console.WriteLine("4. Compare files");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowButtonPressesSummary();
                        break;
                    case "2":
                        ShowRoundsSummary();
                        break;
                    case "3":
                        ShowLapTimesSummary();
                        break;
                    case "4":
                        ShowComparisonMenu();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void ShowButtonPressesSummary()
        {
            string logFile = GetLogFile();
            int buttonPresses = CountButtonPresses(logFile);

            Console.WriteLine($"Summary of Button Presses:");
            Console.WriteLine($"Total button presses in {logFile}: {buttonPresses}");
        }

        static void ShowRoundsSummary()
        {
            string logFile = GetLogFile();
            int rounds = CountRounds(logFile);
            List<TimeSpan> lapTimes = GetLapTimes(logFile);

            Console.WriteLine($"Summary of Rounds Recorded:");
            Console.WriteLine($"Total rounds in {logFile}: {rounds}");
            Console.WriteLine($"Lap times in {logFile}:");
            foreach (var lapTime in lapTimes)
            {
                Console.WriteLine($"- {lapTime}");
            }
        }

        static void ShowLapTimesSummary()
        {
            string logFile = GetLogFile();
            List<TimeSpan> lapTimes = GetLapTimes(logFile);

            Console.WriteLine($"Summary of Lap Times:");
            Console.WriteLine($"Lap times in {logFile}:");
            foreach (var lapTime in lapTimes)
            {
                Console.WriteLine($"- {lapTime}");
            }
        }

        static void ShowComparisonMenu()
        {
            Console.WriteLine("\nComparison Menu:");
            Console.WriteLine("1. Compare total button presses");
            Console.WriteLine("2. Compare total rounds");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CompareButtonPresses();
                    break;
                case "2":
                    CompareRounds();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }

        static void CompareButtonPresses()
        {
            string logFile1 = GetLogFile("Enter path to first log file: ");
            string logFile2 = GetLogFile("Enter path to second log file: ");

            int buttonPresses1 = CountButtonPresses(logFile1);
            int buttonPresses2 = CountButtonPresses(logFile2);

            Console.WriteLine($"Comparison of Total Button Presses:");
            Console.WriteLine($"Total button presses in {logFile1}: {buttonPresses1}");
            Console.WriteLine($"Total button presses in {logFile2}: {buttonPresses2}");
        }

        static void CompareRounds()
        {
            string logFile1 = GetLogFile("Enter path to first log file: ");
            string logFile2 = GetLogFile("Enter path to second log file: ");

            int rounds1 = CountRounds(logFile1);
            int rounds2 = CountRounds(logFile2);

            Console.WriteLine($"Comparison of Total Rounds:");
            Console.WriteLine($"Total rounds in {logFile1}: {rounds1}");
            Console.WriteLine($"Total rounds in {logFile2}: {rounds2}");
        }

        static string GetLogFile(string prompt = "Enter path to log file: ")
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        static int CountButtonPresses(string filePath)
        {
            int totalButtonPresses = 0;
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.Contains("Button Press"))
                    {
                        totalButtonPresses++;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file '{filePath}': {ex.Message}");
            }
            return totalButtonPresses;
        }

        static int CountRounds(string filePath)
        {
            int totalRounds = 0;
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.Contains("Round"))
                    {
                        totalRounds++;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file '{filePath}': {ex.Message}");
            }
            return totalRounds;
        }

        static List<TimeSpan> GetLapTimes(string filePath)
        {
            List<TimeSpan> lapTimes = new List<TimeSpan>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("Lap Time"))
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2 && TimeSpan.TryParse(parts[1].Trim(), out TimeSpan lapTime))
                        {
                            lapTimes.Add(lapTime);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file '{filePath}': {ex.Message}");
            }
            return lapTimes;
        }
    }
}
