using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace CinemaWindowsApp.Data
{
    internal class Database
    {
        private string connectionString = "Host=localhost; Database=film_archive; User Id = SuperUser; Password = 1234; ";

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    MessageBox.Show("Подключение к базе данных успешно!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
