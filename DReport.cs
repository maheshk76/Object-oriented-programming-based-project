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
            DataTable dt = df.GetPatient(textBox1.Text, true);
            if (dt != null)
            {
                dt.Columns.Remove("GuardianName");
                dt.Columns.Remove("PEmail");
                dt.Columns.Remove("BirthDate");
                dt.Columns.Remove("PAddress");
                dt.Columns.Remove("AddDate");
                dt.Columns.Remove("DisDate");
                dt.Columns.Remove("PAge");
                dt.Columns.Remove("PGender");
                dt.Columns.Remove("PContact");
            }
            dataGridView1.DataSource = dt;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
