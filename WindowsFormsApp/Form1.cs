using DataLayer.DataHandling;

namespace WindowsFormsApp
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();

            ConfigHandling.OnConfigFileMissing += HandleConfigFileMissing;

            ConfigHandling.ConfigExists();
            CultureHandling.LoadCulture();
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
    }
}
