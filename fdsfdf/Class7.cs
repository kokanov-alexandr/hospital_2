using System;
using System.Collections.Generic;

namespace Polymorphism
{
    internal struct Point
    {
        internal int x, y;
        internal Point(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }
    }

    internal struct Patient
    {
        internal string time;
        internal string time_2;
        internal string name;
        internal int day_of_the_week;
        internal Patient(string hour, string minuts, string name, int day_of_the_week)
        {
            time = hour;
            time_2 = minuts;
            this.name = name;
            this.day_of_the_week = day_of_the_week;
        }
    }
    internal struct Doctor
    {
        internal string Full_name;
        internal string Mail;
        internal string Password;
        internal Header Windows_Header;
        internal List<Patient> patients;
        internal Doctor(string Full_name, string Mail, string Password)
        {
            this.Full_name = Full_name;
            this.Mail = Mail;
            this.Password = Password;
            patients = new List<Patient>();
            Windows_Header = new Header("");
        }
    }
}