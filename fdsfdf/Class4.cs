using System;
using System.Collections.Generic;

namespace Polymorphism
{
    internal static class Drawer
    {
        internal static void Print_Inscription(int x, int y, string s, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(s);
            Console.ForegroundColor = ConsoleColor.White;
        }
        internal static void Print_gor_line(Point cords, int w, string s)
        {
            Console.SetCursorPosition(cords.x, cords.y);
            for (int i = 0; i < w; i++)
            {
                Console.Write(s);
                Console.SetCursorPosition(cords.x + i, cords.y);
            }
        }

        internal static void Print_vert_line(Point cords, int h, string s)
        {
            Console.SetCursorPosition(cords.x, cords.y);
            for (int i = 0; i < h; i++)
            {
                Console.Write(s);
                Console.SetCursorPosition(cords.x, cords.y + i);
            }
        }

        internal static void Create_Pic(Point cords, Point size, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            View view = new View(cords.x, cords.y, size.x, size.y);
            Print_gor_line(new Point(view.cords.x - 1, view.cords.y - 1), view.size.y + 4, "_");
            Print_gor_line(new Point(view.cords.x - 1, view.cords.y + view.size.x - 2), view.size.y + 4, "_");
            Print_vert_line(new Point(view.cords.x - 1, view.cords.y), view.size.x, "|");
            Print_vert_line(new Point(view.cords.x + view.size.y + 1, view.cords.y), view.size.x, "|");
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void Clear_Char()
        {
            Drawer.Print_Inscription(10 - 1, 10, " ");
        }
    }
}