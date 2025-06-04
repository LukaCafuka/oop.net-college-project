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
        public bool selected = false;

        public string FavouriteName { get; set; }

        public PlayerInfo(StartingEleven player)
        {
            InitializeComponent();
            Player = player;
            SetData(player);
        }
        private void SetData(StartingEleven player)
        {
            FavouriteName = player.Name;
            lblPlayerName.Text = player.Name;
            lblPlayerNumber.Text = player.ShirtNumber.ToString();
            lblPosition.Text = player.Position.ToString();
            lblCaptain.Text = player.Captain ? "Captain" : " ";
            lblFavourite.Text = selected ? "Favourite" : "Not Favourite ";

            // Use assets/images/players as the directory for player images
            string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "assets", "images", "players");
            if (Directory.Exists(imagesDir))
            {
                string[] filePaths = Directory.GetFiles(imagesDir);
                for (int i = 0; i < filePaths.Length; i++)
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePaths[i]);
                    if (player.Name == fileName)
                    {
                        // You can load the image here if you have a PictureBox, e.g.:
                        // pictureBoxPlayer.Image = Image.FromFile(filePaths[i]);
                    }
                }
            }
        }

        private void PlayersInfo_MouseDown(object sender, MouseEventArgs e)
        {
            PlayerInfo playerInfo = sender as PlayerInfo;
            if (e.Button == MouseButtons.Left)
            {
                playerInfo.DoDragDrop(playerInfo, DragDropEffects.Move);
                if (selected)
                {
                    lblFavourite.Text = "Favourite";
                }
                else
                {
                    lblFavourite.Text = "Not Favourite";
                }
            }
        }
    }
}
