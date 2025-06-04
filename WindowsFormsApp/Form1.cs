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

            // Subscribe to ComboBox event ONCE
            cbChampionship.SelectedIndexChanged += CbChampionship_SelectedIndexChanged;
        }

        private async void LoadPlayers(string country)
        {
            try
            {
                pnlPlayers.Controls.Clear();
                pnlPlayerFavourites.Controls.Clear();

                HashSet<Matches> matches = await ApiDataHandling.LoadJsonMatches();

                // Use a dictionary to ensure unique players by name
                Dictionary<string, StartingEleven> playerDict = new Dictionary<string, StartingEleven>();
                userRankedPlayerControlsList = new List<TeamEvent>();
                userRankedStadiumControlsList = new List<Matches>();

                int matchCount = 0;
                foreach (var match in matches)
                {
                    if (match.HomeTeamStatistics.Country == country)
                    {
                        matchCount++;
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
                        matchCount++;
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
                    pnlPlayers.Controls.Add(playerInfo);
                    yOffset += playerInfo.Height + 10;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading players: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbChampionship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChampionship.SelectedItem != null)
            {
                // Extract country name from ComboBox item (e.g., "England (ENG)")
                string selected = cbChampionship.SelectedItem.ToString();
                string country = selected.Split('(')[0].Trim();
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
    }
}
