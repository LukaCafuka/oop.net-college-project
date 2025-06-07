using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickType;

namespace WindowsFormsApp
{
    public partial class PlayerInfo : UserControl
    {
        public StartingEleven Player { get; private set; }
        public bool IsFavorite { get; private set; }
        private ContextMenuStrip contextMenu;
        private Label starLabel;

        public event EventHandler<PlayerInfo> FavoriteStatusChanged;

        public PlayerInfo(StartingEleven player)
        {
            InitializeComponent();
            Player = player;
            InitializeContextMenu();
            InitializeStarLabel();
            SetData(player);
            this.AllowDrop = true;
            this.DragEnter += PlayerInfo_DragEnter;
            this.DragDrop += PlayerInfo_DragDrop;
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            var toggleFavorite = new ToolStripMenuItem("Toggle Favorite");
            toggleFavorite.Click += (s, e) => ToggleFavorite();
            contextMenu.Items.Add(toggleFavorite);
            this.ContextMenuStrip = contextMenu;
        }

        private void InitializeStarLabel()
        {
            starLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(lblPlayerName.Right + 5, lblPlayerName.Top - 2),
                Text = "☆",
                ForeColor = Color.Gold,
                Visible = false,
                BackColor = Color.Transparent
            };
            this.Controls.Add(starLabel);
        }

        private void SetData(StartingEleven player)
        {
            lblPlayerName.Text = player.Name;
            lblPlayerNumber.Text = player.ShirtNumber.ToString();
            lblPosition.Text = player.Position.ToString();
            lblCaptain.Text = player.Captain ? "Captain" : " ";

            // Load player image
            string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "assets", "images", "players");
            string defaultImagePath = Path.Combine(imagesDir, "default_player.png");
            string playerImagePath = Path.Combine(imagesDir, $"{player.Name}.png");

            try
            {
                if (File.Exists(playerImagePath))
                {
                    pictureBoxPlayer.Image = Image.FromFile(playerImagePath);
                }
                else if (File.Exists(defaultImagePath))
                {
                    pictureBoxPlayer.Image = Image.FromFile(defaultImagePath);
                }
                else
                {
                    // Create a default image if none exists
                    using (Bitmap bmp = new Bitmap(40, 40))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.LightGray);
                            g.DrawString("?", new Font("Arial", 20), Brushes.Black, 15, 5);
                        }
                        pictureBoxPlayer.Image = new Bitmap(bmp);
                    }
                }
            }
            catch (Exception ex)
            {
                // If there's an error loading the image, create a default one
                using (Bitmap bmp = new Bitmap(40, 40))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.LightGray);
                        g.DrawString("?", new Font("Arial", 20), Brushes.Black, 15, 5);
                    }
                    pictureBoxPlayer.Image = new Bitmap(bmp);
                }
            }
        }

        public void SetFavorite(bool isFavorite)
        {
            IsFavorite = isFavorite;
            starLabel.Text = isFavorite ? "★" : "☆";
            starLabel.Visible = true;
            starLabel.BringToFront();
            FavoriteStatusChanged?.Invoke(this, this);
        }

        private void ToggleFavorite()
        {
            SetFavorite(!IsFavorite);
        }

        private void PlayerInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.DoDragDrop(this, DragDropEffects.Move);
            }
        }

        private void PlayerInfo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerInfo)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void PlayerInfo_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerInfo)))
            {
                PlayerInfo draggedPlayer = (PlayerInfo)e.Data.GetData(typeof(PlayerInfo));
                // Handle the drop - this will be implemented in Form1
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (starLabel != null)
            {
                starLabel.Location = new Point(lblPlayerName.Right + 5, lblPlayerName.Top - 2);
                starLabel.BringToFront();
            }
        }
    }
}
