using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital.Classes;

namespace Hospital
{
    public partial class CPrescription : UserControl
    {
        DoctorFunctions df = new DoctorFunctions();
        ChemistFunctions cf = new ChemistFunctions();
        public CPrescription()
        {
            InitializeComponent();
            comboBox1.DataSource = cf.GetMedicines();
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
                dt.Columns[1].ColumnName = "Patient Name";
                dt.Columns[2].ColumnName = "Tests";
                dataGridView1.DataSource = dt;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].Width = 170;
                dataGridView1.Columns[3].Width = 200;
                dataGridView1.Columns[4].Width = 120;
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pid = PId.Text;
            string med_name = comboBox1.Text;
            int quan = Convert.ToInt32(comboBox2.Text);
            DataTable dt=cf.AddToList(pid,med_name,quan);
            if (dt != null)
            {
                dt.Columns.Remove("Injection");
                dt.Columns.Remove("Injection_Taken_or_not");
                dt.Columns.Remove("Id");
                dt.Columns.Remove("Diet");
                dt.Columns.Remove("Number_of_Doses");
            }
            dataGridView2.DataSource = dt;
            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
