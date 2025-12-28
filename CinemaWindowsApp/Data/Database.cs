using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

        public DataTable ExecuteQuery(string sqlQuery, params NpgsqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

            return dataTable;
        }

        public int ExecuteNonQuery(string sqlQuery, params NpgsqlParameter[] parameters)
        {
            int rowsAffected = 0;

                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }


            return rowsAffected;
        }

        public object ExecuteScalar(string sqlQuery, params NpgsqlParameter[] parameters)
        {
            object result = null;

                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        result = cmd.ExecuteScalar();
                    }
                }

            return result;
        }
    }
}
