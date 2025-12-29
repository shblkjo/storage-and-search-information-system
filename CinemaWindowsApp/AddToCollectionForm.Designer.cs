namespace CinemaWindowsApp
{
    partial class AddToCollectionForm
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
            this.dgvCollections = new System.Windows.Forms.DataGridView();
            this.lblCollectionsCount = new System.Windows.Forms.Label();
            this.buttonCreateNew = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCollections)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCollections
            // 
            this.dgvCollections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCollections.Location = new System.Drawing.Point(47, 73);
            this.dgvCollections.Name = "dgvCollections";
            this.dgvCollections.Size = new System.Drawing.Size(421, 224);
            this.dgvCollections.TabIndex = 0;
            // 
            // lblCollectionsCount
            // 
            this.lblCollectionsCount.AutoSize = true;
            this.lblCollectionsCount.Location = new System.Drawing.Point(44, 36);
            this.lblCollectionsCount.Name = "lblCollectionsCount";
            this.lblCollectionsCount.Size = new System.Drawing.Size(96, 13);
            this.lblCollectionsCount.TabIndex = 1;
            this.lblCollectionsCount.Text = "lblCollectionsCount";
            // 
            // buttonCreateNew
            // 
            this.buttonCreateNew.Location = new System.Drawing.Point(359, 24);
            this.buttonCreateNew.Name = "buttonCreateNew";
            this.buttonCreateNew.Size = new System.Drawing.Size(109, 25);
            this.buttonCreateNew.TabIndex = 2;
            this.buttonCreateNew.Text = "Создать подборку";
            this.buttonCreateNew.UseVisualStyleBackColor = true;
            this.buttonCreateNew.Click += new System.EventHandler(this.buttonCreateNew_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(393, 318);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Закрыть";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 318);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Добавить в выбранную";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonAddToSelected_Click);
            // 
            // AddToCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 383);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCreateNew);
            this.Controls.Add(this.lblCollectionsCount);
            this.Controls.Add(this.dgvCollections);
            this.Name = "AddToCollectionForm";
            this.Text = "Добавление в коллекцию";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCollections)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCollections;
        private System.Windows.Forms.Label lblCollectionsCount;
        private System.Windows.Forms.Button buttonCreateNew;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button button1;
    }
}