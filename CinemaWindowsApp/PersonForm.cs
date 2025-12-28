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
    public partial class PersonForm : Form
    {
        private Data.Database db;
        private int? personId;

        public PersonForm(int? id = null)
        {
            personId = id;
            db = new Data.Database();
            InitializeComponent();
        }

        private void PersonForm_Load(object sender, EventArgs e)
        {
            // Загружаем студии
            LoadStudios();

            // Загружаем страны для CheckedListBox
            LoadCountries();

            // Если редактирование, загружаем данные
            if (personId.HasValue)
            {
                LoadPersonData();
            }
        }

        // Метод для загрузки студий
        private void LoadStudios()
        {
            try
            {
                DataTable studios = db.ExecuteQuery("SELECT id, name FROM cinema.studios ORDER BY name");
                cmbStudio.Items.Clear();
                cmbStudio.Items.Add(""); // Пустой элемент

                foreach (DataRow row in studios.Rows)
                {
                    cmbStudio.Items.Add(new StudioItem(
                        Convert.ToInt32(row["id"]),
                        row["name"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фильмов: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для загрузки стран в CheckedListBox
        private void LoadCountries()
        {
            try
            {
                DataTable countries = db.ExecuteQuery("SELECT id, name FROM cinema.countries ORDER BY name");
                chkCountries.Items.Clear();

                foreach (DataRow row in countries.Rows)
                {
                    chkCountries.Items.Add(new CountryItem(
                        Convert.ToInt32(row["id"]),
                        row["name"].ToString()
                    ), false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки стран: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadPersonData()
        {
            try
            {
                // 1. Получаем данные персоны
                string query = "SELECT * FROM cinema.people WHERE id = @id";
                NpgsqlParameter param = new NpgsqlParameter("@id", personId.Value);
                DataTable person = db.ExecuteQuery(query, param);

                if (person.Rows.Count == 0)
                {
                    MessageBox.Show("Персона не найдена");
                    this.Close();
                    return;
                }

                DataRow row = person.Rows[0];

                // 2. Заполняем основные поля
                txtFirstName.Text = row["first_name"].ToString();
                txtLastName.Text = row["last_name"].ToString();

                if (row["middle_name"] != DBNull.Value)
                    txtMiddleName.Text = row["middle_name"].ToString();

                if (row["birth_date"] != DBNull.Value)
                {
                    dtpBirthDate.Value = Convert.ToDateTime(row["birth_date"]);
                    dtpBirthDate.Checked = true;
                }

                if (row["profession"] != DBNull.Value)
                    cmbProfession.Text = row["profession"].ToString();

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

                if (row["biography"] != DBNull.Value)
                    txtBiography.Text = row["biography"].ToString();

                // 3. Загружаем страны гражданства
                LoadPersonCountries();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPersonCountries()
        {
            try
            {
                string query = "SELECT country_id FROM cinema.person_countries WHERE person_id = @id";
                NpgsqlParameter param = new NpgsqlParameter("@id", personId.Value);
                DataTable personCountries = db.ExecuteQuery(query, param);

                // Отмечаем страны
                foreach (DataRow row in personCountries.Rows)
                {
                    int countryId = Convert.ToInt32(row["country_id"]);

                    for (int i = 0; i < chkCountries.Items.Count; i++)
                    {
                        var item = (CountryItem)chkCountries.Items[i];
                        if (item.Id == countryId)
                        {
                            chkCountries.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки стран гражданства: " + ex.Message);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // 1. Проверка
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию");
                return;
            }

            // Проверка заглавных букв
            if (txtFirstName.Text.Length > 0 && !char.IsUpper(txtFirstName.Text[0]))
            {
                MessageBox.Show("Имя должно начинаться с заглавной буквы");
                return;
            }

            if (txtLastName.Text.Length > 0 && !char.IsUpper(txtLastName.Text[0]))
            {
                MessageBox.Show("Фамилия должна начинаться с заглавной буквы");
                return;
            }

            try
            {
                // 2. Подготавливаем параметры для SQL
                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>
                {
                    new NpgsqlParameter("@first_name", txtFirstName.Text.Trim()),
                    new NpgsqlParameter("@last_name", txtLastName.Text.Trim()),
                    new NpgsqlParameter("@middle_name", string.IsNullOrWhiteSpace(txtMiddleName.Text) ? DBNull.Value : (object)txtMiddleName.Text.Trim()),
                    new NpgsqlParameter("@profession", string.IsNullOrWhiteSpace(cmbProfession.Text) ? DBNull.Value : (object)cmbProfession.Text),
                    new NpgsqlParameter("@biography", string.IsNullOrWhiteSpace(txtBiography.Text) ? DBNull.Value : (object)txtBiography.Text.Trim())
                };

                if (dtpBirthDate.Checked)
                    parameters.Add(new NpgsqlParameter("@birth_date", dtpBirthDate.Value));
                else
                    parameters.Add(new NpgsqlParameter("@birth_date", DBNull.Value));

                if (cmbStudio.SelectedItem is StudioItem selectedStudio && selectedStudio.Id > 0)
                    parameters.Add(new NpgsqlParameter("@studio_id", selectedStudio.Id));
                else
                    parameters.Add(new NpgsqlParameter("@studio_id", DBNull.Value));

                // 3. SQL запрос
                string query;
                if (personId.HasValue)
                {
                    // Обновление
                    query = @"UPDATE cinema.people 
                             SET first_name = @first_name, last_name = @last_name, middle_name = @middle_name,
                                 birth_date = @birth_date, profession = @profession, biography = @biography,
                                 studio_id = @studio_id
                             WHERE id = @id";
                    parameters.Add(new NpgsqlParameter("@id", personId.Value));
                }
                else
                {
                    // Добавление
                    query = @"INSERT INTO cinema.people 
                             (first_name, last_name, middle_name, birth_date, profession, biography, studio_id)
                             VALUES (@first_name, @last_name, @middle_name, @birth_date, @profession, 
                                     @biography, @studio_id)";
                }

                // 4. Выполняем запрос
                db.ExecuteNonQuery(query, parameters.ToArray());

                // 5. Если это новая персона, получаем ее ID
                if (!personId.HasValue)
                {
                    DataTable lastId = db.ExecuteQuery("SELECT MAX(id) FROM cinema.people");
                    if (lastId.Rows.Count > 0 && lastId.Rows[0][0] != DBNull.Value)
                        personId = Convert.ToInt32(lastId.Rows[0][0]);
                }

                // 6. Сохраняем страны гражданства
                SavePersonCountries();

                // 7. Закрываем форму
                MessageBox.Show("Данные сохранены");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (PostgresException pgEx)
            {
                MessageBox.Show($"Ошибка базы данных: {pgEx.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavePersonCountries()
        {
            try
            {
                // Удаляем старые страны
                string deleteQuery = "DELETE FROM cinema.person_countries WHERE person_id = @person_id";
                db.ExecuteNonQuery(deleteQuery, new NpgsqlParameter("@person_id", personId.Value));

                // Добавляем новые страны
                foreach (var checkedItem in chkCountries.CheckedItems)
                {
                    CountryItem country = (CountryItem)checkedItem;
                    string insertQuery = "INSERT INTO cinema.person_countries (person_id, country_id) VALUES (@person_id, @country_id)";
                    NpgsqlParameter[] parameters =
                    {
                        new NpgsqlParameter("@person_id", personId.Value),
                        new NpgsqlParameter("@country_id", country.Id)
                    };
                    db.ExecuteNonQuery(insertQuery, parameters);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения стран гражданства: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
