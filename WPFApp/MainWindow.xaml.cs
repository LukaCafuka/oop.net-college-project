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
    }
}
