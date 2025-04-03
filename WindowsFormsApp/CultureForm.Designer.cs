namespace WindowsFormsApp
{
    partial class CultureForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CultureForm));
            rbFemaleSelect = new RadioButton();
            rbMaleSelect = new RadioButton();
            gbGenderSelection = new GroupBox();
            gbLangSelection = new GroupBox();
            rbCroatianSelect = new RadioButton();
            rbEnglishSelect = new RadioButton();
            gbGenderSelection.SuspendLayout();
            gbLangSelection.SuspendLayout();
            SuspendLayout();
            // 
            // rbFemaleSelect
            // 
            resources.ApplyResources(rbFemaleSelect, "rbFemaleSelect");
            rbFemaleSelect.Name = "rbFemaleSelect";
            rbFemaleSelect.TabStop = true;
            rbFemaleSelect.UseVisualStyleBackColor = true;
            // 
            // rbMaleSelect
            // 
            resources.ApplyResources(rbMaleSelect, "rbMaleSelect");
            rbMaleSelect.Name = "rbMaleSelect";
            rbMaleSelect.TabStop = true;
            rbMaleSelect.UseVisualStyleBackColor = true;
            // 
            // gbGenderSelection
            // 
            gbGenderSelection.Controls.Add(rbMaleSelect);
            gbGenderSelection.Controls.Add(rbFemaleSelect);
            resources.ApplyResources(gbGenderSelection, "gbGenderSelection");
            gbGenderSelection.Name = "gbGenderSelection";
            gbGenderSelection.TabStop = false;
            // 
            // gbLangSelection
            // 
            gbLangSelection.Controls.Add(rbEnglishSelect);
            gbLangSelection.Controls.Add(rbCroatianSelect);
            resources.ApplyResources(gbLangSelection, "gbLangSelection");
            gbLangSelection.Name = "gbLangSelection";
            gbLangSelection.TabStop = false;
            // 
            // rbCroatianSelect
            // 
            resources.ApplyResources(rbCroatianSelect, "rbCroatianSelect");
            rbCroatianSelect.Name = "rbCroatianSelect";
            rbCroatianSelect.TabStop = true;
            rbCroatianSelect.UseVisualStyleBackColor = true;
            // 
            // rbEnglishSelect
            // 
            resources.ApplyResources(rbEnglishSelect, "rbEnglishSelect");
            rbEnglishSelect.Name = "rbEnglishSelect";
            rbEnglishSelect.TabStop = true;
            rbEnglishSelect.UseVisualStyleBackColor = true;
            // 
            // CultureForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbLangSelection);
            Controls.Add(gbGenderSelection);
            Name = "CultureForm";
            Load += CultureForm_Load;
            gbGenderSelection.ResumeLayout(false);
            gbGenderSelection.PerformLayout();
            gbLangSelection.ResumeLayout(false);
            gbLangSelection.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RadioButton rbFemaleSelect;
        private RadioButton rbMaleSelect;
        private GroupBox gbGenderSelection;
        private GroupBox gbLangSelection;
        private RadioButton rbEnglishSelect;
        private RadioButton rbCroatianSelect;
    }
}
