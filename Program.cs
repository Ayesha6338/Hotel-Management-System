using System;
using System.Windows.Forms;
using HotelManagementSystem.Database;
using HotelManagementSystem.Forms;

namespace HotelManagementSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Database aur saari tables initialize karo
            DatabaseInitializer.Initialize();

            Application.Run(new LoginForm());
        }
    }
}