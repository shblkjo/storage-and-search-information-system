namespace CinemaWindowsApp
{
    partial class MovieDetailsForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblAgeRating = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.lblStudio = new System.Windows.Forms.Label();
            this.lblGenres = new System.Windows.Forms.Label();
            this.lblActors = new System.Windows.Forms.Label();
            this.lblDirectors = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(289, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(37, 13);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "lblTitle";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(289, 51);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(39, 13);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "lblYear";
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(289, 74);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(48, 13);
            this.lblRating.TabIndex = 3;
            this.lblRating.Text = "lblRating";
            // 
            // lblAgeRating
            // 
            this.lblAgeRating.AutoSize = true;
            this.lblAgeRating.Location = new System.Drawing.Point(289, 98);
            this.lblAgeRating.Name = "lblAgeRating";
            this.lblAgeRating.Size = new System.Drawing.Size(67, 13);
            this.lblAgeRating.TabIndex = 4;
            this.lblAgeRating.Text = "lblAgeRating";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(289, 123);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(57, 13);
            this.lblDuration.TabIndex = 5;
            this.lblDuration.Text = "lblDuration";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(289, 149);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(53, 13);
            this.lblCountry.TabIndex = 6;
            this.lblCountry.Text = "lblCountry";
            // 
            // lblStudio
            // 
            this.lblStudio.AutoSize = true;
            this.lblStudio.Location = new System.Drawing.Point(289, 176);
            this.lblStudio.Name = "lblStudio";
            this.lblStudio.Size = new System.Drawing.Size(47, 13);
            this.lblStudio.TabIndex = 7;
            this.lblStudio.Text = "lblStudio";
            // 
            // lblGenres
            // 
            this.lblGenres.AutoSize = true;
            this.lblGenres.Location = new System.Drawing.Point(289, 200);
            this.lblGenres.Name = "lblGenres";
            this.lblGenres.Size = new System.Drawing.Size(51, 13);
            this.lblGenres.TabIndex = 8;
            this.lblGenres.Text = "lblGenres";
            // 
            // lblActors
            // 
            this.lblActors.AutoSize = true;
            this.lblActors.Location = new System.Drawing.Point(289, 226);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(47, 13);
            this.lblActors.TabIndex = 9;
            this.lblActors.Text = "lblActors";
            // 
            // lblDirectors
            // 
            this.lblDirectors.AutoSize = true;
            this.lblDirectors.Location = new System.Drawing.Point(289, 252);
            this.lblDirectors.Name = "lblDirectors";
            this.lblDirectors.Size = new System.Drawing.Size(59, 13);
            this.lblDirectors.TabIndex = 10;
            this.lblDirectors.Text = "lblDirectors";
            // 
            // txtDescription
            // 
            this.txtDescription.AutoSize = true;
            this.txtDescription.Location = new System.Drawing.Point(289, 275);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(71, 13);
            this.txtDescription.TabIndex = 11;
            this.txtDescription.Text = "txtDescription";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(289, 297);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(41, 13);
            this.lblType.TabIndex = 12;
            this.lblType.Text = "lblType";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(570, 389);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(85, 27);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // MovieDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 428);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDirectors);
            this.Controls.Add(this.lblActors);
            this.Controls.Add(this.lblGenres);
            this.Controls.Add(this.lblStudio);
            this.Controls.Add(this.lblCountry);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblAgeRating);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblTitle);
            this.Name = "MovieDetailsForm";
            this.Text = "MovieDetailsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblAgeRating;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblStudio;
        private System.Windows.Forms.Label lblGenres;
        private System.Windows.Forms.Label lblActors;
        private System.Windows.Forms.Label lblDirectors;
        private System.Windows.Forms.Label txtDescription;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button buttonClose;
    }
}