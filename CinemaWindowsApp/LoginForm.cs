using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaWindowsApp
{
    public partial class LoginForm : Form
    {
        public static User CurrentUser { get; set; }

        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public bool IsAdmin { get; set; }
        }

        public LoginForm()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите имя пользователя и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hashedPassword = HashPassword(password);

            try
            {
                using (var conn = new NpgsqlConnection("Host=localhost; Database=film_archive; User Id = SuperUser; Password = 1234;"))
                {
                    conn.Open();
                    string sql = @"SELECT id, username, email, is_admin 
                                  FROM cinema.users 
                                  WHERE username = @username AND password_hash = @password";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CurrentUser = new User
                                {
                                    Id = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    IsAdmin = reader.GetBoolean(3)
                                };

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            //RegisterForm registerForm = new RegisterForm();
            //registerForm.ShowDialog();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
