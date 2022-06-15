
namespace Polymorphism
{
    internal class View
    {
        internal Point cords;
        internal Point size;
        public View(int x, int y, int w, int h)
        {
            cords = new Point(x, y);
            size = new Point(w, h);
        }

    }
}