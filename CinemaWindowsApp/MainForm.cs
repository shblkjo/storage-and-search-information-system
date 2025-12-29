using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace CinemaWindowsApp
{
    public partial class MainForm : Form
    {
        private Data.Database db = new Data.Database();

        public MainForm()
        {
            InitializeComponent();
            LoadUserInfo();
            LoadAllMovies(); // По умолчанию загружаем все фильмы

            // Настраиваем DataGridView
            SetupDataGridView();
        }

        private void LoadUserInfo()
        {
            // Скрываем админские кнопки если не админ
            if (!LoginForm.CurrentUser.IsAdmin)
            {
                buttonAdminPanel.Visible = false;
            }
        }

        private void SetupDataGridView()
        {
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
        }

        // ==================== ЗАГРУЗКА ВСЕХ ФИЛЬМОВ ====================
        private void LoadAllMovies()
        {
            try
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT
                        m.id,
                        m.title as ""Название фильма"",
                        m.release_year as ""Год выпуска"",
                        COALESCE(m.rating, 0) as ""Рейтинг"",
                        COALESCE(m.age_rating, 'Не указан') as ""Возрастной рейтинг"",
                        m.duration as ""Продолжительность (мин)"",
                        c.name as ""Страна""
                     FROM cinema.movies m
                    LEFT JOIN cinema.countries c ON m.country_id = c.id
                    ORDER BY m.title";

                    var adapter = new NpgsqlDataAdapter(sql, conn);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView.DataSource = dt;

                    // Скрываем ID
                    dataGridView.Columns["id"].Visible = false;

                    // Форматируем рейтинг
                    if (dataGridView.Columns.Contains("Рейтинг"))
                    {
                        dataGridView.Columns["Рейтинг"].DefaultCellStyle.Format = "0.0";
                        dataGridView.Columns["Рейтинг"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Центрируем числовые столбцы
                    if (dataGridView.Columns.Contains("Год выпуска"))
                        dataGridView.Columns["Год выпуска"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    if (dataGridView.Columns.Contains("Продолжительность (мин)"))
                        dataGridView.Columns["Продолжительность (мин)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фильмов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== ПОИСК ====================
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadAllMovies();
                return;
            }

            SearchMovies(searchText);
        }

        private void SearchMovies(string searchText)
        {
            try
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();

                    // Ищем только фильмы
                    string sql = @"SELECT 
                        m.id,
                        m.title as ""Название фильма"",
                        m.release_year as ""Год выпуска"",
                        COALESCE(m.rating, 0) as ""Рейтинг"",
                        COALESCE(m.age_rating, 'Не указан') as ""Возрастной рейтинг"",
                        COALESCE(m.duration, 0) as ""Продолжительность (мин)"",
                        COALESCE(c.name, 'Не указана') as ""Страна""
                    FROM cinema.movies m
                    LEFT JOIN cinema.countries c ON m.country_id = c.id
                    WHERE m.title ILIKE @search
                    ORDER BY m.title";

                    var adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@search", $"%{searchText}%");

                    var dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView.DataSource = dt;

                    // Скрываем ID
                    if (dataGridView.Columns.Contains("id"))
                        dataGridView.Columns["id"].Visible = false;

                    // Форматируем рейтинг
                    if (dataGridView.Columns.Contains("Рейтинг"))
                    {
                        dataGridView.Columns["Рейтинг"].DefaultCellStyle.Format = "0.0";
                        dataGridView.Columns["Рейтинг"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    if (dataGridView.Rows.Count == 0)
                    {
                        MessageBox.Show($"По запросу '{searchText}' ничего не найдено", "Результаты поиска",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdminPanel_Click(object sender, EventArgs e)
        {
            if (LoginForm.CurrentUser.IsAdmin)
            {
                AdminPanelForm adminForm = new AdminPanelForm();
                adminForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("У вас нет прав администратора", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ==================== ОТКРЫТИЕ ДЕТАЛЕЙ ====================
        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex < 0) return;

                int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["id"].Value);
                ShowMovieDetails(id);
            }
        }

        private void ShowMovieDetails(int movieId)
        {
            try
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT m.*, 
                                          COALESCE(c.name, 'Не указана') as country_name, 
                                          COALESCE(s.name, 'Не указана') as studio_name,
                                          STRING_AGG(DISTINCT g.name, ', ') as genres,
                                          STRING_AGG(DISTINCT 
                                              CASE WHEN mp.role_type  = 'actor' 
                                              THEN CONCAT(p.first_name, ' ', p.last_name) 
                                              ELSE NULL END, ', ') as actors,
                                          STRING_AGG(DISTINCT 
                                              CASE WHEN mp.role_type  = 'director' 
                                              THEN CONCAT(p.first_name, ' ', p.last_name) 
                                              ELSE NULL END, ', ') as directors
                                   FROM cinema.movies m
                                   LEFT JOIN cinema.countries c ON m.country_id = c.id
                                   LEFT JOIN cinema.studios s ON m.studio_id = s.id
                                   LEFT JOIN cinema.movie_genres mg ON m.id = mg.movie_id
                                   LEFT JOIN cinema.genres g ON mg.genre_id = g.id
                                   LEFT JOIN cinema.movie_people mp ON m.id = mp.movie_id
                                   LEFT JOIN cinema.people p ON mp.person_id = p.id
                                   WHERE m.id = @id
                                   GROUP BY m.id, c.name, s.name";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", movieId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MovieDetailsForm detailsForm = new MovieDetailsForm();
                                detailsForm.LoadMovieDetails(reader, LoginForm.CurrentUser.Id);
                                detailsForm.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки деталей фильма: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}