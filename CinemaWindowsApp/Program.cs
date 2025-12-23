using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaWindowsApp
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Создаем и показываем форму входа
            LoginForm loginForm = new LoginForm();

            // Показываем форму входа как диалог
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Если вход успешен, запускаем главную форму
                Application.Run(new MainForm());
            }
            else
            {
                // Если пользователь отменил вход
                Application.Exit();
            }
        }
    }
}
