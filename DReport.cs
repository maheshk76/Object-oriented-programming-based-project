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
    public partial class Dreport : UserControl
    {
        DoctorFunctions df = new DoctorFunctions();
        DataTable dt = new DataTable();
        public Dreport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Search by Id;

            dataGridView1.DataSource =
            df.GetPatient(Convert.ToInt32(textBox1.Text),true);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
