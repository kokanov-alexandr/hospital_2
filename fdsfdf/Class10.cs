using System;

namespace Polymorphism
{
    internal static class Windows_Action
    {

        internal static void Exit()
        {
            Console.Clear();
            string s = "Спасибо, что воспользовались нашей больничкой)";
            Drawer.Print_Inscription(Console.WindowWidth / 2 - s.Length / 2, Console.WindowHeight / 2, s, ConsoleColor.Yellow);
            System.Threading.Thread.Sleep(1600);
            Environment.Exit(0);
        }

        public static bool Yes_No_Buttons(Point cords_1, Point cords_2, string button_1, string buttin_2)
        {
            int count = 0;
            while (true)
            {
                if (count == 0)
                {
                    Drawer.Print_Inscription(cords_1.x, cords_1.y, button_1);
                    Drawer.Print_Inscription(cords_2.x, cords_2.y, buttin_2, ConsoleColor.Green);
                }

                if (count == 1)
                {
                    Drawer.Print_Inscription(cords_2.x, cords_2.y, buttin_2);
                    Drawer.Print_Inscription(cords_1.x, cords_1.y, button_1, ConsoleColor.Red);
                }
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    if (count == 0)
                        return true;
                    return false;
                }
                if (consoleKeyInfo.Key == ConsoleKey.Tab)
                    count = (count + 1) % 2;
            }
        }


        internal static bool Change_of_coordinates(ref Window window_1, ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (window_1.cords.y > 1)
                        window_1.cords.y--;
                    return true;
                case ConsoleKey.DownArrow:
                    if (window_1.cords.y + window_1.size.x < Constants.BORDER_CONSOL_Y)
                        window_1.cords.y++;
                    return true;
                case ConsoleKey.LeftArrow:
                    if (window_1.cords.x > 1)
                        window_1.cords.x--;
                    return true;
                case ConsoleKey.RightArrow:
                    if (window_1.cords.x + window_1.size.y < Constants.BORDED_CONSOL_X)
                        window_1.cords.x++;
                    return true;
                default:
                    return false;
            }
        }




        public static int Windows_Begin(ref Window window, ref All_Info All_Win)
        {
            bool is_change_cords = false;
            Console.Clear();
            Drawer.Create_Pic(window.cords, window.size);
            while (true)
            {
                if (is_change_cords)
                {
                    Console.Clear();
                    Drawer.Create_Pic(window.cords, window.size);
                }

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 3, "Добро пожаловать в нашу больницу!");
                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 6, "Войти");
                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 8, "Зарегистрироваться");

                switch (window.counter_tab)
                {
                    case 0:
                        Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 6, "Войти", ConsoleColor.Green);
                        break;
                    case 1:
                        Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 8, "Зарегистрироваться", ConsoleColor.Green);
                        break;

                }

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                is_change_cords = Change_of_coordinates(ref window, consoleKeyInfo);

                if (consoleKeyInfo.Key == ConsoleKey.Tab)
                    window.counter_tab = (window.counter_tab + 1) % 2;

                if (consoleKeyInfo.Key == ConsoleKey.Escape)
                    Exit();

                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    switch (window.counter_tab)
                    {
                        case 0:
                            return Input(ref window, ref All_Win);
                        case 1:
                            return Registration(ref window, ref All_Win);
                    }
                }

            }
        }


        public static int Registration(ref Window window, ref All_Info All_Win)
        {
            bool is_was_mistake = false;
            int scan_counter = 1;
            string password = "", email = "", password_2, name = "";
            Console.Clear();
            Drawer.Create_Pic(window.cords, window.size);
            while (scan_counter != 5)
            {
                if (is_was_mistake)
                {
                    Console.Clear();
                    Drawer.Create_Pic(window.cords, window.size);
                }
                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 23, "Назад");
                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 1, "Регистрация");

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 3, "Введите ФИО:");
                Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 5), new Point(3, 40));

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 8, "Введите адрес электонной почты:");
                Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 10), new Point(3, 40));

                if (scan_counter > 1)
                    Drawer.Print_Inscription(window.cords.x + 6, window.cords.y + 5, name);

                if (scan_counter > 2)
                    Drawer.Print_Inscription(window.cords.x + 6, window.cords.y + 10, email);

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 13, "Придумайте пароль:");
                Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 15), new Point(3, 40));

                if (scan_counter > 3)
                {
                    Console.SetCursorPosition(window.cords.x + 5, window.cords.y + 15);
                    for (int i = 0; i < password.Length; i++)
                        Console.Write('*');
                }

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 18, "Повторите пароль:");
                Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 20), new Point(3, 40));

                if (is_was_mistake)
                {
                    Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 23, "Назад", ConsoleColor.Red);
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                    if (consoleKeyInfo.Key == ConsoleKey.Enter)
                        return -1;
                    Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 23, "Назад");
                }

                switch (scan_counter)
                {
                    case 1:
                        Console.SetCursorPosition(window.cords.x + 6, window.cords.y + 5);
                        if (!Action_Text.Is_Scan_Full_Name(ref name))
                        {
                            is_was_mistake = true;
                            Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 7, "Введено некорректное ФИО!", ConsoleColor.Red);
                            Console.ReadKey();
                        }
                        else
                        {
                            scan_counter++;
                            is_was_mistake = false;
                        }

                        break;
                    case 2:
                        Console.SetCursorPosition(window.cords.x + 6, window.cords.y + 10);
                        if (!Action_Text.Is_scan_Email(ref email))
                        {
                            Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 12, "Некорректный адрес электронной почты!", ConsoleColor.Red);
                            Console.ReadKey();
                            is_was_mistake = true;
                        }
                        else
                        {
                            scan_counter++;
                            is_was_mistake = false;
                        }
                        break;
                    case 3:
                        password = Action_Text.Scan_Password(window.cords.x + 5, window.cords.y + 15);
                        scan_counter++;
                        is_was_mistake = false;
                        break;
                    case 4:
                        Console.SetCursorPosition(window.cords.x + 5, window.cords.y + 20);
                        password_2 = Action_Text.Scan_Password(window.cords.x + 5, window.cords.y + 20);
                        if (password != password_2 || password.Length < 6)
                        {

                            if (password != password_2)
                                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 22, "Пароли не совпадают!", ConsoleColor.Red);
                            else if (password.Length < 6)
                                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 22, "Ненадёжный пароль!", ConsoleColor.Red);
                            is_was_mistake = true;
                            scan_counter = 3;
                            Console.ReadKey();
                        }
                        else
                        {
                            bool is_new = true;
                            foreach (var user in All_Win.all_doctors)
                            {
                                if (user.Mail == email)
                                {
                                    Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 22, "Данный аккаунт уже зарегистрирован!", ConsoleColor.Red);
                                    scan_counter = 1;
                                    is_was_mistake = true;
                                    is_new = false;
                                    Console.ReadKey();
                                    break;
                                }

                            }
                            if (is_new) scan_counter = 5;
                        }
                        break;
                }
            }
            if (!Yes_No_Buttons(new Point(window.cords.x + 5, window.cords.y + 23), new Point(window.cords.x + 25, window.cords.y + 23),
                "Назад", "Ок"))
                return -1;

            All_Win.all_doctors.Add(new Doctor(name, email, password));
            return All_Win.all_doctors.Count - 1;
        }


        public static int Input(ref Window window, ref All_Info All_win)
        {
            int index = -1;
            string password, email;
            while (true)
            {
                Console.Clear();
                Drawer.Create_Pic(window.cords, window.size);
                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 1, "Вход");

                Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 5), new Point(3, 40));
                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 3, "Введите адрес электонной почты:");

                Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 11), new Point(3, 40));
                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 9, "Введите пароль:");

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 23, "Назад", ConsoleColor.Red);

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    Console.CursorVisible = false;
                    return -1;
                }

                else
                    Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 23, "Назад");

                Console.SetCursorPosition(window.cords.x + 5, window.cords.y + 5);

                Console.CursorVisible = true;
                email = Console.ReadLine();
                password = Action_Text.Scan_Password(window.cords.x + 5, window.cords.y + 11);
                Console.CursorVisible = false;

                for (int i = 0; i < All_win.all_doctors.Count; i++)
                {
                    if (All_win.all_doctors[i].Mail == email && All_win.all_doctors[i].Password == password)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                    break;
                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 14, "Неправильный пароль или адрес электронной почты!", ConsoleColor.Red);
                Console.ReadKey();

            }
            if (!Yes_No_Buttons(new Point(window.cords.x + 5, window.cords.y + 23), new Point(window.cords.x + 25, window.cords.y + 23),
               "Назад", "Ок"))
                return -1;
            else return index;
        }
    }
}