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
    public partial class DAppointment : UserControl
    {
        DoctorFunctions df = new DoctorFunctions();
        public DAppointment()
        {
            InitializeComponent();
            DataTable dt = df.GetAllAppointments();
            
            dataGridView1.DataSource = dt;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Provide Patient Id number to approve", "Info");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
