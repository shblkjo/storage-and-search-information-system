using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaWindowsApp
{
    public partial class MovieDetailsForm : Form
    {
        private int movieId;
        private int userId;
        private PictureBox posterPictureBox;

        public MovieDetailsForm()
        {
            InitializeComponent();
        }

        public void LoadMovieDetails(NpgsqlDataReader reader, int userId = 0)
        {
            this.userId = userId;
            this.movieId = Convert.ToInt32(reader["id"]);

            try
            {
                // Заголовок формы
                this.Text = reader["title"].ToString();

                // Основная информация
                lblTitle.Text = reader["title"].ToString();
                lblYear.Text = $"Год выпуска: {reader["release_year"]}";

                if (reader["rating"] != DBNull.Value)
                    lblRating.Text = $"Рейтинг: {reader["rating"]:0.0}/10";
                else
                    lblRating.Text = "Рейтинг: не указан";

                if (reader["age_rating"] != DBNull.Value)
                    lblAgeRating.Text = $"Возрастной рейтинг: {reader["age_rating"]}";
                else
                    lblAgeRating.Text = "Возрастной рейтинг: не указан";

                if (reader["duration"] != DBNull.Value)
                    lblDuration.Text = $"Продолжительность: {reader["duration"]} мин.";
                else
                    lblDuration.Text = "Продолжительность: не указана";

                if (reader["country_name"] != DBNull.Value)
                    lblCountry.Text = $"Страна: {reader["country_name"]}";
                else
                    lblCountry.Text = "Страна: не указана";

                if (reader["studio_name"] != DBNull.Value)
                    lblStudio.Text = $"Студия: {reader["studio_name"]}";
                else
                    lblStudio.Text = "Студия: не указана";

                if (reader["genres"] != DBNull.Value)
                    lblGenres.Text = $"Жанры: {reader["genres"]}";
                else
                    lblGenres.Text = "Жанры: не указаны";

                // Актеры и режиссеры
                if (reader["actors"] != DBNull.Value)
                    lblActors.Text = $"В ролях: {reader["actors"]}";
                else
                    lblActors.Text = "Актеры: не указаны";

                if (reader["directors"] != DBNull.Value)
                    lblDirectors.Text = $"Режиссеры: {reader["directors"]}";
                else
                    lblDirectors.Text = "Режиссеры: не указаны";

                // Описание
                if (reader["description"] != DBNull.Value)
                    txtDescription.Text = reader["description"].ToString();
                else
                    txtDescription.Text = "Описание отсутствует";

                // Тип фильма
                if (reader["type"] != DBNull.Value)
                    lblType.Text = $"Тип: {reader["type"]}";
                else
                    lblType.Text = "Тип: не указан";

                // Загружаем постер
                LoadMoviePoster();

                // Добавляем кнопку "Добавить в подборку" если пользователь авторизован
                if (userId > 0)
                {
                    AddCollectionButton();
                }

                // Добавляем кнопку для админа (загрузка постера)
                if (LoginForm.CurrentUser != null && LoginForm.CurrentUser.IsAdmin)
                {
                    AddAdminPosterButton();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки деталей: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMoviePoster()
        {
            try
            {
                // Пробуем загрузить из базы данных
                byte[] imageData = GetPosterFromDatabase();

                if (imageData != null && imageData.Length > 0)
                {
                    DisplayPosterFromBytes(imageData);
                }
                else
                {
                    // Если в базе нет, показываем заглушку
                    DisplayDefaultPoster();
                }
            }
            catch (Exception)
            {
                // Если ошибка, показываем заглушку
                DisplayDefaultPoster();
            }
        }

        private byte[] GetPosterFromDatabase()
        {
            try
            {
                using (var conn = new Data.Database().GetConnection())
                {
                    conn.Open();

                    // Получаем последний загруженный постер для фильма
                    string sql = @"SELECT image_data 
                                  FROM cinema.movie_images 
                                  WHERE movie_id = @movieId 
                                  ORDER BY upload_date DESC 
                                  LIMIT 1";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@movieId", movieId);

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return (byte[])result;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Игнорируем ошибку, вернем null
            }

            return null;
        }

        private void DisplayPosterFromBytes(byte[] imageData)
        {
            try
            {
                // Создаем PictureBox если еще не создан
                if (posterPictureBox == null)
                {
                    posterPictureBox = new PictureBox();
                    posterPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    posterPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    posterPictureBox.Size = new Size(250, 350);
                    posterPictureBox.Location = new Point(20, 50);
                    this.Controls.Add(posterPictureBox);
                }

                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Image poster = Image.FromStream(ms);
                    posterPictureBox.Image = poster;
                }
            }
            catch (Exception)
            {
                DisplayDefaultPoster();
            }
        }

        private void DisplayDefaultPoster()
        {
            // Создаем PictureBox если еще не создан
            if (posterPictureBox == null)
            {
                posterPictureBox = new PictureBox();
                posterPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                posterPictureBox.BorderStyle = BorderStyle.FixedSingle;
                posterPictureBox.Size = new Size(250, 350);
                posterPictureBox.Location = new Point(20, 50);
                this.Controls.Add(posterPictureBox);
            }

            // Простая заглушка
            posterPictureBox.Image = null;
            posterPictureBox.BackColor = Color.LightGray;
        }

        private void AddCollectionButton()
        {
            Button btnAddToCollection = new Button();
            btnAddToCollection.Text = "Добавить в подборку";
            btnAddToCollection.Size = new Size(150, 30);
            btnAddToCollection.Location = new Point(this.Width - 170, 20);
            btnAddToCollection.Click += BtnAddToCollection_Click;
            this.Controls.Add(btnAddToCollection);
        }

        private void AddAdminPosterButton()
        {
            Button btnUploadPoster = new Button();
            btnUploadPoster.Text = "Загрузить постер";
            btnUploadPoster.Size = new Size(150, 30);
            btnUploadPoster.Location = new Point(this.Width - 170, 60);
            btnUploadPoster.Click += BtnUploadPoster_Click;
            this.Controls.Add(btnUploadPoster);
        }

        private void BtnAddToCollection_Click(object sender, EventArgs e)
        {
            //AddToCollectionForm addForm = new AddToCollectionForm(movieId, userId);
            //addForm.ShowDialog();
        }

        private void BtnUploadPoster_Click(object sender, EventArgs e)
        {
            //UploadPosterForm uploadForm = new UploadPosterForm(movieId);
            //if (uploadForm.ShowDialog() == DialogResult.OK)
            //{
            //    // Перезагружаем постер
            //    LoadMoviePoster();
            //    MessageBox.Show("Постер успешно загружен", "Успех",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
