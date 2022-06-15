using System;

namespace Polymorphism
{
    internal class Window : View
    {
        internal int index_doctor;
        internal int counter_tab;
        internal Window(int x, int y, int w, int h, int index_user = -1) : base(x, y, w, h)
        {
            counter_tab = 1;
            this.index_doctor = index_user;
        }
    }
}