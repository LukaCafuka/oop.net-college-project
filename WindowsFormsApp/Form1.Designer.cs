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
            this.components = new System.ComponentModel.Container();
            this.cbChampionship = new System.Windows.Forms.ComboBox();
            this.pnlPlayers = new System.Windows.Forms.Panel();
            this.pnlPlayerFavourites = new System.Windows.Forms.Panel();
            this.lstGoalsRanking = new System.Windows.Forms.ListBox();
            this.lstYellowCardsRanking = new System.Windows.Forms.ListBox();
            this.lstAttendanceRanking = new System.Windows.Forms.ListBox();
            this.btnPrintRankings = new System.Windows.Forms.Button();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // cbChampionship
            // 
            this.cbChampionship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChampionship.FormattingEnabled = true;
            this.cbChampionship.Location = new System.Drawing.Point(12, 12);
            this.cbChampionship.Name = "cbChampionship";
            this.cbChampionship.Size = new System.Drawing.Size(200, 23);
            this.cbChampionship.TabIndex = 0;
            // 
            // pnlPlayers
            // 
            this.pnlPlayers.AutoScroll = true;
            this.pnlPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlayers.Location = new System.Drawing.Point(12, 41);
            this.pnlPlayers.Name = "pnlPlayers";
            this.pnlPlayers.Size = new System.Drawing.Size(300, 400);
            this.pnlPlayers.TabIndex = 1;
            // 
            // pnlPlayerFavourites
            // 
            this.pnlPlayerFavourites.AutoScroll = true;
            this.pnlPlayerFavourites.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlayerFavourites.Location = new System.Drawing.Point(318, 41);
            this.pnlPlayerFavourites.Name = "pnlPlayerFavourites";
            this.pnlPlayerFavourites.Size = new System.Drawing.Size(300, 400);
            this.pnlPlayerFavourites.TabIndex = 2;
            // 
            // lstGoalsRanking
            // 
            this.lstGoalsRanking.FormattingEnabled = true;
            this.lstGoalsRanking.ItemHeight = 15;
            this.lstGoalsRanking.Location = new System.Drawing.Point(624, 41);
            this.lstGoalsRanking.Name = "lstGoalsRanking";
            this.lstGoalsRanking.Size = new System.Drawing.Size(200, 124);
            this.lstGoalsRanking.TabIndex = 3;
            // 
            // lstYellowCardsRanking
            // 
            this.lstYellowCardsRanking.FormattingEnabled = true;
            this.lstYellowCardsRanking.ItemHeight = 15;
            this.lstYellowCardsRanking.Location = new System.Drawing.Point(624, 171);
            this.lstYellowCardsRanking.Name = "lstYellowCardsRanking";
            this.lstYellowCardsRanking.Size = new System.Drawing.Size(200, 124);
            this.lstYellowCardsRanking.TabIndex = 4;
            // 
            // lstAttendanceRanking
            // 
            this.lstAttendanceRanking.FormattingEnabled = true;
            this.lstAttendanceRanking.ItemHeight = 15;
            this.lstAttendanceRanking.Location = new System.Drawing.Point(624, 301);
            this.lstAttendanceRanking.Name = "lstAttendanceRanking";
            this.lstAttendanceRanking.Size = new System.Drawing.Size(200, 124);
            this.lstAttendanceRanking.TabIndex = 5;
            // 
            // btnPrintRankings
            // 
            this.btnPrintRankings.Location = new System.Drawing.Point(624, 431);
            this.btnPrintRankings.Name = "btnPrintRankings";
            this.btnPrintRankings.Size = new System.Drawing.Size(200, 23);
            this.btnPrintRankings.TabIndex = 6;
            this.btnPrintRankings.Text = "Print Rankings";
            this.btnPrintRankings.UseVisualStyleBackColor = true;
            this.btnPrintRankings.Click += new System.EventHandler(this.btnPrintRankings_Click);
            // 
            // btnOpenConfig
            // 
            this.btnOpenConfig.Location = new System.Drawing.Point(218, 12);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(94, 23);
            this.btnOpenConfig.TabIndex = 7;
            this.btnOpenConfig.Text = "Settings";
            this.btnOpenConfig.UseVisualStyleBackColor = true;
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 466);
            this.Controls.Add(this.btnOpenConfig);
            this.Controls.Add(this.btnPrintRankings);
            this.Controls.Add(this.lstAttendanceRanking);
            this.Controls.Add(this.lstYellowCardsRanking);
            this.Controls.Add(this.lstGoalsRanking);
            this.Controls.Add(this.pnlPlayerFavourites);
            this.Controls.Add(this.pnlPlayers);
            this.Controls.Add(this.cbChampionship);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox cbChampionship;
        private System.Windows.Forms.Panel pnlPlayers;
        private System.Windows.Forms.Panel pnlPlayerFavourites;
        private System.Windows.Forms.ListBox lstGoalsRanking;
        private System.Windows.Forms.ListBox lstYellowCardsRanking;
        private System.Windows.Forms.ListBox lstAttendanceRanking;
        private System.Windows.Forms.Button btnPrintRankings;
        private System.Windows.Forms.Button btnOpenConfig;
    }
}
