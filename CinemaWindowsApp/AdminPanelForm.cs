using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaWindowsApp
{
    public partial class AdminPanelForm : Form
    {
        private Data.Database db;
        private DataGridView currentDataGridView;

        public AdminPanelForm()
        {
            InitializeComponent();
            db = new Data.Database();
            LoadDataForCurrentTab();
        }

        private void AdminPanelForm_Load(object sender, EventArgs e)
        {
            LoadDataForCurrentTab();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataForCurrentTab();
        }

        private void LoadDataForCurrentTab()
        {
            string query = "";

            switch (tabControl1.SelectedIndex)
            {
                case 0: // Фильмы
                    query = "SELECT * FROM cinema.movies ORDER BY id";
                    currentDataGridView = dataGridViewMovies;
                    break;
                case 1: // Персоны
                    query = "SELECT * FROM cinema.people ORDER BY id";
                    currentDataGridView = dataGridViewPeople;
                    break;
                case 2: // Студии
                    query = "SELECT * FROM cinema.studios ORDER BY id";
                    currentDataGridView = dataGridViewStudios;
                    break;
                case 3: // Страны
                    query = "SELECT * FROM cinema.countries ORDER BY id";
                    currentDataGridView = dataGridViewCountries;
                    break;
                case 4: // Жанры
                    query = "SELECT * FROM cinema.genres ORDER BY id";
                    currentDataGridView = dataGridViewGenres;
                    break;
                case 5: // Пользователи
                    query = "SELECT * FROM cinema.users ORDER BY id";
                    currentDataGridView = dataGridViewUsers;
                    break;
                case 6: // Коллекции
                    query = "SELECT * FROM cinema.collections ORDER BY id";
                    currentDataGridView = dataGridViewCollections;
                    break;
            }

            try
            {
                DataTable dt = db.ExecuteQuery(query);
                currentDataGridView.DataSource = dt;
                currentDataGridView.ReadOnly = true;
                currentDataGridView.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message, "Ошибка");
            }
        }

        // === КНОПКИ ДЛЯ ФИЛЬМОВ ===
        private void buttonAddMovie_Click(object sender, EventArgs e)
        {
            MovieForm form = new MovieForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDataForCurrentTab();
            }
        }

        private void buttonEditMovie_Click(object sender, EventArgs e)
        {
            if (dataGridViewMovies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите фильм для редактирования");
                return;
            }

            int id = Convert.ToInt32(dataGridViewMovies.SelectedRows[0].Cells["id"].Value);
            MovieForm form = new MovieForm(id);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDataForCurrentTab();
            }
        }

        private void buttonDeleteMovie_Click(object sender, EventArgs e)
        {
            if (dataGridViewMovies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите фильм для удаления");
                return;
            }

            if (MessageBox.Show("Удалить выбранный фильм?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridViewMovies.SelectedRows[0].Cells["id"].Value);
                    string query = "DELETE FROM cinema.movies WHERE id = @id";
                    NpgsqlParameter param = new NpgsqlParameter("@id", id);
                    db.ExecuteNonQuery(query, param);
                    LoadDataForCurrentTab();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        // === КНОПКИ ДЛЯ ПЕРСОН ===
        private void buttonAddPerson_Click(object sender, EventArgs e)
        {
            PersonForm form = new PersonForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDataForCurrentTab();
            }
        }

        private void buttonEditPerson_Click(object sender, EventArgs e)
        {
            if (dataGridViewPeople.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите персону для редактирования");
                return;
            }

            int id = Convert.ToInt32(dataGridViewPeople.SelectedRows[0].Cells["id"].Value);
            PersonForm form = new PersonForm(id);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDataForCurrentTab();
            }
        }

        private void buttonDeletePerson_Click(object sender, EventArgs e)
        {
            if (dataGridViewPeople.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите персону для удаления");
                return;
            }

            if (MessageBox.Show("Удалить выбранную персону?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridViewPeople.SelectedRows[0].Cells["id"].Value);
                    string query = "DELETE FROM cinema.people WHERE id = @id";
                    NpgsqlParameter param = new NpgsqlParameter("@id", id);
                    db.ExecuteNonQuery(query, param);
                    LoadDataForCurrentTab();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        // === КНОПКИ ДЛЯ ПОЛЬЗОВАТЕЛЕЙ ===
        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для удаления");
                return;
            }

            if (MessageBox.Show("Удалить выбранного пользователя?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["id"].Value);
                    string query = "DELETE FROM cinema.users WHERE id = @id";
                    NpgsqlParameter param = new NpgsqlParameter("@id", id);
                    db.ExecuteNonQuery(query, param);
                    LoadDataForCurrentTab();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        // === КНОПКИ ДЛЯ КОЛЛЕКЦИЙ ===
        private void buttonDeleteCollection_Click(object sender, EventArgs e)
        {
            if (dataGridViewCollections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите коллекцию для удаления");
                return;
            }

            if (MessageBox.Show("Удалить выбранную коллекцию?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridViewCollections.SelectedRows[0].Cells["id"].Value);
                    string query = "DELETE FROM cinema.collections WHERE id = @id";
                    NpgsqlParameter param = new NpgsqlParameter("@id", id);
                    db.ExecuteNonQuery(query, param);
                    LoadDataForCurrentTab();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
