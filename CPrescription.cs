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
    public partial class CPrescription : UserControl
    {
        DoctorFunctions df = new DoctorFunctions();
        public CPrescription()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Search Patient Prescription By PId
            DataTable dt = df.GetPatient(PId.Text, false);
            if (dt != null)
            {
                dt.Columns.Remove("Id");
                dt.Columns.Remove("DId");
                dt.Columns.Remove("Dname");
            }
            dataGridView1.DataSource = dt;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
