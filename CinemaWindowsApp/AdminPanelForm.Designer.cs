namespace CinemaWindowsApp
{
    partial class AdminPanelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonEditMovie = new System.Windows.Forms.Button();
            this.buttonAddMovie = new System.Windows.Forms.Button();
            this.dataGridViewMovies = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonEditPerson = new System.Windows.Forms.Button();
            this.buttonAddPerson = new System.Windows.Forms.Button();
            this.dataGridViewPeople = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.buttonEditStudio = new System.Windows.Forms.Button();
            this.buttonAddStudio = new System.Windows.Forms.Button();
            this.dataGridViewStudios = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridViewCountries = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dataGridViewGenres = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.button15 = new System.Windows.Forms.Button();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridViewCollections = new System.Windows.Forms.DataGridView();
            this.buttonBack = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMovies)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPeople)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudios)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCountries)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGenres)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollections)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(2, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(795, 435);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.CausesValidation = false;
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.buttonEditMovie);
            this.tabPage1.Controls.Add(this.buttonAddMovie);
            this.tabPage1.Controls.Add(this.dataGridViewMovies);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(787, 409);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Фильмы";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(673, 373);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 30);
            this.button4.TabIndex = 3;
            this.button4.Text = "Удалить";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.buttonDeleteMovie_Click);
            // 
            // buttonEditMovie
            // 
            this.buttonEditMovie.Location = new System.Drawing.Point(572, 374);
            this.buttonEditMovie.Name = "buttonEditMovie";
            this.buttonEditMovie.Size = new System.Drawing.Size(95, 30);
            this.buttonEditMovie.TabIndex = 2;
            this.buttonEditMovie.Text = "Изменить";
            this.buttonEditMovie.UseVisualStyleBackColor = true;
            this.buttonEditMovie.Click += new System.EventHandler(this.buttonEditMovie_Click);
            // 
            // buttonAddMovie
            // 
            this.buttonAddMovie.Location = new System.Drawing.Point(471, 373);
            this.buttonAddMovie.Name = "buttonAddMovie";
            this.buttonAddMovie.Size = new System.Drawing.Size(95, 30);
            this.buttonAddMovie.TabIndex = 1;
            this.buttonAddMovie.Text = "Добавить";
            this.buttonAddMovie.UseVisualStyleBackColor = true;
            this.buttonAddMovie.Click += new System.EventHandler(this.buttonAddMovie_Click);
            // 
            // dataGridViewMovies
            // 
            this.dataGridViewMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMovies.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewMovies.Name = "dataGridViewMovies";
            this.dataGridViewMovies.Size = new System.Drawing.Size(773, 353);
            this.dataGridViewMovies.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.buttonEditPerson);
            this.tabPage2.Controls.Add(this.buttonAddPerson);
            this.tabPage2.Controls.Add(this.dataGridViewPeople);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(787, 409);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Персоны";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(674, 372);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 30);
            this.button2.TabIndex = 7;
            this.button2.Text = "Удалить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonDeletePerson_Click);
            // 
            // buttonEditPerson
            // 
            this.buttonEditPerson.Location = new System.Drawing.Point(573, 373);
            this.buttonEditPerson.Name = "buttonEditPerson";
            this.buttonEditPerson.Size = new System.Drawing.Size(95, 30);
            this.buttonEditPerson.TabIndex = 6;
            this.buttonEditPerson.Text = "Изменить";
            this.buttonEditPerson.UseVisualStyleBackColor = true;
            this.buttonEditPerson.Click += new System.EventHandler(this.buttonEditPerson_Click);
            // 
            // buttonAddPerson
            // 
            this.buttonAddPerson.Location = new System.Drawing.Point(472, 372);
            this.buttonAddPerson.Name = "buttonAddPerson";
            this.buttonAddPerson.Size = new System.Drawing.Size(95, 30);
            this.buttonAddPerson.TabIndex = 5;
            this.buttonAddPerson.Text = "Добавить";
            this.buttonAddPerson.UseVisualStyleBackColor = true;
            this.buttonAddPerson.Click += new System.EventHandler(this.buttonAddPerson_Click);
            // 
            // dataGridViewPeople
            // 
            this.dataGridViewPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPeople.Location = new System.Drawing.Point(7, 5);
            this.dataGridViewPeople.Name = "dataGridViewPeople";
            this.dataGridViewPeople.Size = new System.Drawing.Size(773, 353);
            this.dataGridViewPeople.TabIndex = 4;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.buttonEditStudio);
            this.tabPage3.Controls.Add(this.buttonAddStudio);
            this.tabPage3.Controls.Add(this.dataGridViewStudios);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(787, 409);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Студии";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(674, 372);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(95, 30);
            this.button6.TabIndex = 7;
            this.button6.Text = "Удалить";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // buttonEditStudio
            // 
            this.buttonEditStudio.Location = new System.Drawing.Point(573, 373);
            this.buttonEditStudio.Name = "buttonEditStudio";
            this.buttonEditStudio.Size = new System.Drawing.Size(95, 30);
            this.buttonEditStudio.TabIndex = 6;
            this.buttonEditStudio.Text = "Изменить";
            this.buttonEditStudio.UseVisualStyleBackColor = true;
            // 
            // buttonAddStudio
            // 
            this.buttonAddStudio.Location = new System.Drawing.Point(472, 372);
            this.buttonAddStudio.Name = "buttonAddStudio";
            this.buttonAddStudio.Size = new System.Drawing.Size(95, 30);
            this.buttonAddStudio.TabIndex = 5;
            this.buttonAddStudio.Text = "Добавить";
            this.buttonAddStudio.UseVisualStyleBackColor = true;
            // 
            // dataGridViewStudios
            // 
            this.dataGridViewStudios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudios.Location = new System.Drawing.Point(7, 5);
            this.dataGridViewStudios.Name = "dataGridViewStudios";
            this.dataGridViewStudios.Size = new System.Drawing.Size(773, 353);
            this.dataGridViewStudios.TabIndex = 4;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridViewCountries);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(787, 409);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Страны";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridViewCountries
            // 
            this.dataGridViewCountries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCountries.Location = new System.Drawing.Point(7, 5);
            this.dataGridViewCountries.Name = "dataGridViewCountries";
            this.dataGridViewCountries.Size = new System.Drawing.Size(773, 353);
            this.dataGridViewCountries.TabIndex = 4;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dataGridViewGenres);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(787, 409);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Жанры";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dataGridViewGenres
            // 
            this.dataGridViewGenres.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGenres.Location = new System.Drawing.Point(7, 5);
            this.dataGridViewGenres.Name = "dataGridViewGenres";
            this.dataGridViewGenres.Size = new System.Drawing.Size(773, 353);
            this.dataGridViewGenres.TabIndex = 4;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.button15);
            this.tabPage6.Controls.Add(this.dataGridViewUsers);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(787, 409);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Пользователи";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(674, 372);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(95, 30);
            this.button15.TabIndex = 7;
            this.button15.Text = "Удалить";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.buttonDeleteUser_Click);
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(7, 5);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.Size = new System.Drawing.Size(773, 353);
            this.dataGridViewUsers.TabIndex = 4;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.button3);
            this.tabPage7.Controls.Add(this.dataGridViewCollections);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(787, 409);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Подборки";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(674, 372);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 30);
            this.button3.TabIndex = 11;
            this.button3.Text = "Удалить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonDeleteCollection_Click);
            // 
            // dataGridViewCollections
            // 
            this.dataGridViewCollections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCollections.Location = new System.Drawing.Point(7, 5);
            this.dataGridViewCollections.Name = "dataGridViewCollections";
            this.dataGridViewCollections.Size = new System.Drawing.Size(773, 353);
            this.dataGridViewCollections.TabIndex = 8;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(6, 12);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(85, 28);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // AdminPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 484);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.tabControl1);
            this.Name = "AdminPanelForm";
            this.Text = "Панель администрации";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMovies)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPeople)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudios)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCountries)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGenres)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollections)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.DataGridView dataGridViewMovies;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonEditMovie;
        private System.Windows.Forms.Button buttonAddMovie;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonEditPerson;
        private System.Windows.Forms.Button buttonAddPerson;
        private System.Windows.Forms.DataGridView dataGridViewPeople;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button buttonEditStudio;
        private System.Windows.Forms.Button buttonAddStudio;
        private System.Windows.Forms.DataGridView dataGridViewStudios;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridViewCountries;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dataGridViewGenres;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridViewCollections;
    }
}