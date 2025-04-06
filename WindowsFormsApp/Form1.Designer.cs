namespace WindowsFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbChampionship = new ComboBox();
            pnlPlayers = new Panel();
            pnlPlayerFavourites = new Panel();
            SuspendLayout();
            // 
            // cbChampionship
            // 
            cbChampionship.FormattingEnabled = true;
            cbChampionship.Location = new Point(12, 12);
            cbChampionship.Name = "cbChampionship";
            cbChampionship.Size = new Size(158, 23);
            cbChampionship.TabIndex = 0;
            // 
            // pnlPlayers
            // 
            pnlPlayers.AutoScroll = true;
            pnlPlayers.Location = new Point(12, 72);
            pnlPlayers.Name = "pnlPlayers";
            pnlPlayers.Size = new Size(534, 381);
            pnlPlayers.TabIndex = 1;
            // 
            // pnlPlayerFavourites
            // 
            pnlPlayerFavourites.AutoScroll = true;
            pnlPlayerFavourites.ImeMode = ImeMode.NoControl;
            pnlPlayerFavourites.Location = new Point(552, 72);
            pnlPlayerFavourites.Name = "pnlPlayerFavourites";
            pnlPlayerFavourites.Size = new Size(420, 381);
            pnlPlayerFavourites.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 761);
            Controls.Add(pnlPlayerFavourites);
            Controls.Add(pnlPlayers);
            Controls.Add(cbChampionship);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Play";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbChampionship;
        private Panel pnlPlayers;
        private Panel pnlPlayerFavourites;
    }
}
