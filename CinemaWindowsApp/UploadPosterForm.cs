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
    public partial class UploadPosterForm : Form
    {
        private Data.Database db = new Data.Database();
        private int movieId;
        private byte[] imageData;
        private string fileName;

        public UploadPosterForm(int movieId)
        {
            InitializeComponent();
            this.movieId = movieId;
            this.Text = $"Загрузка постера для фильма ID: {movieId}";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        txtFilePath.Text = filePath;
                        fileName = Path.GetFileName(filePath);

                        // Читаем файл в byte[]
                        imageData = File.ReadAllBytes(filePath);

                        lblStatus.Text = $"Выбран файл: {fileName} ({imageData.Length} байт)";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки файла: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (imageData == null || imageData.Length == 0)
            {
                MessageBox.Show("Сначала выберите файл изображения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("Не удалось определить имя файла", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();

                    // Вставляем новый постер
                    string sql = @"INSERT INTO cinema.movie_images 
                                  (movie_id, file_name, image_data)
                                  VALUES (@movieId, @fileName, @imageData)";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@movieId", movieId);
                        cmd.Parameters.AddWithValue("@fileName", fileName);
                        cmd.Parameters.AddWithValue("@imageData", imageData);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Постер успешно загружен в базу данных", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки постера: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
