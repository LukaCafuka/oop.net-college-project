namespace WindowsFormsApp
{
    partial class PlayerInfo
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
            lblPlayerName = new Label();
            lblPlayerNumber = new Label();
            lblPosition = new Label();
            lblCaptain = new Label();
            lblFavourite = new Label();
            SuspendLayout();
            // 
            // lblPlayerName
            // 
            lblPlayerName.AutoSize = true;
            lblPlayerName.Location = new Point(12, 13);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(39, 15);
            lblPlayerName.TabIndex = 0;
            lblPlayerName.Text = "Name";
            // 
            // lblPlayerNumber
            // 
            lblPlayerNumber.AutoSize = true;
            lblPlayerNumber.Location = new Point(12, 45);
            lblPlayerNumber.Name = "lblPlayerNumber";
            lblPlayerNumber.Size = new Size(34, 15);
            lblPlayerNumber.TabIndex = 1;
            lblPlayerNumber.Text = "Num";
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(12, 76);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(50, 15);
            lblPosition.TabIndex = 2;
            lblPosition.Text = "Position";
            // 
            // lblCaptain
            // 
            lblCaptain.AutoSize = true;
            lblCaptain.Location = new Point(107, 13);
            lblCaptain.Name = "lblCaptain";
            lblCaptain.Size = new Size(50, 15);
            lblCaptain.TabIndex = 3;
            lblCaptain.Text = "Kapetan";
            // 
            // lblFavourite
            // 
            lblFavourite.AutoSize = true;
            lblFavourite.Location = new Point(107, 28);
            lblFavourite.Name = "lblFavourite";
            lblFavourite.Size = new Size(56, 15);
            lblFavourite.TabIndex = 4;
            lblFavourite.Text = "Favourite";
            // 
            // PlayerInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblFavourite);
            Controls.Add(lblCaptain);
            Controls.Add(lblPosition);
            Controls.Add(lblPlayerNumber);
            Controls.Add(lblPlayerName);
            Name = "PlayerInfo";
            Size = new Size(300, 100);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPlayerName;
        private Label lblPlayerNumber;
        private Label lblPosition;
        private Label lblCaptain;
        private Label lblFavourite;
    }
}
