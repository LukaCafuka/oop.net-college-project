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

namespace WindowsFormsApp
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {

            UpdateControlTexts();
        }

        private void UpdateControlTexts()
        {
            rbGenderFemaleSelect.Text = CultureHandling.GetString("FemaleGender");
            rbGenderMaleSelect.Text = CultureHandling.GetString("MaleGender");

            rbCroatianLangSelect.Text = CultureHandling.GetString("CroatianLang");
            rbEnglishLangSelect.Text = CultureHandling.GetString("EnglishLang");

            gbChampionshipSelection.Text = CultureHandling.GetString("ChampionshipGroupBoxText");
            gbLangSelection.Text = CultureHandling.GetString("LanguageGroupBoxText");

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

        private void btnSubmitConfig_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("The changes have been saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Hide();
                rbGenderFemaleSelect.Checked = false;
                ConfigFile.country = String.Empty;
                ConfigHandling.SaveConfig();
            }
        }


    }
}
