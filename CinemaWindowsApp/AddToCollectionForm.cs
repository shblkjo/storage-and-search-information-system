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
    public partial class AddToCollectionForm : Form
    {
        private Data.Database db = new Data.Database();
        private int movieId;
        private int userId;
        public AddToCollectionForm(int movieId, int userId)
        {
            InitializeComponent();
            this.movieId = movieId;
            this.userId = userId;
            LoadUserCollections();
        }

        private void LoadUserCollections()
        {
            try
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT c.id, c.title, 
                                          CASE WHEN EXISTS (
                                              SELECT 1 FROM cinema.collection_movies 
                                              WHERE collection_id = c.id AND movie_id = @movieId
                                          ) THEN true ELSE false END as already_added
                                   FROM cinema.collections c
                                   WHERE c.user_id = @userId
                                   ORDER BY c.title";

                    var adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@movieId", movieId);
                    adapter.SelectCommand.Parameters.AddWithValue("@userId", userId);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    dgvCollections.DataSource = dt;

                    dgvCollections.Columns["id"].Visible = false;
                    dgvCollections.Columns["title"].HeaderText = "Мои подборки";
                    dgvCollections.Columns["title"].Width = 250;
                    dgvCollections.Columns["already_added"].HeaderText = "Добавлен";
                    dgvCollections.Columns["already_added"].Width = 70;

                    lblCollectionsCount.Text = $"Мои подборки: {dgvCollections.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки подборок: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddToSelected_Click(object sender, EventArgs e)
        {
            if (dgvCollections.CurrentRow == null)
            {
                MessageBox.Show("Выберите подборку", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int collectionId = Convert.ToInt32(dgvCollections.CurrentRow.Cells["id"].Value);
            string collectionTitle = dgvCollections.CurrentRow.Cells["title"].Value.ToString();

            try
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = @"INSERT INTO cinema.collection_movies 
                                  (collection_id, movie_id) 
                                  VALUES (@collectionId, @movieId)
                                  ON CONFLICT (collection_id, movie_id) DO NOTHING";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@collectionId", collectionId);
                        cmd.Parameters.AddWithValue("@movieId", movieId);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show($"Фильм добавлен в подборку '{collectionTitle}'", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Фильм уже есть в этой подборке", "Информация",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        LoadUserCollections();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCreateNew_Click(object sender, EventArgs e)
        {
            CreateCollectionForm createForm = new CreateCollectionForm(userId);
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                LoadUserCollections();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
