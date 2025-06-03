using DataLayer.DataHandling;
using QuickType;
using DataLayer.JsonModels;
using System.Reflection;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        //Print variables
        List<TeamEvent> userRankedPlayerControlsList = new List<TeamEvent>();
        List<Matches> userRankedStadiumControlsList = new List<Matches>();

        //Favourite List
        HashSet<string> userFavourites = new HashSet<string>();
        public Form1()
        {
            InitializeComponent();

            ConfigHandling.OnConfigFileMissing += HandleConfigFileMissing;

            ConfigHandling.ConfigExists();
            CultureHandling.LoadCulture();
            userFavourites = FavouriteHandling.LoadFavourites();
        }

        private async void LoadPlayers()
        {
            try
            {
                // Clear existing controls
                pnlPlayers.Controls.Clear();
                pnlPlayerFavourites.Controls.Clear();

                HashSet<Matches> matches = await ApiDataHandling.LoadJsonMatches();

                HashSet<StartingEleven> playerList = new HashSet<StartingEleven>();
                HashSet<TeamEvent> rankedPlayerList = new HashSet<TeamEvent>();
                HashSet<Matches> rankedStadiumList = new HashSet<Matches>();

                // Reset the lists
                userRankedPlayerControlsList = new List<TeamEvent>();
                userRankedStadiumControlsList = new List<Matches>();

                // Process matches
                foreach (var match in matches)
                {
                    if (match.HomeTeamStatistics.Country == ConfigFile.country)
                    {
                        rankedStadiumList.Add(match);
                        userRankedStadiumControlsList.Add(match);
                        
                        // Add starting eleven players
                        foreach (var player in match.HomeTeamStatistics.StartingEleven)
                        {
                            playerList.Add(player);
                        }
                        
                        // Add substitute players
                        foreach (var player in match.HomeTeamStatistics.Substitutes)
                        {
                            playerList.Add(player);
                        }
                        
                        // Add team events
                        foreach (var evt in match.HomeTeamEvents)
                        {
                            rankedPlayerList.Add(evt);
                            userRankedPlayerControlsList.Add(evt);
                        }
                    }
                    
                    if (match.AwayTeamStatistics.Country == ConfigFile.country)
                    {
                        rankedStadiumList.Add(match);
                        userRankedStadiumControlsList.Add(match);
                        
                        // Add starting eleven players
                        foreach (var player in match.AwayTeamStatistics.StartingEleven)
                        {
                            playerList.Add(player);
                        }
                        
                        // Add substitute players
                        foreach (var player in match.AwayTeamStatistics.Substitutes)
                        {
                            playerList.Add(player);
                        }
                        
                        // Add team events
                        foreach (var evt in match.AwayTeamEvents)
                        {
                            rankedPlayerList.Add(evt);
                            userRankedPlayerControlsList.Add(evt);
                        }
                    }
                }

                // Sort players by shirt number
                var sortedPlayers = playerList.OrderBy(p => p.ShirtNumber).ToList();

                // Create and add player controls
                int yOffset = 10;
                foreach (var player in sortedPlayers)
                {
                    // Format player name
                    string playerName = new System.Globalization.CultureInfo("en-US", false)
                        .TextInfo.ToTitleCase(player.Name.ToLower());
                    player.Name = playerName;

                    // Create player info control
                    var playerInfo = new PlayerInfo(player);
                    playerInfo.Location = new Point(10, yOffset);
                    playerInfo.Width = pnlPlayers.Width - 30; // Leave some margin
                    
                    // Add to panel
                    pnlPlayers.Controls.Add(playerInfo);
                    
                    // Update y offset for next control
                    yOffset += playerInfo.Height + 10;
                }

                // Add event handler for championship selection
                cbChampionship.SelectedIndexChanged += CbChampionship_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading players: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbChampionship_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Refresh player display when championship is changed
            LoadPlayers();
        }

        private async void LoadChampionship()
        {
            try
            {
                cbChampionship.Items.Clear();
                HashSet<TeamResults> teams = await ApiDataHandling.LoadJsonTeams();

                // Sort teams by points (descending) and then by goal difference (descending)
                foreach (var team in teams.OrderByDescending(t => t.Points).ThenByDescending(t => t.GoalDifferential))
                {
                    cbChampionship.Items.Add(team.FormatForComboBox());
                }

                // Select first item if available
                if (cbChampionship.Items.Count > 0)
                {
                    cbChampionship.SelectedIndex = 0;
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
            LoadPlayers();
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
    }
}
