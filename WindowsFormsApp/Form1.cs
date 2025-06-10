using DataLayer.DataHandling;
using QuickType;
using DataLayer.JsonModels;
using System.Reflection;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Linq;

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

        private void UpdatePanelLayout(Panel panel)
        {
            // Sort controls by player number
            var sortedControls = panel.Controls.Cast<PlayerInfo>()
                .OrderBy(p => p.Player.ShirtNumber)
                .ToList();

            // Clear and re-add controls in sorted order
            panel.Controls.Clear();
            int yOffset = 10;
            foreach (var control in sortedControls)
            {
                control.Location = new Point(10, yOffset);
                panel.Controls.Add(control);
                yOffset += control.Height + 10;
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
                Panel targetPanel = (Panel)sender;
                PlayerInfo draggedPlayer = (PlayerInfo)e.Data.GetData(typeof(PlayerInfo));
                Panel sourcePanel = (Panel)draggedPlayer.Parent;

                // Get all selected players
                var selectedPlayers = sourcePanel.Controls.OfType<PlayerInfo>()
                    .Where(p => p.IsSelected)
                    .ToList();

                // If no players are selected, just use the dragged player
                if (!selectedPlayers.Any())
                {
                    selectedPlayers.Add(draggedPlayer);
                }

                // Check if we're moving to favorites panel
                bool isMovingToFavorites = targetPanel == pnlPlayerFavourites;
                if (isMovingToFavorites)
                {
                    int currentFavorites = pnlPlayerFavourites.Controls.Count;
                    int newFavorites = selectedPlayers.Count(p => !p.IsFavorite);
                    if (currentFavorites + newFavorites > MAX_FAVORITE_PLAYERS)
                    {
                        MessageBox.Show($"You can only have {MAX_FAVORITE_PLAYERS} favorite players!", 
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Move all selected players
                foreach (var player in selectedPlayers.ToList()) // Create a copy of the list to avoid modification during iteration
                {
                    // Remove from source panel
                    sourcePanel.Controls.Remove(player);
                    // Add to target panel
                    targetPanel.Controls.Add(player);
                    // Update favorite status
                    player.SetFavorite(isMovingToFavorites);
                }

                // Update layouts
                UpdatePanelLayout(pnlPlayers);
                UpdatePanelLayout(pnlPlayerFavourites);

                SaveFavorites();
            }
        }

        private void SortPanel(Panel panel)
        {
            var players = panel.Controls.OfType<PlayerInfo>().ToList();
            players.Sort((a, b) => string.Compare(a.Player.Name, b.Player.Name));
            panel.Controls.Clear();
            foreach (var player in players)
            {
                panel.Controls.Add(player);
            }
        }

        private void SaveFavorites()
        {
            userFavourites.Clear();
            foreach (PlayerInfo player in pnlPlayerFavourites.Controls)
            {
                if (player.IsFavorite)
                {
                    userFavourites.Add(player.Player.Name);
                }
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
                foreach (var player in sortedPlayers)
                {
                    string playerName = new System.Globalization.CultureInfo("en-US", false)
                        .TextInfo.ToTitleCase(player.Name.ToLower());
                    player.Name = playerName;
                    var playerInfo = new PlayerInfo(player);
                    playerInfo.Width = pnlPlayers.Width - 30;
                    playerInfo.FavoriteStatusChanged += PlayerInfo_FavoriteStatusChanged;

                    // Check if player is in favorites
                    if (userFavourites.Contains(player.Name))
                    {
                        playerInfo.SetFavorite(true);
                        favoritePlayers.Add(playerInfo);
                        pnlPlayerFavourites.Controls.Add(playerInfo);
                    }
                    else
                    {
                        pnlPlayers.Controls.Add(playerInfo);
                    }
                }

                // Update panel layouts
                UpdatePanelLayout(pnlPlayers);
                UpdatePanelLayout(pnlPlayerFavourites);

                UpdateRankings();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadPlayers: Error loading players: {ex}");
                MessageBox.Show($"Error loading players: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlayerInfo_FavoriteStatusChanged(object sender, PlayerInfo playerInfo)
        {
            if (playerInfo.IsFavorite)
            {
                // Moving to favorites panel
                if (pnlPlayerFavourites.Controls.Count >= MAX_FAVORITE_PLAYERS)
                {
                    MessageBox.Show($"You can only have {MAX_FAVORITE_PLAYERS} favorite players!", 
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    playerInfo.SetFavorite(false);
                    return;
                }

                pnlPlayers.Controls.Remove(playerInfo);
                pnlPlayerFavourites.Controls.Add(playerInfo);
                favoritePlayers.Add(playerInfo);
            }
            else
            {
                // Moving back to main panel
                pnlPlayerFavourites.Controls.Remove(playerInfo);
                pnlPlayers.Controls.Add(playerInfo);
                favoritePlayers.Remove(playerInfo);
            }

            // Update layouts
            UpdatePanelLayout(pnlPlayers);
            UpdatePanelLayout(pnlPlayerFavourites);

            // Save changes
            SaveFavorites();
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
            UpdateRankingList(pnlGoalsRanking, goalsRanking, "Goals");
            UpdateRankingList(pnlYellowCardsRanking, yellowCardsRanking, "Yellow Cards");
            UpdateAttendanceList(pnlAttendanceRanking, attendanceRanking);
        }

        private void UpdateRankingList<T>(Panel panel, IEnumerable<T> ranking, string title)
        {
            panel.Controls.Clear();
            
            // Add title
            var titleLabel = new Label
            {
                Text = $"{title} Ranking:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(5, 5)
            };
            panel.Controls.Add(titleLabel);

            // Add rank items
            int yOffset = 30;
            foreach (var item in ranking)
            {
                string playerName = "";
                int count = 0;

                // Use type checking instead of dynamic
                if (item is { } rankingItem)
                {
                    var properties = rankingItem.GetType().GetProperties();
                    var playerProperty = properties.FirstOrDefault(p => p.Name == "Player");
                    var countProperty = properties.FirstOrDefault(p => p.Name == "Goals" || p.Name == "Cards");

                    if (playerProperty != null && countProperty != null)
                    {
                        var playerValue = playerProperty.GetValue(rankingItem);
                        if (playerValue != null)
                        {
                            playerName = playerValue.ToString() ?? "";
                            count = Convert.ToInt32(countProperty.GetValue(rankingItem) ?? 0);

                            var rankItem = new RankListItem(playerName, count);
                            rankItem.Location = new Point(5, yOffset);
                            panel.Controls.Add(rankItem);
                            yOffset += rankItem.Height + 5;
                        }
                    }
                }
            }
        }

        private void UpdateAttendanceList(Panel panel, List<Matches> matches)
        {
            panel.Controls.Clear();

            // Add title
            var titleLabel = new Label
            {
                Text = "Attendance Ranking:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(5, 5)
            };
            panel.Controls.Add(titleLabel);

            // Add match items
            int yOffset = 30;
            foreach (var match in matches)
            {
                var matchItem = new Label
                {
                    Text = $"{match.Location} - {match.Attendance:N0} - {match.HomeTeam.Country} vs {match.AwayTeam.Country}",
                    AutoSize = true,
                    Location = new Point(5, yOffset)
                };
                panel.Controls.Add(matchItem);
                yOffset += matchItem.Height + 5;
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
                foreach (Control control in pnlGoalsRanking.Controls)
                {
                    if (control is RankListItem rankItem)
                    {
                        e.Graphics.DrawString($"{rankItem.Text} - Count: {rankItem.Count}", contentFont, Brushes.Black, 50, yPos);
                        yPos += 20;
                    }
                }

                // Print Yellow Cards Ranking
                yPos += 30;
                e.Graphics.DrawString("Yellow Cards Ranking", titleFont, Brushes.Black, 50, yPos);
                yPos += 30;
                foreach (Control control in pnlYellowCardsRanking.Controls)
                {
                    if (control is RankListItem rankItem)
                    {
                        e.Graphics.DrawString($"{rankItem.Text} - Count: {rankItem.Count}", contentFont, Brushes.Black, 50, yPos);
                        yPos += 20;
                    }
                }

                // Print Attendance Ranking
                yPos += 30;
                e.Graphics.DrawString("Attendance Ranking", titleFont, Brushes.Black, 50, yPos);
                yPos += 30;
                foreach (Control control in pnlAttendanceRanking.Controls)
                {
                    if (control is Label label)
                    {
                        e.Graphics.DrawString(label.Text, contentFont, Brushes.Black, 50, yPos);
                        yPos += 20;
                    }
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
