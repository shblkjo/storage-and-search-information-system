using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace CinemaWindowsApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            txtConfirmPassword.PasswordChar = '*';
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

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            // Проверка ввода
            if (string.IsNullOrEmpty(txtUsername.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Заполните все обязательные поля", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string hashedPassword = HashPassword(txtPassword.Text);

                using (var conn = new NpgsqlConnection("Host=localhost; Database=film_archive; User Id = SuperUser; Password = 1234; "))
                {
                    conn.Open();
                    string sql = @"INSERT INTO cinema.users 
                                  (username, email, password_hash, birth_date, is_admin)
                                  VALUES (@username, @email, @password, @birthdate, false)";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);

                        if (DateTime.TryParse(txtBirthDate.Text, out DateTime birthDate))
                            cmd.Parameters.AddWithValue("@birthdate", birthDate);
                        else
                            cmd.Parameters.AddWithValue("@birthdate", DBNull.Value);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Регистрация успешна! Теперь вы можете войти.", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                MessageBox.Show("Пользователь с таким именем или email уже существует", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}