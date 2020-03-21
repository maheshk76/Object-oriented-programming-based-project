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
        Patient_Management pm = new Patient_Management();
        public CPrescription()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Search Patient Prescription By PId
            dataGridView1.DataSource = pm.GetPrescription(Convert.ToInt32(PId.Text));
        }
    }
}
