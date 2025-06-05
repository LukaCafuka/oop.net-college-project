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
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.lblPlayerNumber = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblCaptain = new System.Windows.Forms.Label();
            this.pictureBoxPlayer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Location = new System.Drawing.Point(60, 10);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(38, 15);
            this.lblPlayerName.TabIndex = 0;
            this.lblPlayerName.Text = "Name";
            // 
            // lblPlayerNumber
            // 
            this.lblPlayerNumber.AutoSize = true;
            this.lblPlayerNumber.Location = new System.Drawing.Point(60, 35);
            this.lblPlayerNumber.Name = "lblPlayerNumber";
            this.lblPlayerNumber.Size = new System.Drawing.Size(51, 15);
            this.lblPlayerNumber.TabIndex = 1;
            this.lblPlayerNumber.Text = "Number";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(60, 60);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(50, 15);
            this.lblPosition.TabIndex = 2;
            this.lblPosition.Text = "Position";
            // 
            // lblCaptain
            // 
            this.lblCaptain.AutoSize = true;
            this.lblCaptain.Location = new System.Drawing.Point(60, 85);
            this.lblCaptain.Name = "lblCaptain";
            this.lblCaptain.Size = new System.Drawing.Size(47, 15);
            this.lblCaptain.TabIndex = 3;
            this.lblCaptain.Text = "Captain";
            // 
            // pictureBoxPlayer
            // 
            this.pictureBoxPlayer.Location = new System.Drawing.Point(10, 10);
            this.pictureBoxPlayer.Name = "pictureBoxPlayer";
            this.pictureBoxPlayer.Size = new System.Drawing.Size(40, 40);
            this.pictureBoxPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPlayer.TabIndex = 4;
            this.pictureBoxPlayer.TabStop = false;
            // 
            // PlayerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBoxPlayer);
            this.Controls.Add(this.lblCaptain);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblPlayerNumber);
            this.Controls.Add(this.lblPlayerName);
            this.Name = "PlayerInfo";
            this.Size = new System.Drawing.Size(300, 110);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayerInfo_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblPlayerNumber;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblCaptain;
        private System.Windows.Forms.PictureBox pictureBoxPlayer;
    }
}
