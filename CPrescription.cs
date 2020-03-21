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
        DoctorFunctions pm = new DoctorFunctions();
        public CPrescription()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Search Patient Prescription By PId
            dataGridView1.DataSource = pm.GetPatient(PId.Text,false);
        }
    }
}
