using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayer.JsonModels;
using DataLayer.DataHandling;
using QuickType;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TeamResults> _teams = new();
        private List<Matches> _matches = new();
        private TeamResults? _selectedFavorite;
        private TeamResults? _selectedOpponent;

        public MainWindow()
        {
            InitializeComponent();
            ApplyResolution();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTeamsAndMatches();
            PopulateFavoriteComboBox();
        }

        private async Task LoadTeamsAndMatches()
        {
            var teamsSet = await ApiDataHandling.LoadJsonTeams();
            _teams = teamsSet.OrderBy(t => t.Country).ToList();
            var matchesSet = await ApiDataHandling.LoadJsonMatches();
            _matches = matchesSet.ToList();
        }

        private void PopulateFavoriteComboBox()
        {
            cbFavoriteTeam.ItemsSource = _teams;
            cbFavoriteTeam.SelectedValuePath = "FifaCode";

            // Preselect favorite from config
            if (!string.IsNullOrEmpty(ConfigFile.country))
            {
                var fav = _teams.FirstOrDefault(t => t.Country == ConfigFile.country);
                if (fav != null)
                    cbFavoriteTeam.SelectedItem = fav;
            }
        }

        private void cbFavoriteTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFavorite = cbFavoriteTeam.SelectedItem as TeamResults;
            ConfigFile.country = _selectedFavorite?.Country;
            PopulateOpponentComboBox();
            UpdateResultLabel();
            DrawLineups();
        }

        private void PopulateOpponentComboBox()
        {
            cbOpponentTeam.ItemsSource = null;
            if (_selectedFavorite == null) return;
            // Find all teams that played against the favorite
            var opponents = _matches
                .Where(m => m.HomeTeamCountry == _selectedFavorite.Country || m.AwayTeamCountry == _selectedFavorite.Country)
                .Select(m => m.HomeTeamCountry == _selectedFavorite.Country ? m.AwayTeamCountry : m.HomeTeamCountry)
                .Distinct()
                .ToList();
            var opponentTeams = _teams.Where(t => opponents.Contains(t.Country)).ToList();
            cbOpponentTeam.ItemsSource = opponentTeams;
            cbOpponentTeam.SelectedValuePath = "FifaCode";

            // Preselect from config
            if (!string.IsNullOrEmpty(ConfigFile.versusCountry))
            {
                var opp = opponentTeams.FirstOrDefault(t => t.Country == ConfigFile.versusCountry);
                if (opp != null)
                    cbOpponentTeam.SelectedItem = opp;
            }
        }

        private void cbOpponentTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedOpponent = cbOpponentTeam.SelectedItem as TeamResults;
            ConfigFile.versusCountry = _selectedOpponent?.Country;
            UpdateResultLabel();
            DrawLineups();
        }

        private void UpdateResultLabel()
        {
            lblResult.Text = string.Empty;
            if (_selectedFavorite == null || _selectedOpponent == null) return;
            // Find the match
            var match = _matches.FirstOrDefault(m =>
                (m.HomeTeamCountry == _selectedFavorite.Country && m.AwayTeamCountry == _selectedOpponent.Country) ||
                (m.HomeTeamCountry == _selectedOpponent.Country && m.AwayTeamCountry == _selectedFavorite.Country));
            if (match != null && match.HomeTeam != null && match.AwayTeam != null)
            {
                int homeGoals = (int)match.HomeTeam.Goals;
                int awayGoals = (int)match.AwayTeam.Goals;
                string result = match.HomeTeamCountry == _selectedFavorite.Country
                    ? $"{homeGoals} : {awayGoals}"
                    : $"{awayGoals} : {homeGoals}";
                lblResult.Text = result;
            }
            else
            {
                lblResult.Text = "-";
            }
        }

        private void btnFavoriteInfo_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedFavorite != null)
                ShowTeamInfoWindow(_selectedFavorite);
        }

        private void btnOpponentInfo_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedOpponent != null)
                ShowTeamInfoWindow(_selectedOpponent);
        }

        private void ShowTeamInfoWindow(TeamResults team)
        {
            try
            {
                var win = new TeamInfoWindow(team);
                win.Show(); // non-modal
                win.AnimateIn();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ShowTeamInfoWindow: {ex}\n{ex.StackTrace}");
                MessageBox.Show($"Error: {ex.Message}\n\n{ex.StackTrace}", "Info Window Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOpenSettings_Click(object sender, RoutedEventArgs e)
        {
            var win = new SettingsWindow();
            win.Show();
        }

        private void ApplyResolution()
        {
            if (string.IsNullOrEmpty(ConfigFile.resolution))
            {
                return;
            }

            switch (ConfigFile.resolution)
            {
                case "Fullscreen":
                    WindowState = WindowState.Maximized;
                    break;
                case "1280 x 720":
                    Width = 1280;
                    Height = 720;
                    break;
                case "1600 x 900":
                    Width = 1600;
                    Height = 900;
                    break;
                case "1920 x 1080":
                    Width = 1920;
                    Height = 1080;
                    break;
            }
        }

        private void DrawLineups()
        {
            canvasField.Children.Clear();
            if (_selectedFavorite == null || _selectedOpponent == null) return;

            // Find the match between the two teams
            var match = _matches.FirstOrDefault(m =>
                (m.HomeTeamCountry == _selectedFavorite.Country && m.AwayTeamCountry == _selectedOpponent.Country) ||
                (m.HomeTeamCountry == _selectedOpponent.Country && m.AwayTeamCountry == _selectedFavorite.Country));
            if (match == null) return;

            // Determine which team is home/away
            bool favoriteIsHome = match.HomeTeamCountry == _selectedFavorite.Country;
            var favoriteStats = favoriteIsHome ? match.HomeTeamStatistics : match.AwayTeamStatistics;
            var opponentStats = favoriteIsHome ? match.AwayTeamStatistics : match.HomeTeamStatistics;

            DrawTeamLineup(favoriteStats?.StartingEleven, true, match, favoriteIsHome);
            DrawTeamLineup(opponentStats?.StartingEleven, false, match, !favoriteIsHome);
        }

        private void DrawTeamLineup(StartingEleven[]? players, bool isHomeTeam, Matches match, bool isHomeTeamInMatch)
        {
            if (players == null) return;

            double fieldWidth = 800;
            double fieldHeight = 400;

            // X positions for home and away (forwards deeper into opponent's half)
            var homeX = new Dictionary<Position, double>
            {
                { Position.Goalie, 60 },
                { Position.Defender, 180 },
                { Position.Midfield, 350 },
                { Position.Forward, fieldWidth - 120 }
            };
            var awayX = new Dictionary<Position, double>
            {
                { Position.Goalie, fieldWidth - 60 },
                { Position.Defender, fieldWidth - 180 },
                { Position.Midfield, fieldWidth - 350 },
                { Position.Forward, 120 }
            };
            var xPositions = isHomeTeam ? homeX : awayX;

            var teamEvents = isHomeTeamInMatch ? match.HomeTeamEvents : match.AwayTeamEvents;

            foreach (var group in players.GroupBy(p => p.Position))
            {
                double x = xPositions[group.Key];
                int count = group.Count();

                // Distribute players along the Y axis (width of the field), centered
                double yStart = 60;
                double yEnd = fieldHeight - 60;
                double yStep = (yEnd - yStart) / (count + 1);

                int i = 1;
                foreach (var player in group)
                {
                    var control = new PlayerControl
                    {
                        PlayerName = player.Name,
                        ShirtNumber = player.ShirtNumber.ToString(),
                        PlayerImage = GetPlayerImage(player.Name),
                        PlayerData = player,
                        Goals = GetPlayerGoals(player.Name, teamEvents),
                        YellowCards = GetPlayerYellowCards(player.Name, teamEvents)
                    };
                    double y = yStart + yStep * i;
                    Canvas.SetLeft(control, x - 20);
                    Canvas.SetTop(control, y - 20);
                    canvasField.Children.Add(control);
                    i++;
                }
            }
        }

        private int GetPlayerGoals(string playerName, TeamEvent[]? events)
        {
            if (events == null) return 0;
            return events.Count(e => e.TypeOfEvent == TypeOfEvent.Goal && e.Player == playerName);
        }

        private int GetPlayerYellowCards(string playerName, TeamEvent[]? events)
        {
            if (events == null) return 0;
            return events.Count(e => e.TypeOfEvent == TypeOfEvent.YellowCard && e.Player == playerName);
        }

        private ImageSource GetPlayerImage(string playerName)
        {
            // Try to load an image from Resources/Players/{playerName}.png, fallback to a default
            try
            {
                string safeName = playerName.Replace(' ', '_');
                string path = $"pack://application:,,,/Resources/Players/{safeName}.png";
                return new BitmapImage(new Uri(path));
            }
            catch
            {
                // Use a generic footballer icon (online PNG)
                return new BitmapImage(new Uri("https://cdn-icons-png.flaticon.com/512/616/616494.png"));
            }
        }
    }
}
