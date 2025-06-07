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
        public bool IsSelected { get; private set; }
        private ContextMenuStrip contextMenu;
        private Label starLabel;

        public event EventHandler<PlayerInfo> FavoriteStatusChanged;
        public event EventHandler<PlayerInfo> SelectionChanged;

        public PlayerInfo(StartingEleven player)
        {
            InitializeComponent();
            Player = player;
            InitializeContextMenu();
            InitializeStarLabel();
            SetData(player);
            this.MouseDown += PlayerInfo_MouseDown;
            this.MouseClick += PlayerInfo_MouseClick;
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            var favoriteMenuItem = new ToolStripMenuItem(IsFavorite ? "Remove from favorites" : "Set as favorite");
            favoriteMenuItem.Click += (s, e) => ToggleFavorite();
            contextMenu.Items.Add(favoriteMenuItem);
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

        private void ToggleFavorite()
        {
            SetFavorite(!IsFavorite);
            FavoriteStatusChanged?.Invoke(this, this);
        }

        private void PlayerInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                // Toggle selection of this player
                ToggleSelection();
            }
        }

        private void PlayerInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // If no players are selected, select this one
                if (!IsSelected)
                {
                    // Clear other selections
                    if (this.Parent is Panel panel)
                    {
                        foreach (Control control in panel.Controls)
                        {
                            if (control is PlayerInfo playerInfo && playerInfo != this)
                            {
                                playerInfo.ClearSelection();
                            }
                        }
                    }
                    ToggleSelection();
                }
                this.DoDragDrop(this, DragDropEffects.Move);
            }
        }

        public void ToggleSelection()
        {
            IsSelected = !IsSelected;
            this.BackColor = IsSelected ? Color.LightBlue : SystemColors.Control;
            SelectionChanged?.Invoke(this, this);
        }

        public void ClearSelection()
        {
            if (IsSelected)
            {
                IsSelected = false;
                this.BackColor = SystemColors.Control;
                SelectionChanged?.Invoke(this, this);
            }
        }

        public void SetFavorite(bool isFavorite)
        {
            IsFavorite = isFavorite;
            starLabel.Text = isFavorite ? "★" : "☆";
            starLabel.Visible = true;
            starLabel.BringToFront();

            // Update context menu text
            if (contextMenu.Items.Count > 0)
            {
                contextMenu.Items[0].Text = isFavorite ? "Remove from favorites" : "Set as favorite";
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
