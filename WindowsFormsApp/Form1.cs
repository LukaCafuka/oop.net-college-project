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
                HashSet<Matches> matches = await ApiDataHandling.LoadJsonMatches();

                StartingEleven player = new StartingEleven();
                Team rankedPlayer = new Team();
                Matches stadium = new Matches();

                HashSet<StartingEleven> playerList = new HashSet<StartingEleven>();
                HashSet<TeamEvent> rankedPlayerList = new HashSet<TeamEvent>();
                HashSet<Matches> rankedStadiumList = new HashSet<Matches>();

                HashSet<PlayerInfo> userPlayerControls = new HashSet<PlayerInfo>();

                userRankedPlayerControlsList = new List<TeamEvent>();
                userRankedStadiumControlsList = new List<Matches>();

                foreach (var players in matches)
                {
                    if (players.HomeTeamStatistics.Country == ConfigFile.country)
                    {
                        rankedStadiumList.Add(players);
                        foreach (var playerItem in players.HomeTeamStatistics.StartingEleven)
                        {
                            playerList.Add(playerItem);

                            foreach (var rankedItem in players.HomeTeamEvents)
                            {
                                rankedPlayerList.Add(rankedItem);
                            }
                        }
                        foreach (var playerItem in players.HomeTeamStatistics.Substitutes)
                        {
                            playerList.Add(playerItem);

                            foreach (var rankedItem in players.HomeTeamEvents)
                            {
                                rankedPlayerList.Add(rankedItem);
                            }
                        }
                    }
                    if (players.AwayTeamStatistics.Country == ConfigFile.country)
                    {
                        rankedStadiumList.Add(players);
                        foreach (var playerItem in players.AwayTeamStatistics.StartingEleven)
                        {
                            playerList.Add(playerItem);

                            foreach (var rankedItem in players.AwayTeamEvents)
                            {
                                rankedPlayerList.Add(rankedItem);
                            }
                        }
                        foreach (var playerItem in players.AwayTeamStatistics.Substitutes)
                        {
                            playerList.Add(playerItem);

                            foreach (var rankedItem in players.AwayTeamEvents)
                            {
                                rankedPlayerList.Add(rankedItem);
                            }
                        }
                    }
                }

                //Players
                IEnumerable<StartingEleven> sortedPlayers = playerList.OrderBy(item => item.ShirtNumber);

                foreach (var playerItem in sortedPlayers)
                {
                    rankedPlayer = new Team();
                    foreach (var rankedItem in rankedPlayerList)
                    {
                        if (playerItem.Name == rankedItem.Player)
                        {
                            string rankedPlayerName = rankedItem.Player;
                            rankedPlayerName = new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(rankedPlayerName.ToLower());
                        }
                    }
                    ;
                    string playerName = playerItem.Name;
                    playerName = new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(playerName.ToLower());
                    playerItem.Name = playerName;
                    userPlayerControls.Add(new PlayerInfo(playerItem));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void LoadChampionship()
        {
            try
            {
                HashSet<TeamResults> teams = await ApiDataHandling.LoadJsonTeams();

                foreach (var orderedTeam in teams)
                {
                    cbChampionship.Items.Add(orderedTeam.FormatForComboBox());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HandleConfigFileMissing()
        {
            using (var configForm = new ConfigForm()) // Replace ConfigForm with your actual form class name
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
                    Application.Exit(); // Exit the application completely
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
                return;
            }
        }
    }
}
