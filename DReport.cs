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
        private void button1_Click(object sender, EventArgs e)
        {
            //Get-Report by Id
            string PID = textBox1.Text;
            if (PID.Equals(""))
                MessageBox.Show("Enter valid data");
            else
            {
                DataTable dt = df.GetPatientReport(PID);

                dataGridView1.DataSource = dt;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (dt != null)
                {
                    dataGridView1.Columns[1].Width = 80;
                    dataGridView1.Columns[2].Width = 80;
                    dataGridView1.Columns[5].Width = 200;
                }
            }
        }
    }
}
