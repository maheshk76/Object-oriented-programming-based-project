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
    public partial class DPrescription : UserControl
    {
        DoctorFunctions df = new DoctorFunctions();
        public DPrescription()
        {
            InitializeComponent();
        }
        private void PID_KeyPress(object sender,KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //ADD Prescription By Doctor
            string patient_id =PID.Text;
            string med = medicine.Text;

            if (patient_id.Equals("") || med.Equals(""))
                MessageBox.Show("Enter valid data","Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                df.MakePrescription(patient_id, med);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //See Presc
            DataTable dt= df.GetPatient(PID.Text, false);
            if (dt != null)
            {
                dt.Columns.Remove("Id");
                dt.Columns.Remove("DId");
                dt.Columns.Remove("Dname");
                dt.Columns[0].ColumnName = "Patient Id";
                dt.Columns[1].ColumnName = "Patient Name";
                dt.Columns[2].ColumnName = "Test Details";
                dataGridView1.DataSource = dt;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}