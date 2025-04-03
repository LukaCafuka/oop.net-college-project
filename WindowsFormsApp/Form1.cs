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
            // Open the configuration form
            using (var configForm = new CultureForm()) // Replace ConfigForm with your actual form class name
            {

            }
        }
    }
}
