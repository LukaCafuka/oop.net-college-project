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


            string[] filePaths = Directory.GetFiles(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, $"Pictures/Saved/"));
            for (int i = 0; i < filePaths.Length; i++)
            {
                string exactFile = ($"{filePaths[i].Substring(filePaths[i].IndexOf("d/") + 2)}");
                string parsedFile = exactFile.Remove(exactFile.IndexOf('.'));
                if (player.Name == parsedFile)
                {
                    
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
