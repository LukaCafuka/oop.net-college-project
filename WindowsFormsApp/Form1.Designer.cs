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
            pnlGoalsRanking = new Panel();
            pnlYellowCardsRanking = new Panel();
            pnlAttendanceRanking = new Panel();
            btnPrintRankings = new Button();
            btnOpenConfig = new Button();
            SuspendLayout();
            // 
            // cbChampionship
            // 
            cbChampionship.DropDownStyle = ComboBoxStyle.DropDownList;
            cbChampionship.FormattingEnabled = true;
            cbChampionship.Location = new Point(12, 12);
            cbChampionship.Name = "cbChampionship";
            cbChampionship.Size = new Size(400, 23);
            cbChampionship.TabIndex = 0;
            // 
            // pnlPlayers
            // 
            pnlPlayers.AutoScroll = true;
            pnlPlayers.BorderStyle = BorderStyle.FixedSingle;
            pnlPlayers.Location = new Point(12, 41);
            pnlPlayers.Name = "pnlPlayers";
            pnlPlayers.Size = new Size(400, 747);
            pnlPlayers.TabIndex = 1;
            // 
            // pnlPlayerFavourites
            // 
            pnlPlayerFavourites.AutoScroll = true;
            pnlPlayerFavourites.BorderStyle = BorderStyle.FixedSingle;
            pnlPlayerFavourites.Location = new Point(418, 41);
            pnlPlayerFavourites.Name = "pnlPlayerFavourites";
            pnlPlayerFavourites.Size = new Size(400, 747);
            pnlPlayerFavourites.TabIndex = 2;
            // 
            // pnlGoalsRanking
            // 
            pnlGoalsRanking.AutoScroll = true;
            pnlGoalsRanking.BorderStyle = BorderStyle.FixedSingle;
            pnlGoalsRanking.Location = new Point(850, 12);
            pnlGoalsRanking.Name = "pnlGoalsRanking";
            pnlGoalsRanking.Size = new Size(338, 243);
            pnlGoalsRanking.TabIndex = 0;
            // 
            // pnlYellowCardsRanking
            // 
            pnlYellowCardsRanking.AutoScroll = true;
            pnlYellowCardsRanking.BorderStyle = BorderStyle.FixedSingle;
            pnlYellowCardsRanking.Location = new Point(850, 261);
            pnlYellowCardsRanking.Name = "pnlYellowCardsRanking";
            pnlYellowCardsRanking.Size = new Size(338, 292);
            pnlYellowCardsRanking.TabIndex = 1;
            // 
            // pnlAttendanceRanking
            // 
            pnlAttendanceRanking.AutoScroll = true;
            pnlAttendanceRanking.BorderStyle = BorderStyle.FixedSingle;
            pnlAttendanceRanking.Location = new Point(850, 559);
            pnlAttendanceRanking.Name = "pnlAttendanceRanking";
            pnlAttendanceRanking.Size = new Size(338, 204);
            pnlAttendanceRanking.TabIndex = 2;
            // 
            // btnPrintRankings
            // 
            btnPrintRankings.Location = new Point(850, 769);
            btnPrintRankings.Name = "btnPrintRankings";
            btnPrintRankings.Size = new Size(159, 23);
            btnPrintRankings.TabIndex = 6;
            btnPrintRankings.Text = "Print Rankings";
            btnPrintRankings.UseVisualStyleBackColor = true;
            btnPrintRankings.Click += btnPrintRankings_Click;
            // 
            // btnOpenConfig
            // 
            btnOpenConfig.Location = new Point(1053, 769);
            btnOpenConfig.Name = "btnOpenConfig";
            btnOpenConfig.Size = new Size(135, 23);
            btnOpenConfig.TabIndex = 7;
            btnOpenConfig.Text = "Settings";
            btnOpenConfig.UseVisualStyleBackColor = true;
            btnOpenConfig.Click += btnOpenConfig_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            Controls.Add(btnOpenConfig);
            Controls.Add(btnPrintRankings);
            Controls.Add(pnlAttendanceRanking);
            Controls.Add(pnlYellowCardsRanking);
            Controls.Add(pnlGoalsRanking);
            Controls.Add(pnlPlayerFavourites);
            Controls.Add(pnlPlayers);
            Controls.Add(cbChampionship);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox cbChampionship;
        private System.Windows.Forms.Panel pnlPlayers;
        private System.Windows.Forms.Panel pnlPlayerFavourites;
        private System.Windows.Forms.Panel pnlGoalsRanking;
        private System.Windows.Forms.Panel pnlYellowCardsRanking;
        private System.Windows.Forms.Panel pnlAttendanceRanking;
        private System.Windows.Forms.Button btnPrintRankings;
        private System.Windows.Forms.Button btnOpenConfig;
    }
}
