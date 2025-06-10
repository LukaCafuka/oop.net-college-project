using DataLayer.DataHandling;
using DataLayer.JsonModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using QuickType;

namespace WPFApp
{
    public partial class SettingsWindow : Window
    {
        private string selectedResolution = "Fullscreen";
        private List<TeamResults> _teams = new();
        private List<Matches> _matches = new();

        public SettingsWindow()
        {
            InitializeComponent();
            Loaded += SettingsWindow_Loaded;
            LoadExistingSettings();
            cbFavoriteCountry.SelectionChanged += cbFavoriteCountry_SelectionChanged;
        }

        private async void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTeams();
            await LoadMatches();
            PopulateTeamComboBoxes();
        }

        private async System.Threading.Tasks.Task LoadTeams()
        {
            try
            {
                var teamsSet = await ApiDataHandling.LoadJsonTeams();
                _teams = teamsSet.OrderBy(t => t.Country).ToList();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _teams = new List<TeamResults>();
            }
        }

        private async System.Threading.Tasks.Task LoadMatches()
        {
            try
            {
                var matchesSet = await ApiDataHandling.LoadJsonMatches();
                _matches = matchesSet.ToList();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading matches: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _matches = new List<Matches>();
            }
        }

        private void PopulateTeamComboBoxes()
        {
            cbFavoriteCountry.ItemsSource = _teams;
            cbOpponentCountry.ItemsSource = _teams;
            // Preselect from config
            if (!string.IsNullOrEmpty(ConfigFile.country))
            {
                var fav = _teams.FirstOrDefault(t => t.Country == ConfigFile.country);
                if (fav != null)
                    cbFavoriteCountry.SelectedItem = fav;
            }
            if (!string.IsNullOrEmpty(ConfigFile.versusCountry))
            {
                var opp = _teams.FirstOrDefault(t => t.Country == ConfigFile.versusCountry);
                if (opp != null)
                    cbOpponentCountry.SelectedItem = opp;
            }
        }

        private void LoadExistingSettings()
        {
            // Load gender setting
            if (ConfigFile.gender == ConfigFile.Gender.Male)
            {
                rbMale.IsChecked = true;
            }
            else if (ConfigFile.gender == ConfigFile.Gender.Female)
            {
                rbFemale.IsChecked = true;
            }

            // Load language setting
            if (ConfigFile.language == "Croatian")
            {
                rbCroatian.IsChecked = true;
            }
            else if (ConfigFile.language == "English")
            {
                rbEnglish.IsChecked = true;
            }

            // Load resolution setting
            if (!string.IsNullOrEmpty(ConfigFile.resolution))
            {
                selectedResolution = ConfigFile.resolution;
                switch (ConfigFile.resolution)
                {
                    case "Fullscreen":
                        rbFullscreen.IsChecked = true;
                        break;
                    case "1280 x 720":
                        rbResolution1.IsChecked = true;
                        break;
                    case "1600 x 900":
                        rbResolution2.IsChecked = true;
                        break;
                    case "1920 x 1080":
                        rbResolution3.IsChecked = true;
                        break;
                }
            }
        }

        private void rbGender_Checked(object sender, RoutedEventArgs e)
        {
            if (rbMale.IsChecked == true)
                ConfigFile.gender = ConfigFile.Gender.Male;
            else if (rbFemale.IsChecked == true)
                ConfigFile.gender = ConfigFile.Gender.Female;
            // Reload teams for new gender
            _ = ReloadTeamsOnGenderChange();
        }

        private async System.Threading.Tasks.Task ReloadTeamsOnGenderChange()
        {
            await LoadTeams();
            PopulateTeamComboBoxes();
        }

        private void rbLanguage_Checked(object sender, RoutedEventArgs e)
        {
            if (rbCroatian.IsChecked == true)
            {
                ConfigFile.language = "Croatian";
                CultureHandling.SetCulture("hr-HR");
                LocalizationHelper.Instance.ChangeLanguage("hr-HR");
            }
            else if (rbEnglish.IsChecked == true)
            {
                ConfigFile.language = "English";
                CultureHandling.SetCulture("en-US");
                LocalizationHelper.Instance.ChangeLanguage("en-US");
            }
        }

        private void rbResolution_Checked(object sender, RoutedEventArgs e)
        {
            if (rbFullscreen.IsChecked == true)
            {
                selectedResolution = "Fullscreen";
            }
            else if (rbResolution1.IsChecked == true)
            {
                selectedResolution = "1280 x 720";
            }
            else if (rbResolution2.IsChecked == true)
            {
                selectedResolution = "1600 x 900";
            }
            else if (rbResolution3.IsChecked == true)
            {
                selectedResolution = "1920 x 1080";
            }
        }

        private void cbFavoriteCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFavorite = cbFavoriteCountry.SelectedItem as TeamResults;
            if (selectedFavorite == null)
            {
                cbOpponentCountry.ItemsSource = null;
                return;
            }
            // Find all teams that played against the favorite
            var opponents = _matches
                .Where(m => m.HomeTeamCountry == selectedFavorite.Country || m.AwayTeamCountry == selectedFavorite.Country)
                .Select(m => m.HomeTeamCountry == selectedFavorite.Country ? m.AwayTeamCountry : m.HomeTeamCountry)
                .Distinct()
                .ToList();
            var opponentTeams = _teams.Where(t => opponents.Contains(t.Country)).ToList();
            cbOpponentCountry.ItemsSource = opponentTeams;
            // Preselect from config if possible
            if (!string.IsNullOrEmpty(ConfigFile.versusCountry))
            {
                var opp = opponentTeams.FirstOrDefault(t => t.Country == ConfigFile.versusCountry);
                if (opp != null)
                    cbOpponentCountry.SelectedItem = opp;
            }
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            // Save selected favorite/opponent
            if (cbFavoriteCountry.SelectedItem is TeamResults fav)
                ConfigFile.country = fav.Country;
            if (cbOpponentCountry.SelectedItem is TeamResults opp)
                ConfigFile.versusCountry = opp.Country;
            ConfigFile.resolution = selectedResolution;
            ConfigHandling.SaveConfig();
            MessageBox.Show("Settings saved!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
} 