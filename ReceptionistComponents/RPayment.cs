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
    public partial class RPayment : UserControl
    {
        Patient_Management pm = new Patient_Management();
        DoctorFunctions df = new DoctorFunctions();
        public RPayment()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            pm.DischargePatient(textBox2.Text);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            DataTable dt = pm.GetBills(textBox2.Text, out bool stat,true);
            if (dt != null)
            {
                if (stat)
                {
                    label2.Text = "Paid";
                }
                else
                {
                    label2.Text = "Pending";
                }
                dt.Columns.Remove("Id");
                dataGridView1.DataSource = dt;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            DataTable dt1=df.GetPatient(textBox2.Text, true);
            if (dt1 != null)
            {
                try
                {
                    DateTime x = (from DataRow dr in dt1.Rows
                                  where dr["PatientId"].ToString() == textBox2.Text
                                  select Convert.ToDateTime(dr["Discharge Date"])).FirstOrDefault();
                    if (x == null) button2.Enabled = true;
                    else button2.Enabled = false;
                }
                catch(Exception)
                {
                    button2.Enabled = true;
                }
            }
            if (textBox2.Text.Length.Equals(0))
                button2.Enabled = true;
        }
    }
}