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
        readonly DoctorFunctions df = new DoctorFunctions();
        public Dreport()
        {
            InitializeComponent();
        }
        private void textBox1_KeyPress(object sender,KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Get-Report by Id
            string PID = textBox1.Text;
            if (PID.Equals(""))
                MessageBox.Show("Enter valid data","Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                DataTable dt = df.GetPatientReport(PID);
                dataGridView1.DataSource = dt;

                dt.Columns.Remove("Id");
                dt.Columns.Remove("Disease");
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (dt != null)
                {
                    dataGridView1.Columns[0].Width = 80;
                    dataGridView1.Columns[3].Width = 130;
                }
            }
        }
    }
}
