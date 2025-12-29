using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaWindowsApp
{
    public partial class MovieForm : Form
    {
        private Data.Database db;
        private int? movieId;
        private Dictionary<string, string> typeMap;
        private DataTable countriesDt; // Храним данные для ComboBox
        private DataTable studiosDt;

        public MovieForm(int? id = null)
        {
            movieId = id;
            db = new Data.Database();
            InitializeComponent();

            // Словарь для преобразования типов (русский → английский)
            typeMap = new Dictionary<string, string>
            {
                { "Фильм", "film" },
                { "Сериал", "series" },
                { "Анимация", "animation" },
                { "Документальный", "documentary" },
                { "Короткометражный", "short" }
            };
        }

        private void MovieForm_Load(object sender, EventArgs e)
        {
            // Настройка NumericUpDown
            // Год выпуска: от 1800 до 2100
            numYear.Minimum = 1800;
            numYear.Maximum = 2100;
            numYear.Value = DateTime.Now.Year;

            // Продолжительность: от 1 минуты до 1000
            numDuration.Minimum = 1;
            numDuration.Maximum = 1000;

            // Рейтинг: от 0 до 10
            numRating.Minimum = 0;
            numRating.Maximum = 10;
            numRating.DecimalPlaces = 1;
            numRating.Increment = 0.1m;


            // Сначала заполняем статические списки
            FillStaticComboBoxes();

            // Затем загружаем данные из базы
            LoadDataFromDatabase();

            // Загружаем данные фильма, если редактируем
            if (movieId.HasValue)
            {
                LoadMovie();
            }
        }

        // Метод для заполнения статических выпадающих списков
        private void FillStaticComboBoxes()
        {
            // Типы фильмов
            cmbType.Items.Clear();
            cmbType.Items.AddRange(new string[] { "Фильм", "Сериал", "Анимация", "Документальный", "Короткометражный" });

            // Возрастные рейтинги
            cmbAgeRating.Items.Clear();
            cmbAgeRating.Items.AddRange(new string[] { "0+", "6+", "12+", "16+", "18+" });


        }

        // Метод для загрузки данных из базы данных
        private void LoadDataFromDatabase()
        {
            try
            {
                // 1. Загружаем страны
                countriesDt = db.ExecuteQuery("SELECT id, name FROM cinema.countries ORDER BY name");

                cmbCountry.Items.Clear();
                cmbCountry.Items.Add("");

                foreach (DataRow row in countriesDt.Rows)
                {
                    cmbCountry.Items.Add(new CountryItem(
                        Convert.ToInt32(row["id"]),
                        row["name"].ToString()
                    ));
                }

                // 2. Загружаем студии
                studiosDt = db.ExecuteQuery("SELECT id, name FROM cinema.studios ORDER BY name");

                cmbStudio.Items.Clear();
                cmbStudio.Items.Add("");

                foreach (DataRow row in studiosDt.Rows)
                {
                    cmbStudio.Items.Add(new StudioItem(
                        Convert.ToInt32(row["id"]),
                        row["name"].ToString()
                    ));
                }

                // 3. Жанры (из базы данных)
                DataTable genres = db.ExecuteQuery("SELECT id, name FROM cinema.genres ORDER BY name");
                chkGenres.Items.Clear();
                foreach (DataRow row in genres.Rows)
                {
                    chkGenres.Items.Add(new GenreItem(
                        Convert.ToInt32(row["id"]),
                        row["name"].ToString()
                    ), false);
                }
                // Загружаем всех людей из таблицы people
                DataTable people = db.ExecuteQuery(
                    "SELECT id, first_name, last_name FROM cinema.people ORDER BY last_name, first_name"
                );

                chkDirectors.Items.Clear();
                chkActors.Items.Clear();

                foreach (DataRow row in people.Rows)
                {
                    int personId = Convert.ToInt32(row["id"]);
                    string fullName = $"{row["last_name"]} {row["first_name"]}";

                    // Создаем объект PersonItem для каждого списка
                    PersonItem directorItem = new PersonItem(personId, fullName, "Режиссер");
                    PersonItem actorItem = new PersonItem(personId, fullName, "Актер");

                    chkDirectors.Items.Add(directorItem, false);
                    chkActors.Items.Add(actorItem, false);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для загрузки данных фильма
        private void LoadMovie()
        {
            try
            {
                // 1. Получаем данные фильма
                string query = "SELECT * FROM cinema.movies WHERE id = @id";
                NpgsqlParameter param = new NpgsqlParameter("@id", movieId.Value);
                DataTable movie = db.ExecuteQuery(query, param);

                if (movie.Rows.Count == 0)
                {
                    MessageBox.Show("Фильм не найден");
                    this.Close();
                    return;
                }

                DataRow row = movie.Rows[0];

                // 2. Заполняем поля
                txtTitle.Text = row["title"].ToString();
                numYear.Value = Convert.ToInt32(row["release_year"]);

                // Продолжительность
                if (row["duration"] != DBNull.Value)
                    numDuration.Value = Convert.ToInt32(row["duration"]);

                // Тип
                string dbType = row["type"]?.ToString();
                if (!string.IsNullOrEmpty(dbType))
                {
                    // Ищем русское название
                    foreach (var kvp in typeMap)
                    {
                        if (kvp.Value == dbType)
                        {
                            for (int i = 0; i < cmbType.Items.Count; i++)
                            {
                                if (cmbType.Items[i].ToString() == kvp.Key)
                                {
                                    cmbType.SelectedIndex = i;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                // Страна
                if (row["country_id"] != DBNull.Value)
                {
                    int countryId = Convert.ToInt32(row["country_id"]);
                    for (int i = 0; i < cmbCountry.Items.Count; i++)
                    {
                        if (cmbCountry.Items[i] is CountryItem countryItem && countryItem.Id == countryId)
                        {
                            cmbCountry.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Студия
                if (row["studio_id"] != DBNull.Value)
                {
                    int studioId = Convert.ToInt32(row["studio_id"]);
                    for (int i = 0; i < cmbStudio.Items.Count; i++)
                    {
                        if (cmbStudio.Items[i] is StudioItem studioItem && studioItem.Id == studioId)
                        {
                            cmbStudio.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Рейтинг
                if (row["rating"] != DBNull.Value)
                    numRating.Value = Convert.ToDecimal(row["rating"]);

                // Возрастной рейтинг
                if (row["age_rating"] != DBNull.Value)
                {
                    string ageRating = row["age_rating"].ToString();
                    for (int i = 0; i < cmbAgeRating.Items.Count; i++)
                    {
                        if (cmbAgeRating.Items[i].ToString() == ageRating)
                        {
                            cmbAgeRating.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Описание
                if (row["description"] != DBNull.Value)
                    txtDescription.Text = row["description"].ToString();

                // 3. Загружаем жанры фильма
                LoadMovieGenres();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фильма: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для загрузки жанров фильма
        private void LoadMovieGenres()
        {
            try
            {
                string query = "SELECT genre_id FROM cinema.movie_genres WHERE movie_id = @id";
                NpgsqlParameter param = new NpgsqlParameter("@id", movieId.Value);
                DataTable genres = db.ExecuteQuery(query, param);

                // Отмечаем жанры
                foreach (DataRow row in genres.Rows)
                {
                    int genreId = Convert.ToInt32(row["genre_id"]);

                    for (int i = 0; i < chkGenres.Items.Count; i++)
                    {
                        var item = (GenreItem)chkGenres.Items[i];
                        if (item.Id == genreId)
                        {
                            chkGenres.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки жанров фильма: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // 1. Проверка
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название фильма");
                return;
            }

            if (cmbType.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тип фильма");
                return;
            }

            try
            {
                // 2. Подготавливаем данные
                string type = "";
                if (cmbType.SelectedItem != null)
                {
                    string russianType = cmbType.SelectedItem.ToString();
                    if (typeMap.ContainsKey(russianType))
                    {
                        type = typeMap[russianType];
                    }
                }

                // 3. Параметры для SQL
                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>
                {
                    new NpgsqlParameter("@title", txtTitle.Text.Trim()),
                    new NpgsqlParameter("@release_year", (int)numYear.Value),
                    new NpgsqlParameter("@type", type),
                    new NpgsqlParameter("@duration", (int)numDuration.Value),
                    new NpgsqlParameter("@description", txtDescription.Text.Trim())
                };

                // Рейтинг (может быть пустым)
                if (numRating.Value > 0)
                    parameters.Add(new NpgsqlParameter("@rating", (double)numRating.Value));
                else
                    parameters.Add(new NpgsqlParameter("@rating", DBNull.Value));

                // Возрастной рейтинг
                if (cmbAgeRating.SelectedIndex != -1)
                    parameters.Add(new NpgsqlParameter("@age_rating", cmbAgeRating.SelectedItem.ToString()));
                else
                    parameters.Add(new NpgsqlParameter("@age_rating", DBNull.Value));

                // Страна
                if (cmbCountry.SelectedItem is CountryItem selectedCountry && selectedCountry.Id > 0)
                    parameters.Add(new NpgsqlParameter("@country_id", selectedCountry.Id));
                else
                    parameters.Add(new NpgsqlParameter("@country_id", DBNull.Value));

                // Студия
                if (cmbStudio.SelectedItem is StudioItem selectedStudio && selectedStudio.Id > 0)
                    parameters.Add(new NpgsqlParameter("@studio_id", selectedStudio.Id));
                else
                    parameters.Add(new NpgsqlParameter("@studio_id", DBNull.Value));

                // 4. SQL запрос
                string query;
                if (movieId.HasValue)
                {
                    // Обновление
                    query = @"UPDATE cinema.movies 
                                 SET title = @title, release_year = @release_year, 
                                     type = @type, duration = @duration, description = @description,
                                     rating = @rating, age_rating = @age_rating,
                                     country_id = @country_id, studio_id = @studio_id
                                 WHERE id = @id";
                    parameters.Add(new NpgsqlParameter("@id", movieId.Value));
                }
                else
                {
                    // Добавление
                    query = @"INSERT INTO cinema.movies 
                                 (title, release_year, type, duration, description,
                                  rating, age_rating, country_id, studio_id)
                                 VALUES (@title, @release_year, @type, @duration, @description,
                                         @rating, @age_rating, @country_id, @studio_id)";
                }

                // 5. Выполняем запрос
                db.ExecuteNonQuery(query, parameters.ToArray());

                // 6. Если это новый фильм, получаем его ID
                if (!movieId.HasValue)
                {
                    DataTable lastId = db.ExecuteQuery("SELECT MAX(id) FROM cinema.movies");
                    if (lastId.Rows.Count > 0 && lastId.Rows[0][0] != DBNull.Value)
                        movieId = Convert.ToInt32(lastId.Rows[0][0]);
                }

                // 7. Сохраняем жанры
                SaveGenres();

                // 8. Сохраняем людей
                SaveMoviePeople();

                // 9. Закрываем форму
                MessageBox.Show("Фильм сохранен");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (PostgresException pgEx)
            {
                MessageBox.Show($"Ошибка: {pgEx.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для сохранения жанров
        private void SaveGenres()
        {
            try
            {
                // Удаляем старые жанры
                string deleteQuery = "DELETE FROM cinema.movie_genres WHERE movie_id = @movie_id";
                db.ExecuteNonQuery(deleteQuery, new NpgsqlParameter("@movie_id", movieId.Value));

                // Добавляем новые жанры
                foreach (var checkedItem in chkGenres.CheckedItems)
                {
                    GenreItem genre = (GenreItem)checkedItem;
                    string insertQuery = "INSERT INTO cinema.movie_genres (movie_id, genre_id) VALUES (@movie_id, @genre_id)";
                    NpgsqlParameter[] parameters =
                    {
                        new NpgsqlParameter("@movie_id", movieId.Value),
                        new NpgsqlParameter("@genre_id", genre.Id)
                    };
                    db.ExecuteNonQuery(insertQuery, parameters);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения жанров: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    

        // Метод для сохранения режиссеров и актеров
        private void SaveMoviePeople()
        {
            try
            {
                // Удаляем старые связи
                string deleteQuery = "DELETE FROM cinema.movie_people WHERE movie_id = @movie_id";
                db.ExecuteNonQuery(deleteQuery, new NpgsqlParameter("@movie_id", movieId.Value));

                // Сохраняем режиссеров
                foreach (var checkedItem in chkDirectors.CheckedItems)
                {
                    PersonItem person = (PersonItem)checkedItem;
                    string insertQuery = @"INSERT INTO cinema.movie_people 
                                          (movie_id, person_id, role_type) 
                                          VALUES (@movie_id, @person_id, @role_type)";
                    NpgsqlParameter[] parameters =
                    {
                        new NpgsqlParameter("@movie_id", movieId.Value),
                        new NpgsqlParameter("@person_id", person.Id),
                        new NpgsqlParameter("@role_type", "Режиссер")
                    };
                    db.ExecuteNonQuery(insertQuery, parameters);
                }

                // Сохраняем актеров
                foreach (var checkedItem in chkActors.CheckedItems)
                {
                    PersonItem person = (PersonItem)checkedItem;
                    string insertQuery = @"INSERT INTO cinema.movie_people 
                                          (movie_id, person_id, role_type) 
                                          VALUES (@movie_id, @person_id, @role_type)";
                    NpgsqlParameter[] parameters =
                    {
                        new NpgsqlParameter("@movie_id", movieId.Value),
                        new NpgsqlParameter("@person_id", person.Id),
                        new NpgsqlParameter("@role_type", "Актер")
                    };
                    db.ExecuteNonQuery(insertQuery, parameters);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения персон: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Класс для хранения жанра
    public class GenreItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GenreItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    // Класс для хранения страны
    public class CountryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CountryItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    // Класс для хранения студии
    public class StudioItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StudioItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    // Класс для хранения персоны
    public class PersonItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public PersonItem(int id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}