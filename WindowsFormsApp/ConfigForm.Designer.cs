namespace WindowsFormsApp
{
    partial class ConfigForm
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
            gbChampionshipSelection = new GroupBox();
            rbGenderFemaleSelect = new RadioButton();
            rbGenderMaleSelect = new RadioButton();
            gbLangSelection = new GroupBox();
            rbEnglishLangSelect = new RadioButton();
            rbCroatianLangSelect = new RadioButton();
            btnSubmitConfig = new Button();
            gbFavoriteTeam = new GroupBox();
            cbFavoriteTeam = new ComboBox();
            lblFavoriteTeam = new Label();
            gbChampionshipSelection.SuspendLayout();
            gbLangSelection.SuspendLayout();
            gbFavoriteTeam.SuspendLayout();
            SuspendLayout();
            // 
            // gbChampionshipSelection
            // 
            gbChampionshipSelection.Controls.Add(rbGenderFemaleSelect);
            gbChampionshipSelection.Controls.Add(rbGenderMaleSelect);
            gbChampionshipSelection.Location = new Point(45, 29);
            gbChampionshipSelection.Name = "gbChampionshipSelection";
            gbChampionshipSelection.Size = new Size(200, 100);
            gbChampionshipSelection.TabIndex = 0;
            gbChampionshipSelection.TabStop = false;
            gbChampionshipSelection.Text = "groupBox1";
            // 
            // rbGenderFemaleSelect
            // 
            rbGenderFemaleSelect.AutoSize = true;
            rbGenderFemaleSelect.Location = new Point(22, 47);
            rbGenderFemaleSelect.Name = "rbGenderFemaleSelect";
            rbGenderFemaleSelect.Size = new Size(63, 19);
            rbGenderFemaleSelect.TabIndex = 1;
            rbGenderFemaleSelect.TabStop = true;
            rbGenderFemaleSelect.Text = "Female";
            rbGenderFemaleSelect.UseVisualStyleBackColor = true;
            rbGenderFemaleSelect.Click += rbGenderFemaleSelect_Click;
            // 
            // rbGenderMaleSelect
            // 
            rbGenderMaleSelect.AutoSize = true;
            rbGenderMaleSelect.Location = new Point(22, 22);
            rbGenderMaleSelect.Name = "rbGenderMaleSelect";
            rbGenderMaleSelect.Size = new Size(51, 19);
            rbGenderMaleSelect.TabIndex = 0;
            rbGenderMaleSelect.TabStop = true;
            rbGenderMaleSelect.Text = "Male";
            rbGenderMaleSelect.UseVisualStyleBackColor = true;
            rbGenderMaleSelect.Click += rbGenderMaleSelect_Click;
            // 
            // gbLangSelection
            // 
            gbLangSelection.Controls.Add(rbEnglishLangSelect);
            gbLangSelection.Controls.Add(rbCroatianLangSelect);
            gbLangSelection.Location = new Point(353, 29);
            gbLangSelection.Name = "gbLangSelection";
            gbLangSelection.Size = new Size(200, 100);
            gbLangSelection.TabIndex = 1;
            gbLangSelection.TabStop = false;
            gbLangSelection.Text = "groupBox2";
            // 
            // rbEnglishLangSelect
            // 
            rbEnglishLangSelect.AutoSize = true;
            rbEnglishLangSelect.Checked = true;
            rbEnglishLangSelect.Location = new Point(19, 47);
            rbEnglishLangSelect.Name = "rbEnglishLangSelect";
            rbEnglishLangSelect.Size = new Size(94, 19);
            rbEnglishLangSelect.TabIndex = 3;
            rbEnglishLangSelect.TabStop = true;
            rbEnglishLangSelect.Text = "radioButton3";
            rbEnglishLangSelect.UseVisualStyleBackColor = true;
            rbEnglishLangSelect.Click += CroatianLang_Click;
            // 
            // rbCroatianLangSelect
            // 
            rbCroatianLangSelect.AutoSize = true;
            rbCroatianLangSelect.Location = new Point(19, 22);
            rbCroatianLangSelect.Name = "rbCroatianLangSelect";
            rbCroatianLangSelect.Size = new Size(94, 19);
            rbCroatianLangSelect.TabIndex = 2;
            rbCroatianLangSelect.Text = "radioButton4";
            rbCroatianLangSelect.UseVisualStyleBackColor = true;
            rbCroatianLangSelect.Click += EnglishLang_Click;
            // 
            // btnSubmitConfig
            // 
            btnSubmitConfig.DialogResult = DialogResult.OK;
            btnSubmitConfig.Location = new Point(233, 209);
            btnSubmitConfig.Name = "btnSubmitConfig";
            btnSubmitConfig.Size = new Size(119, 40);
            btnSubmitConfig.TabIndex = 2;
            btnSubmitConfig.Text = "button1";
            btnSubmitConfig.UseVisualStyleBackColor = true;
            btnSubmitConfig.Click += btnSubmitConfig_Click;
            // 
            // gbFavoriteTeam
            // 
            gbFavoriteTeam.Controls.Add(cbFavoriteTeam);
            gbFavoriteTeam.Controls.Add(lblFavoriteTeam);
            gbFavoriteTeam.Location = new Point(45, 135);
            gbFavoriteTeam.Name = "gbFavoriteTeam";
            gbFavoriteTeam.Size = new Size(508, 60);
            gbFavoriteTeam.TabIndex = 3;
            gbFavoriteTeam.TabStop = false;
            gbFavoriteTeam.Text = "Favorite Team";
            // 
            // cbFavoriteTeam
            // 
            cbFavoriteTeam.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFavoriteTeam.FormattingEnabled = true;
            cbFavoriteTeam.Location = new Point(6, 22);
            cbFavoriteTeam.Name = "cbFavoriteTeam";
            cbFavoriteTeam.Size = new Size(496, 23);
            cbFavoriteTeam.TabIndex = 1;
            cbFavoriteTeam.SelectedIndexChanged += cbFavoriteTeam_SelectedIndexChanged;
            // 
            // lblFavoriteTeam
            // 
            lblFavoriteTeam.AutoSize = true;
            lblFavoriteTeam.Location = new Point(6, 24);
            lblFavoriteTeam.Name = "lblFavoriteTeam";
            lblFavoriteTeam.Size = new Size(0, 15);
            lblFavoriteTeam.TabIndex = 0;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 261);
            ControlBox = false;
            Controls.Add(gbFavoriteTeam);
            Controls.Add(btnSubmitConfig);
            Controls.Add(gbLangSelection);
            Controls.Add(gbChampionshipSelection);
            Name = "ConfigForm";
            Text = "ConfigForm";
            Load += ConfigForm_Load;
            gbChampionshipSelection.ResumeLayout(false);
            gbChampionshipSelection.PerformLayout();
            gbLangSelection.ResumeLayout(false);
            gbLangSelection.PerformLayout();
            gbFavoriteTeam.ResumeLayout(false);
            gbFavoriteTeam.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbChampionshipSelection;
        private RadioButton rbGenderFemaleSelect;
        private RadioButton rbGenderMaleSelect;
        private GroupBox gbLangSelection;
        private RadioButton rbEnglishLangSelect;
        private RadioButton rbCroatianLangSelect;
        private Button btnSubmitConfig;
        private GroupBox gbFavoriteTeam;
        private ComboBox cbFavoriteTeam;
        private Label lblFavoriteTeam;
    }
}