using System;
using System.Text.RegularExpressions;

namespace Polymorphism
{
    internal static class Action_Text
    {

        public static bool Is_scan_Email(ref string email)
        {
            Console.CursorVisible = true;
            string exp = @"[\w\.\-]+@[\w\.]+\.[a-z]+$";
            string s = Console.ReadLine();
            email = s;
            Console.CursorVisible = false;
            return Regex.IsMatch(s, exp);
        }

        public static bool Is_scan_Time(ref string email)
        {
            Console.CursorVisible = true;
            string exp = "^([8-9]|1[0-6]):[0-5][0-9]|(17:00)$";
            string s = Console.ReadLine();
            email = s;
            Console.CursorVisible = false;
            return Regex.IsMatch(s, exp);
        }


        public static string Scan_Password(int x, int y)
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(x, y);
            string password = "";
            while (true)
            {
                Console.SetCursorPosition(x + password.Length, y);
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.Enter)
                    break;

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.SetCursorPosition(x + password.Length - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(x + password.Length - 1, y);
                    password = password[0..^1];
                }

                else if (key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9 || key.Key >= ConsoleKey.A && key.Key <= ConsoleKey.Z ||
                    key.Key >= ConsoleKey.NumPad0 && key.Key <= ConsoleKey.Decimal || key.Key >= ConsoleKey.OemPlus && key.Key <= ConsoleKey.Oem8 ||
                    key.Key == ConsoleKey.Spacebar)
                {
                    Drawer.Print_Inscription(x + password.Length, y, "*");
                    password += key.KeyChar;
                    Console.SetCursorPosition(x + password.Length, y);
                }
            }
            Console.CursorVisible = false;
            return password;
        }
        public static bool Is_Scan_Full_Name(ref string full_name)
        {
            Console.CursorVisible = true;
            full_name = Console.ReadLine();
            Console.CursorVisible = false;
            return full_name.Split().Length == 3;
        }
    }
}