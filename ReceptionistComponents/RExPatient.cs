using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class RExPatient : UserControl
    {
        DoctorFunctions df = new DoctorFunctions();
        Patient_Management pm = new Patient_Management();
        public RExPatient()
        {
            InitializeComponent();
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
            private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string PID = textBox2.Text;
           DataTable dt= df.GetAllAppointments(PID,false);
            SearchResultGridView.DataSource = dt;

            for (int i = 0; i < SearchResultGridView.Columns.Count; i++)
                SearchResultGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SearchResultGridView.Columns[0].Width = 100;
            SearchResultGridView.Columns[2].Width = 180;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string PID = textBox2.Text;
            pm.AddtoAppointments(PID, "", "");
        }
    }
}
