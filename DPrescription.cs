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
        DoctorFunctions am = new DoctorFunctions();
        public DPrescription()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ADD Prescription By Doctor
            int patient_id = Convert.ToInt32(PID.Text);
            string med = medicine.Text;
            am.MakePrescription(patient_id, med);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //See Presc
            dataGridView1.DataSource = am.GetPatient(PID.Text,false);

        }
    }
}