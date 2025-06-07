using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public class RankListItem : UserControl
    {
        private PictureBox playerImage = null!;
        private Label playerNameLabel = null!;
        private Label countLabel = null!;
        private PlayerImageManager imageManager = null!;

        public override string Text => playerNameLabel.Text;
        public int Count { get; private set; }

        public RankListItem(string playerName, int count)
        {
            InitializeComponent();
            imageManager = new PlayerImageManager();
            SetData(playerName, count);
            LoadPlayerImage(playerName);
        }

        private void InitializeComponent()
        {
            this.playerImage = new PictureBox();
            this.playerNameLabel = new Label();
            this.countLabel = new Label();

            // Configure player image
            this.playerImage.Size = new Size(80, 80);
            this.playerImage.SizeMode = PictureBoxSizeMode.Zoom;
            this.playerImage.Location = new Point(5, 5);
            this.playerImage.BorderStyle = BorderStyle.FixedSingle;

            // Configure player name label
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Location = new Point(90, 5);
            this.playerNameLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            this.playerNameLabel.MaximumSize = new Size(200, 0);

            // Configure count label
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new Point(90, 30);
            this.countLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Configure control
            this.Controls.Add(this.playerImage);
            this.Controls.Add(this.playerNameLabel);
            this.Controls.Add(this.countLabel);
            this.Size = new Size(300, 90);
            this.BackColor = Color.White;
        }

        public void SetData(string playerName, int count)
        {
            if (string.IsNullOrEmpty(playerName)) return;

            this.playerNameLabel.Text = playerName;
            this.countLabel.Text = count.ToString("N0");
            this.Count = count;
            LoadPlayerImage(playerName);
        }

        private void LoadPlayerImage(string playerName)
        {
            try
            {
                if (string.IsNullOrEmpty(playerName)) return;

                var image = imageManager.LoadPlayerImage(playerName);
                if (image != null)
                {
                    if (playerImage.Image != null)
                    {
                        var oldImage = playerImage.Image;
                        playerImage.Image = image;
                        oldImage.Dispose();
                    }
                    else
                    {
                        playerImage.Image = image;
                    }
                }
            }
            catch (Exception)
            {
                // If image loading fails, the default image will be shown
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (playerImage.Image != null)
                {
                    playerImage.Image.Dispose();
                }
                playerImage.Dispose();
                playerNameLabel.Dispose();
                countLabel.Dispose();
            }
            base.Dispose(disposing);
        }
    }
} 