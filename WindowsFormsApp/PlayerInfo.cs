﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickType;
using DataLayer.DataHandling;
using DataLayer.JsonModels;
using System.IO;

namespace WindowsFormsApp
{
    public partial class PlayerInfo : UserControl
    {
        public StartingEleven Player { get; private set; }
        public bool IsFavorite { get; private set; }
        public bool IsSelected { get; private set; }
        private ContextMenuStrip contextMenu;
        private Label starLabel;
        private PictureBox playerImage;
        private PlayerImageManager imageManager;

        public event EventHandler<PlayerInfo> FavoriteStatusChanged;
        public event EventHandler<PlayerInfo> SelectionChanged;

        public PlayerInfo(StartingEleven player)
        {
            InitializeComponent();
            Player = player;
            imageManager = new PlayerImageManager();
            InitializeContextMenu();
            InitializeStarLabel();
            InitializePlayerImage();
            SetData(player);
            this.MouseDown += PlayerInfo_MouseDown;
            this.MouseClick += PlayerInfo_MouseClick;
        }

        private void InitializePlayerImage()
        {
            playerImage = new PictureBox
            {
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            playerImage.Click += PlayerImage_Click;
            this.Controls.Add(playerImage);
            LoadPlayerImage();
        }

        private void LoadPlayerImage()
        {
            try
            {
                // Load the image in a background task
                Task.Run(() =>
                {
                    var newImage = imageManager.LoadPlayerImage(Player.Name);
                    
                    // Update the UI on the main thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (playerImage.Image != null)
                        {
                            var oldImage = playerImage.Image;
                            playerImage.Image = newImage;
                            oldImage.Dispose();
                        }
                        else
                        {
                            playerImage.Image = newImage;
                        }
                    });
                });
            }
            catch (Exception)
            {
                // If image loading fails, it will use the default image
            }
        }

        private void PlayerImage_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs me && me.Button == MouseButtons.Right)
            {
                return; // Let the context menu handle it
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Select Player Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Load and resize the image in a background task
                        Task.Run(() =>
                        {
                            using (var originalImage = Image.FromFile(openFileDialog.FileName))
                            {
                                // Create a new bitmap for the resized image
                                var resizedImage = new Bitmap(80, 80);
                                using (var graphics = Graphics.FromImage(resizedImage))
                                {
                                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    graphics.DrawImage(originalImage, 0, 0, 80, 80);
                                }

                                // Save the image
                                imageManager.SavePlayerImageAsync(Player.Name, resizedImage).Wait();

                                // Update the UI on the main thread
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (playerImage.Image != null)
                                    {
                                        var oldImage = playerImage.Image;
                                        playerImage.Image = resizedImage;
                                        oldImage.Dispose();
                                    }
                                    else
                                    {
                                        playerImage.Image = resizedImage;
                                    }
                                });
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            var favoriteMenuItem = new ToolStripMenuItem(IsFavorite ? "Remove from favorites" : "Set as favorite");
            favoriteMenuItem.Click += (s, e) => ToggleFavorite();
            contextMenu.Items.Add(favoriteMenuItem);

            var changeImageMenuItem = new ToolStripMenuItem("Change Image");
            changeImageMenuItem.Click += (s, e) => PlayerImage_Click(sender: null, e: EventArgs.Empty);
            contextMenu.Items.Add(changeImageMenuItem);

            this.ContextMenuStrip = contextMenu;
        }

        private void InitializeStarLabel()
        {
            starLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(this.Width - 30, 5),
                Text = "☆",
                ForeColor = Color.Gold,
                Visible = false,
                BackColor = Color.Transparent
            };
            this.Controls.Add(starLabel);
        }

        private void SetData(StartingEleven player)
        {
            lblName.Text = player.Name;
            lblShirtNumber.Text = player.ShirtNumber.ToString();
            lblPosition.Text = player.Position.ToString();
            lblCaptain.Text = player.Captain ? "C" : "";
            lblCaptain.Visible = player.Captain;
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
                starLabel.Location = new Point(this.Width - 30, 5);
            }
        }
    }
}
