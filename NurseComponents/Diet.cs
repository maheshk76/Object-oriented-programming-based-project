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
        private void text_KeyPress(object s,KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds=pm.GetPatientTreatment(searchTextbox.Text);
            if (ds.Tables[1] != null)
            {
                dataGridView1.DataSource = ds.Tables[1];
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (ds.Tables[0] != null)
            {
                SearchResultGridView.DataSource = ds.Tables[0];
                for (int i = 0; i < SearchResultGridView.Columns.Count; i++)
                    SearchResultGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }
    }
}
