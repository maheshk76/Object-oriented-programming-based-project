using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital.NurseComponents
{
    public partial class Diet : UserControl
    {
        Patient_Management pm = new Patient_Management();
        public Diet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt=pm.GetPatientTreatment(searchTextbox.Text);
            if (dt != null)
            {
                SearchResultGridView.DataSource = dt;
                for (int i = 0; i < SearchResultGridView.Columns.Count; i++)
                    SearchResultGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }
    }
}
