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
    public partial class RRegister : UserControl
    {

        Patient_Management pm = new Patient_Management();
        public RRegister()
        {
            InitializeComponent();
            listBox1.DataSource= pm.GetDoctorList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Register Patient
            string p_name = Pname.Text;
            string g_name = Gname.Text;
            int age = Convert.ToInt32(agebar.Text);
            string email = mailadd.Text;
            string contact =Phnum.Text;
            string address = Address.Text;
            string gender = "";
            string doctor_assinged = listBox1.SelectedItem.ToString();
            foreach (RadioButton r in groupBox1.Controls)
            {
                if (r.Checked)
                    gender=r.Text;
            }
            DateTime bdate = Convert.ToDateTime(dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            int patient_number_returned=pm.RegisterNewPatient(p_name,g_name,address,age,email,contact,gender,bdate,doctor_assinged);
            if (patient_number_returned < 0)
                MessageBox.Show("Something went wrong,Please try agian", "Error");
            else
                MessageBox.Show("Patient Id Number: " + patient_number_returned, "Success");
        }
    }
}
