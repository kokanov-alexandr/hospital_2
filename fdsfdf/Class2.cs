using System;
using System.Collections.Generic;

namespace Polymorphism
{
    internal class All_Info
    {
        internal Window window;
        internal List<Doctor> all_doctors;

        internal All_Info(int x, int y, int w, int h)
        {
            window = new Window(x, y, w, h);
            all_doctors = new List<Doctor>();
        }
    }
}