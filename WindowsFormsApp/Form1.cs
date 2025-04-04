using DataLayer.DataHandling;
using QuickType;
using DataLayer.JsonModels;
using System.Reflection;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {

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

        private async void FillData()
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
            FillData();
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
