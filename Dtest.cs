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
    public partial class Dtest : UserControl
    {
        DoctorFunctions df = new DoctorFunctions();
        public Dtest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string PID = textBox2.Text;
            string Tests = richTextBox1.Text;
            if (PID.Equals("") || Tests.Equals(""))
                MessageBox.Show("Enter valid data");
            else
                df.AddTestDetails(PID,Tests);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
