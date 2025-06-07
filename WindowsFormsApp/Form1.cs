using DataLayer.DataHandling;
using QuickType;
using DataLayer.JsonModels;
using System.Reflection;
using System.Drawing.Printing;
using System.Diagnostics;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        //Print variables
        List<TeamEvent> userRankedPlayerControlsList = new List<TeamEvent>();
        List<Matches> userRankedStadiumControlsList = new List<Matches>();

        //Favourite List
        HashSet<string> userFavourites = new HashSet<string>();
        private const int MAX_FAVORITE_PLAYERS = 3;
        private List<PlayerInfo> favoritePlayers = new List<PlayerInfo>();

        public Form1()
        {
            try
            {
                Debug.WriteLine("Form1: Starting initialization...");
                InitializeComponent();
                Debug.WriteLine("Form1: Component initialization complete");

                ConfigHandling.OnConfigFileMissing += HandleConfigFileMissing;
                Debug.WriteLine("Form1: Config file missing handler registered");

                // First check if config exists and load it
                Debug.WriteLine("Form1: Checking if config exists...");
                if (ConfigHandling.ConfigExists())
                {
                    Debug.WriteLine("Form1: Loading config...");
                    ConfigHandling.LoadConfig();
                }

                // Then load culture and favorites
                Debug.WriteLine("Form1: Loading culture...");
                CultureHandling.LoadCulture();
                Debug.WriteLine("Form1: Loading favorites...");
                userFavourites = FavouriteHandling.LoadFavourites();

                // Subscribe to ComboBox event ONCE
                Debug.WriteLine("Form1: Setting up event handlers...");
                cbChampionship.SelectedIndexChanged += CbChampionship_SelectedIndexChanged;

                // Initialize panels for drag and drop
                pnlPlayers.AllowDrop = true;
                pnlPlayerFavourites.AllowDrop = true;
                pnlPlayers.DragEnter += Panel_DragEnter;
                pnlPlayerFavourites.DragEnter += Panel_DragEnter;
                pnlPlayers.DragDrop += Panel_DragDrop;
                pnlPlayerFavourites.DragDrop += Panel_DragDrop;

                // Add keyboard shortcuts
                this.KeyPreview = true;
                this.KeyDown += Form1_KeyDown;

                // Set panel properties
                pnlPlayers.BorderStyle = BorderStyle.FixedSingle;
                pnlPlayerFavourites.BorderStyle = BorderStyle.FixedSingle;
                pnlPlayers.AutoScroll = true;
                pnlPlayerFavourites.AutoScroll = true;

                Debug.WriteLine("Form1: Initialization complete");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Form1: Error in constructor: {ex}");
                MessageBox.Show($"An error occurred during form initialization: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw; // Re-throw to be caught by Program.Main
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Handle Enter key for confirmation dialogs
                if (ActiveControl is Button btn && btn.DialogResult == DialogResult.OK)
                {
                    btn.PerformClick();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                // Handle Escape key for cancellation
                if (ActiveControl is Button btn && btn.DialogResult == DialogResult.Cancel)
                {
                    btn.PerformClick();
                }
            }
        }

        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerInfo)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerInfo)))
            {
                PlayerInfo draggedPlayer = (PlayerInfo)e.Data.GetData(typeof(PlayerInfo));
                Panel targetPanel = (Panel)sender;
                Panel sourcePanel = draggedPlayer.Parent as Panel;

                if (sourcePanel != targetPanel)
                {
                    if (targetPanel == pnlPlayerFavourites && favoritePlayers.Count >= MAX_FAVORITE_PLAYERS)
                    {
                        MessageBox.Show($"You can only have {MAX_FAVORITE_PLAYERS} favorite players!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    sourcePanel.Controls.Remove(draggedPlayer);
                    targetPanel.Controls.Add(draggedPlayer);

                    // Calculate new position
                    int yOffset = 10;
                    foreach (Control control in targetPanel.Controls)
                    {
                        if (control != draggedPlayer)
                        {
                            yOffset += control.Height + 10;
                        }
                    }
                    draggedPlayer.Location = new Point(10, yOffset);

                    if (targetPanel == pnlPlayerFavourites)
                    {
                        favoritePlayers.Add(draggedPlayer);
                        draggedPlayer.SetFavorite(true);
                    }
                    else
                    {
                        favoritePlayers.Remove(draggedPlayer);
                        draggedPlayer.SetFavorite(false);
                    }

                    SaveFavorites();
                }
            }
        }

        private void SaveFavorites()
        {
            userFavourites.Clear();
            foreach (var player in favoritePlayers)
            {
                userFavourites.Add(player.Player.Name);
            }
            FavouriteHandling.SaveFavourites(userFavourites);
        }

        private async void LoadPlayers(string country)
        {
            try
            {
                pnlPlayers.Controls.Clear();
                pnlPlayerFavourites.Controls.Clear();
                favoritePlayers.Clear();

                HashSet<Matches> matches = await ApiDataHandling.LoadJsonMatches();

                // Use a dictionary to ensure unique players by name
                Dictionary<string, StartingEleven> playerDict = new Dictionary<string, StartingEleven>();
                userRankedPlayerControlsList = new List<TeamEvent>();
                userRankedStadiumControlsList = new List<Matches>();

                foreach (var match in matches)
                {
                    if (match.HomeTeamStatistics.Country == country)
                    {
                        userRankedStadiumControlsList.Add(match);
                        foreach (var player in match.HomeTeamStatistics.StartingEleven)
                            if (!playerDict.ContainsKey(player.Name)) playerDict[player.Name] = player;
                        foreach (var player in match.HomeTeamStatistics.Substitutes)
                            if (!playerDict.ContainsKey(player.Name)) playerDict[player.Name] = player;
                        foreach (var evt in match.HomeTeamEvents)
                            userRankedPlayerControlsList.Add(evt);
                    }
                    if (match.AwayTeamStatistics.Country == country)
                    {
                        userRankedStadiumControlsList.Add(match);
                        foreach (var player in match.AwayTeamStatistics.StartingEleven)
                            if (!playerDict.ContainsKey(player.Name)) playerDict[player.Name] = player;
                        foreach (var player in match.AwayTeamStatistics.Substitutes)
                            if (!playerDict.ContainsKey(player.Name)) playerDict[player.Name] = player;
                        foreach (var evt in match.AwayTeamEvents)
                            userRankedPlayerControlsList.Add(evt);
                    }
                }

                var sortedPlayers = playerDict.Values.OrderBy(p => p.ShirtNumber).ToList();
                int yOffset = 10;
                foreach (var player in sortedPlayers)
                {
                    string playerName = new System.Globalization.CultureInfo("en-US", false)
                        .TextInfo.ToTitleCase(player.Name.ToLower());
                    player.Name = playerName;
                    var playerInfo = new PlayerInfo(player);
                    playerInfo.Location = new Point(10, yOffset);
                    playerInfo.Width = pnlPlayers.Width - 30;

                    // Check if player is in favorites
                    if (userFavourites.Contains(player.Name))
                    {
                        pnlPlayerFavourites.Controls.Add(playerInfo);
                        playerInfo.SetFavorite(true);
                        favoritePlayers.Add(playerInfo);
                    }
                    else
                    {
                        pnlPlayers.Controls.Add(playerInfo);
                        playerInfo.SetFavorite(false);
                    }
                    yOffset += playerInfo.Height + 10;
                }

                UpdateRankings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading players: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRankings()
        {
            // Goals ranking
            var goalsRanking = userRankedPlayerControlsList
                .Where(e => e.TypeOfEvent == TypeOfEvent.Goal)
                .GroupBy(e => e.Player)
                .Select(g => new { Player = g.Key, Goals = g.Count() })
                .OrderByDescending(x => x.Goals)
                .ToList();

            // Yellow cards ranking
            var yellowCardsRanking = userRankedPlayerControlsList
                .Where(e => e.TypeOfEvent == TypeOfEvent.YellowCard)
                .GroupBy(e => e.Player)
                .Select(g => new { Player = g.Key, Cards = g.Count() })
                .OrderByDescending(x => x.Cards)
                .ToList();

            // Attendance ranking
            var attendanceRanking = userRankedStadiumControlsList
                .OrderByDescending(m => m.Attendance)
                .ToList();

            // Update UI with rankings
            UpdateRankingList(lstGoalsRanking, goalsRanking, "Goals");
            UpdateRankingList(lstYellowCardsRanking, yellowCardsRanking, "Yellow Cards");
            UpdateAttendanceList(lstAttendanceRanking, attendanceRanking);
        }

        private void UpdateRankingList<T>(ListBox listBox, IEnumerable<T> ranking, string title)
        {
            listBox.Items.Clear();
            listBox.Items.Add($"{title} Ranking:");
            foreach (var item in ranking)
            {
                listBox.Items.Add(item.ToString());
            }
        }

        private void UpdateAttendanceList(ListBox listBox, List<Matches> matches)
        {
            listBox.Items.Clear();
            listBox.Items.Add("Attendance Ranking:");
            foreach (var match in matches)
            {
                listBox.Items.Add($"{match.Location} - {match.Attendance} - {match.HomeTeam} vs {match.AwayTeam}");
            }
        }

        private void btnPrintRankings_Click(object sender, EventArgs e)
        {
            PrintRankings();
        }

        private void PrintRankings()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) =>
            {
                float yPos = 50;
                Font titleFont = new Font("Arial", 16, FontStyle.Bold);
                Font contentFont = new Font("Arial", 12);

                // Print Goals Ranking
                e.Graphics.DrawString("Goals Ranking", titleFont, Brushes.Black, 50, yPos);
                yPos += 30;
                foreach (var item in lstGoalsRanking.Items)
                {
                    e.Graphics.DrawString(item.ToString(), contentFont, Brushes.Black, 50, yPos);
                    yPos += 20;
                }

                // Print Yellow Cards Ranking
                yPos += 30;
                e.Graphics.DrawString("Yellow Cards Ranking", titleFont, Brushes.Black, 50, yPos);
                yPos += 30;
                foreach (var item in lstYellowCardsRanking.Items)
                {
                    e.Graphics.DrawString(item.ToString(), contentFont, Brushes.Black, 50, yPos);
                    yPos += 20;
                }

                // Print Attendance Ranking
                yPos += 30;
                e.Graphics.DrawString("Attendance Ranking", titleFont, Brushes.Black, 50, yPos);
                yPos += 30;
                foreach (var item in lstAttendanceRanking.Items)
                {
                    e.Graphics.DrawString(item.ToString(), contentFont, Brushes.Black, 50, yPos);
                    yPos += 20;
                }
            };

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pd;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void CbChampionship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChampionship.SelectedItem != null)
            {
                // Extract country name from ComboBox item (e.g., "England (ENG)")
                string selected = cbChampionship.SelectedItem.ToString();
                string country = selected.Split('(')[0].Trim();
                
                // Only update the index for UI state, don't save to config
                ConfigFile.countryIndex = cbChampionship.SelectedIndex;
                
                LoadPlayers(country);
            }
        }

        private async void LoadChampionship()
        {
            try
            {
                cbChampionship.Items.Clear();
                HashSet<TeamResults> teams = await ApiDataHandling.LoadJsonTeams();
                foreach (var team in teams.OrderByDescending(t => t.Points).ThenByDescending(t => t.GoalDifferential))
                {
                    cbChampionship.Items.Add(team.FormatForComboBox());
                }
                if (cbChampionship.Items.Count > 0)
                {
                    // If we have a saved country index, try to select it
                    if (ConfigFile.countryIndex >= 0 && ConfigFile.countryIndex < cbChampionship.Items.Count)
                    {
                        cbChampionship.SelectedIndex = ConfigFile.countryIndex;
                    }
                    else
                    {
                        cbChampionship.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading championship: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleConfigFileMissing()
        {
            using (var configForm = new ConfigForm())
            {
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    ConfigHandling.SaveConfig();
                }
                else
                {
                    MessageBox.Show("Configuration is required to proceed. The application will now close.",
                            "Configuration Required",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    Application.Exit();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = CultureHandling.GetString("MainFormTitle");
            LoadChampionship();
            // Do not call LoadPlayers here, it will be called by ComboBox selection
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FavouriteHandling.SaveFavourites(userFavourites);
                Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnOpenConfig_Click(object sender, EventArgs e)
        {
            using (var configForm = new ConfigForm())
            {
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    // Reload the championship data since settings might have changed
                    LoadChampionship();
                }
            }
        }
    }
}
