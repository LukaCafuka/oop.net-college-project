using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.DataHandling;

namespace WindowsFormsApp
{
    public partial class CultureForm : UserControl
    {
        public CultureForm()
        {
            InitializeComponent();
        }

        private void CultureForm_Load(object sender, EventArgs e)
        {
            gbGenderSelection.Text = CultureHandling.GetString("ChampionshipGroupBoxText");
            rbMaleSelect.Text = CultureHandling.GetString("MaleGender");
            rbFemaleSelect.Text = CultureHandling.GetString("FemaleGender");

        }
    }
}
