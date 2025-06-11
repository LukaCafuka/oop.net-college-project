using DataLayer.DataHandling;
using DataLayer.JsonModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickType;

namespace WindowsFormsApp
{
    public partial class ConfigForm : Form
    {
        private List<TeamResults> teams = new List<TeamResults>();

        public ConfigForm()
        {
            InitializeComponent();
        }

        private async void ConfigForm_Load(object sender, EventArgs e)
        {
            UpdateControlTexts();
            await LoadTeams();
        }

        private async Task LoadTeams()
        {
            try
            {
                var teamsSet = await ApiDataHandling.LoadJsonTeams();
                teams = teamsSet.OrderBy(t => t.Country).ToList();
                cbFavoriteTeam.Items.Clear();

                foreach (var team in teams)
                {
                    cbFavoriteTeam.Items.Add(team.FormatForComboBox());
                }

                if (!string.IsNullOrEmpty(ConfigFile.country))
                {
                    var team = teams.FirstOrDefault(t => t.Country == ConfigFile.country);
                    if (team != null)
                    {
                        cbFavoriteTeam.SelectedItem = team.FormatForComboBox();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateControlTexts()
        {
            rbGenderFemaleSelect.Text = CultureHandling.GetString("FemaleGender");
            rbGenderMaleSelect.Text = CultureHandling.GetString("MaleGender");

            rbCroatianLangSelect.Text = CultureHandling.GetString("CroatianLang");
            rbEnglishLangSelect.Text = CultureHandling.GetString("EnglishLang");

            gbChampionshipSelection.Text = CultureHandling.GetString("ChampionshipGroupBoxText");
            gbLangSelection.Text = CultureHandling.GetString("LanguageGroupBoxText");
            gbFavoriteTeam.Text = CultureHandling.GetString("FavoriteTeamGroupBoxText");

            btnSubmitConfig.Text = CultureHandling.GetString("ButtonSubmit");
        }

        private void rbGenderMaleSelect_Click(object sender, EventArgs e)
        {
            ConfigFile.gender = ConfigFile.Gender.Male;
        }

        private void rbGenderFemaleSelect_Click(object sender, EventArgs e)
        {
            ConfigFile.gender = ConfigFile.Gender.Female;
        }

        private void EnglishLang_Click(object sender, EventArgs e)
        {
            CultureHandling.SetCulture("hr-HR");
            ConfigFile.language = "Croatian";
            UpdateControlTexts();
        }

        private void CroatianLang_Click(object sender, EventArgs e)
        {
            CultureHandling.SetCulture("en-US");
            ConfigFile.language = "English";
            UpdateControlTexts();
        }

        private void cbFavoriteTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFavoriteTeam.SelectedItem != null)
            {
                string selectedTeam = cbFavoriteTeam.SelectedItem.ToString();
                string country = selectedTeam.Split('(')[0].Trim();
                ConfigFile.country = country;
            }
        }

        private void btnSubmitConfig_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ConfigFile.country))
            {
                MessageBox.Show("Please select a favorite team", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("The changes have been saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Hide();
                rbGenderFemaleSelect.Checked = false;
                ConfigHandling.SaveConfig();
            }
        }
    }
}
