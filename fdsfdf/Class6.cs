using System;
using System.Collections.Generic;
using System.Linq;

namespace Polymorphism
{
    internal static class IN
    {

        internal static IEnumerable<Patient> Сhoice_Day(List<Patient> vs, int day)
        {
            return vs.FindAll((Patient pt) => pt.day_of_the_week == day).OrderBy((Patient a) => Time_To_Int(a.time));
        }
        internal static int Time_To_Int(string time)
        {
            string answer = "";
            for (int i = 0; i < time.Length; i++)
            {
                if (time[i] != ':')
                    answer += time[i];
            }
            return Convert.ToInt32(answer);
        }


        internal static bool Is_empty_time(All_Info all_info, string time, string time_2, int day)
        {
            bool is_empty = true;
            foreach (var t in Сhoice_Day(all_info.all_doctors[all_info.window.index_doctor].patients, day))
            {
                if (!((Time_To_Int(time) < Time_To_Int(t.time) && Time_To_Int(time) < Time_To_Int(t.time_2) &&
                    Time_To_Int(time_2) < Time_To_Int(t.time) && Time_To_Int(time_2) < Time_To_Int(t.time_2)) ||
                    (Time_To_Int(time) > Time_To_Int(t.time) && Time_To_Int(time) > Time_To_Int(t.time_2) &&
                    Time_To_Int(time_2) > Time_To_Int(t.time) && Time_To_Int(time_2) > Time_To_Int(t.time_2))))
                {
                    return false;
                }
            }
            return true;
        }
        internal static int Main_Page(ref All_Info all_info)
        {
            bool is_change_pic = false;
            int counter_tab = 0;
            Console.Clear();
            Drawer.Create_Pic(all_info.window.cords, all_info.window.size);
            while (true)
            {
                if (is_change_pic)
                {
                    Console.Clear();
                    Drawer.Create_Pic(all_info.window.cords, all_info.window.size);
                }
                Drawer.Print_Inscription(all_info.window.cords.x + 3, all_info.window.cords.y + 1, $"Добро пожаловать, {all_info.all_doctors[all_info.window.index_doctor].Full_name}!");
                Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 5, "Выйти");
                Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 8, "Расписание");

                switch (counter_tab)
                {
                    case 0:
                        Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 5, "Выйти", ConsoleColor.Red);
                        break;
                    case 1:
                        Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 8, "Расписание", ConsoleColor.Green);
                        break;
                }

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                is_change_pic = Windows_Action.Change_of_coordinates(ref all_info.window, consoleKeyInfo);

                if (consoleKeyInfo.Key == ConsoleKey.Tab)
                    counter_tab = (counter_tab + 1) % 2;

                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    switch (counter_tab)
                    {
                        case 0:
                            return -1;
                        case 1:
                            Weekly_Schedule(ref all_info);
                            is_change_pic = true;
                            break;
                    }
                }
                else
                    Drawer.Clear_Char();
            }

        }

        internal static int Weekly_Schedule(ref All_Info all_info)
        {
            var weekday = new Dictionary<int, string>() { [1] = "Понедельник", [2] = "Вторник", [3] = "Среда", [4] = "Четверг", [5] = "Пятница" };
            int[] counter_patient;
            int counter_button = 1;
            bool is_change_pic = false;

            Console.Clear();
            Drawer.Create_Pic(all_info.window.cords, all_info.window.size);

            while (true)
            {
                if (is_change_pic)
                {
                    Console.Clear();
                    Drawer.Create_Pic(all_info.window.cords, all_info.window.size);
                }

                counter_patient = new int[5];
                foreach (Patient pat in all_info.all_doctors[all_info.window.index_doctor].patients)
                    counter_patient[pat.day_of_the_week - 1]++;

                Drawer.Print_Inscription(all_info.window.cords.x + 15, all_info.window.cords.y + 2, "Расписание на неделю");

                for (int i = 0; i < 5; i++)
                    Drawer.Print_Inscription(all_info.window.cords.x + 4, all_info.window.cords.y + 4 + i * 2, $"{weekday[i + 1]} - {counter_patient[i]} пациентов");

                Drawer.Print_Inscription(all_info.window.cords.x + 4, all_info.window.cords.y + 14, "Назад");

                for (int i = 4; i <= 12; i += 2)
                    Drawer.Print_Inscription(all_info.window.cords.x + 35, all_info.window.cords.y + i, "Открыть");

                if (counter_button == 6)
                    Drawer.Print_Inscription(all_info.window.cords.x + 4, all_info.window.cords.y + 14, "Назад", ConsoleColor.Red);
                else
                    Drawer.Print_Inscription(all_info.window.cords.x + 35, all_info.window.cords.y + 2 + counter_button * 2, "Открыть", ConsoleColor.Green);


                ConsoleKeyInfo key = Console.ReadKey();

                is_change_pic = Windows_Action.Change_of_coordinates(ref all_info.window, key);

                if (key.Key == ConsoleKey.Tab)
                {
                    counter_button++;
                    if (counter_button > 6)
                        counter_button = 1;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    if (counter_button == 6)
                        return -1;
                    else
                    {
                        Day_Schedule(ref all_info, counter_button);
                        is_change_pic = true;
                    }
                }
            }

        }

        internal static void Day_Schedule(ref All_Info all_info, int day)
        {
            int max_counter_tab;
            int counter_tab = 0;
            bool is_change_pic = false;
            var weekday = new Dictionary<int, string>() { [1] = "понедельник", [2] = "вторник", [3] = "среда", [4] = "четверг", [5] = "пятница" };

            Console.Clear();
            Drawer.Create_Pic(all_info.window.cords, all_info.window.size);

            while (true)
            {
                if (is_change_pic)
                {
                    Console.Clear();
                    Drawer.Create_Pic(all_info.window.cords, all_info.window.size);
                }

                max_counter_tab = all_info.all_doctors[all_info.window.index_doctor].patients.Count * 2 + 2;

                Drawer.Print_Inscription(all_info.window.cords.x + 15, all_info.window.cords.y + 2, $"Расписание на {weekday[day]}");

                int i = 0;
                foreach (var pat in Сhoice_Day(all_info.all_doctors[all_info.window.index_doctor].patients, day))
                {
                    string[] tmp = pat.name.Split();
                    string n = $"{tmp[0]} {tmp[1].Substring(0, 1)}.{tmp[2].Substring(0, 1)}.";
                    Drawer.Print_Inscription(all_info.window.cords.x + 3, all_info.window.cords.y + 4 + i, $"{pat.time} - {pat.time_2} - {n}");
                    Drawer.Print_Inscription(all_info.window.cords.x + all_info.window.size.y - 10, all_info.window.cords.y + 4 + i, "X");
                    Drawer.Print_Inscription(all_info.window.cords.x + all_info.window.size.y - 7, all_info.window.cords.y + 4 + i, "Ред");
                    i += 2;

                }
                if (i == 0)
                    Drawer.Print_Inscription(all_info.window.cords.x + 15, all_info.window.cords.y + 4, "Список пациентов пуст");

                Drawer.Print_Inscription(all_info.window.cords.x + 40, all_info.window.cords.y + 22, "Добавить");
                Drawer.Print_Inscription(all_info.window.cords.x + 6, all_info.window.cords.y + 22, "Назад");

                if (counter_tab == 0)
                    Drawer.Print_Inscription(all_info.window.cords.x + 40, all_info.window.cords.y + 22, "Добавить", ConsoleColor.Green);
                else if (counter_tab == 1)
                    Drawer.Print_Inscription(all_info.window.cords.x + 6, all_info.window.cords.y + 22, "Назад", ConsoleColor.Red);
                else if (counter_tab % 2 == 0)
                    Drawer.Print_Inscription(all_info.window.cords.x + all_info.window.size.y - 10, all_info.window.cords.y + counter_tab - counter_tab % 2 + 2, "X", ConsoleColor.Red);
                else
                    Drawer.Print_Inscription(all_info.window.cords.x + all_info.window.size.y - 7, all_info.window.cords.y + counter_tab - counter_tab % 2 + 2, "Ред", ConsoleColor.Green);

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                is_change_pic = Windows_Action.Change_of_coordinates(ref all_info.window, consoleKeyInfo);

                if (consoleKeyInfo.Key == ConsoleKey.Tab)
                    counter_tab = (counter_tab + 1) % max_counter_tab;

                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    if (counter_tab == 0)
                    {
                        Add_Patient(ref all_info, day);
                        is_change_pic = true;
                    }

                    else if (counter_tab == 1)
                        return;

                    else if (counter_tab % 2 == 0)
                    {
                        all_info.all_doctors[all_info.window.index_doctor].patients.RemoveAt(counter_tab / 2 - 1);
                        is_change_pic = true;
                    }

                    else
                    {
                        var pat = all_info.all_doctors[all_info.window.index_doctor].patients[counter_tab / 2 - 1];
                        Change_Patient(ref pat, ref all_info, day);
                        all_info.all_doctors[all_info.window.index_doctor].patients[counter_tab / 2 - 1] = pat;
                        is_change_pic = true;
                    }
                    counter_tab = 0;
                }
            }
        }


        internal static void Add_Patient(ref All_Info all_info, int day)
        {
            bool is_was_mistake = false;
            int scan_counter = 1;
            string time = "", time_2 = "", name = "";

            Console.Clear();
            Drawer.Create_Pic(all_info.window.cords, all_info.window.size);
            while (scan_counter != 4)
            {
                if (is_was_mistake)
                {
                    Console.Clear();
                    Drawer.Create_Pic(all_info.window.cords, all_info.window.size);
                }

                Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 3, "Введите ФИО пациента:");
                Drawer.Create_Pic(new Point(all_info.window.cords.x + 5, all_info.window.cords.y + 5), new Point(3, 40));

                Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 8, "Введите время начала приёма:");
                Drawer.Create_Pic(new Point(all_info.window.cords.x + 5, all_info.window.cords.y + 10), new Point(3, 20));

                Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 13, "Введите время окончания приёма:");
                Drawer.Create_Pic(new Point(all_info.window.cords.x + 5, all_info.window.cords.y + 15), new Point(3, 20));

                Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 23, "Назад");

                if (scan_counter > 1)
                    Drawer.Print_Inscription(all_info.window.cords.x + 6, all_info.window.cords.y + 5, name);

                if (scan_counter > 2)
                    Drawer.Print_Inscription(all_info.window.cords.x + 6, all_info.window.cords.y + 10, time);

                if (is_was_mistake)
                {
                    Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 23, "Назад", ConsoleColor.Red);
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                    if (consoleKeyInfo.Key == ConsoleKey.Enter)
                        return;
                    else
                        Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 23, "Назад");
                }


                switch (scan_counter)
                {
                    case 1:
                        Console.SetCursorPosition(all_info.window.cords.x + 6, all_info.window.cords.y + 5);
                        if (!Action_Text.Is_Scan_Full_Name(ref name))
                        {
                            is_was_mistake = true;
                            Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 7, "Введено некорректное ФИО!", ConsoleColor.Red);
                            Console.ReadKey();
                        }
                        else
                        {
                            scan_counter++;
                            is_was_mistake = false;
                        }
                        break;
                    case 2:
                        Console.SetCursorPosition(all_info.window.cords.x + 6, all_info.window.cords.y + 10);
                        if (!Action_Text.Is_scan_Time(ref time))
                        {
                            Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 12, "Некорректные данные!", ConsoleColor.Red);
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
                        Console.SetCursorPosition(all_info.window.cords.x + 6, all_info.window.cords.y + 15);
                        if (!Action_Text.Is_scan_Time(ref time_2))
                        {
                            Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 17, "Некорректные данные!", ConsoleColor.Red);
                            Console.ReadKey();
                            is_was_mistake = true;
                        }

                        else
                        {
                            bool is_empty = true;
                            foreach (var t in Сhoice_Day(all_info.all_doctors[all_info.window.index_doctor].patients, day))
                            {
                                if (!((Time_To_Int(time) < Time_To_Int(t.time) && Time_To_Int(time) < Time_To_Int(t.time_2) &&
                                    Time_To_Int(time_2) < Time_To_Int(t.time) && Time_To_Int(time_2) < Time_To_Int(t.time_2)) ||
                                    (Time_To_Int(time) > Time_To_Int(t.time) && Time_To_Int(time) > Time_To_Int(t.time_2) &&
                                    Time_To_Int(time_2) > Time_To_Int(t.time) && Time_To_Int(time_2) > Time_To_Int(t.time_2))))
                                {
                                    Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 17, "Время занято!", ConsoleColor.Red);
                                    Console.ReadKey();
                                    is_was_mistake = true;
                                    is_empty = false;
                                    scan_counter--;
                                    break;
                                }
                            }
                            if (is_empty)
                            {
                                scan_counter++;
                                is_was_mistake = false;
                            }
                        }
                        break;

                }
            }
            if (Windows_Action.Yes_No_Buttons(new Point(all_info.window.cords.x + 5, all_info.window.cords.y + 23), new Point(all_info.window.cords.x + 25, all_info.window.cords.y + 23),
               "Назад", "Ок"))
                all_info.all_doctors[all_info.window.index_doctor].patients.Add(new Patient(time, time_2, name, day));



        }


        internal static void Change_Patient(ref Patient patient, ref All_Info all_info, int day)
        {
            Patient patient_2 = patient;
            bool is_was_mistake = false;
            int counter_tab = 0;
            string str = " ";
            var window = all_info.window;

            Console.Clear();
            Drawer.Create_Pic(window.cords, window.size);
            while (true)
            {
                if (is_was_mistake)
                {
                    Console.Clear();
                    Drawer.Create_Pic(window.cords, window.size);
                }

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 3, "Введите ФИО пациента:");
                Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 5), new Point(3, 40));

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 8, "Введите время начала приёма:");
                Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 10), new Point(3, 20));

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 13, "Введите время окончания приёма:");
                Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 15), new Point(3, 20));

                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 23, "Назад");
                Drawer.Print_Inscription(window.cords.x + 25, window.cords.y + 23, "Ок", ConsoleColor.White);

                Drawer.Print_Inscription(window.cords.x + 6, window.cords.y + 5, patient_2.name);

                Drawer.Print_Inscription(window.cords.x + 6, window.cords.y + 10, patient_2.time);

                Drawer.Print_Inscription(window.cords.x + 6, window.cords.y + 15, patient_2.time_2);

                if (is_was_mistake)
                {
                    Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 23, "Назад", ConsoleColor.Red);
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                        return;
                    else
                    {
                        is_was_mistake = false;
                        counter_tab = 1;
                    }
                    Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 23, "Назад", ConsoleColor.Red);
                }


                switch (counter_tab)
                {
                    case 0:
                        Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 23, "Назад", ConsoleColor.Red);
                        break;
                    case 1:
                        Drawer.Print_Inscription(window.cords.x + 25, window.cords.y + 23, "Ок", ConsoleColor.Green);
                        break;
                    case 2:
                        Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 5), new Point(3, 40), ConsoleColor.Green);
                        break;
                    case 3:
                        Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 10), new Point(3, 20), ConsoleColor.Green);
                        break;
                    case 4:
                        Drawer.Create_Pic(new Point(window.cords.x + 5, window.cords.y + 15), new Point(3, 20), ConsoleColor.Green);
                        break;
                }

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                if (consoleKeyInfo.Key == ConsoleKey.Tab)
                    counter_tab = (counter_tab + 1) % 5;


                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    switch (counter_tab)
                    {
                        case 0:
                            return;
                        case 1:
                            patient = patient_2;
                            return;
                        case 2:
                            Console.SetCursorPosition(window.cords.x + 6, window.cords.y + 5);
                            for (int i = 0; i < 35; i++)
                                Console.Write(' ');
                            Console.SetCursorPosition(window.cords.x + 6, window.cords.y + 5);
                            if (!Action_Text.Is_Scan_Full_Name(ref str))
                            {
                                is_was_mistake = true;
                                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 7, "Введено некорректное ФИО!", ConsoleColor.Red);
                                Console.ReadKey();
                            }
                            else
                            {
                                patient_2.name = str;
                                is_was_mistake = false;
                            }
                            break;
                        case 3:
                            Console.SetCursorPosition(window.cords.x + 6, window.cords.y + 10);
                            for (int i = 0; i < 15; i++)
                                Console.Write(' ');
                            Console.SetCursorPosition(window.cords.x + 6, window.cords.y + 10);

                            if (!Action_Text.Is_scan_Time(ref str))
                            {
                                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 12, "Некорректные данные!", ConsoleColor.Red);
                                Console.ReadKey();
                                is_was_mistake = true;
                            }
                            else
                            {
                                patient_2.time = str;
                                is_was_mistake = false;
                            }
                            break;
                        case 4:
                            Console.SetCursorPosition(window.cords.x + 6, window.cords.y + 15);
                            for (int i = 0; i < 15; i++)
                                Console.Write(' ');
                            Console.SetCursorPosition(window.cords.x + 6, window.cords.y + 15);
                            if (!Action_Text.Is_scan_Time(ref str))
                            {
                                Drawer.Print_Inscription(window.cords.x + 5, window.cords.y + 17, "Некорректные данные!", ConsoleColor.Red);
                                Console.ReadKey();
                                is_was_mistake = true;
                            }
                            else
                            {
                                bool is_empty = true;
                                foreach (var t in Сhoice_Day(all_info.all_doctors[all_info.window.index_doctor].patients, day))
                                {
                                    if (!((Time_To_Int(patient.time) < Time_To_Int(t.time) && Time_To_Int(patient.time) < Time_To_Int(t.time_2) &&
                                        Time_To_Int(patient.time_2) < Time_To_Int(t.time) && Time_To_Int(patient.time_2) < Time_To_Int(t.time_2)) ||
                                        (Time_To_Int(patient.time) > Time_To_Int(t.time) && Time_To_Int(patient.time) > Time_To_Int(t.time_2) &&
                                        Time_To_Int(patient.time_2) > Time_To_Int(t.time) && Time_To_Int(patient.time_2) > Time_To_Int(t.time_2))))
                                    {
                                        Drawer.Print_Inscription(all_info.window.cords.x + 5, all_info.window.cords.y + 17, "Время занято!", ConsoleColor.Red);
                                        Console.ReadKey();
                                        is_was_mistake = true;
                                        is_empty = false;
                                        counter_tab--;
                                        break;
                                    }
                                }
                                if (is_empty)
                                {
                                    counter_tab++;
                                    is_was_mistake = false;
                                }
                                patient_2.time_2 = str;
                            }
                            break;
                    }
                }
            }
        }
    }
}