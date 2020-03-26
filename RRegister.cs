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
        ErrorProvider err = new ErrorProvider();
        Patient_Management pm = new Patient_Management();
        public RRegister()
        {
            InitializeComponent();
            listBox1.DataSource= pm.GetDoctorList("");
        }
        public bool FormValidation(string pname,string gname,string email,string contact,string addr,string gender,int age,DateTime bdate,string doc_as)
        {
             bool isNumber(string s)
            {
                for (int i = 0; i < s.Length; i++)
                    if (char.IsDigit(s[i]) == false)
                        return false;
                return true;
            }
            bool flg = true;
            if (pname == "" || gname == "" || contact == "" || addr == "" || gender == "" || doc_as == "" || age<=0 || bdate==null)
                flg = false;
            if (isNumber(pname) || isNumber(gname) || isNumber(gender) || !isNumber(contact))
                flg = false;
            err.SetError(Pname, "Enter name");
            return flg;
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
            string doctor_assigned = listBox1.SelectedItem.ToString();
            foreach (RadioButton r in groupBox1.Controls)
            {
                if (r.Checked)
                    gender=r.Text;
            }
            DateTime bdate = Convert.ToDateTime(dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            if (FormValidation(p_name,g_name,email,contact, address,gender, age, bdate,doctor_assigned))
            {
                int patient_number_returned = pm.RegisterNewPatient(p_name, g_name, address, age, email, contact, gender, bdate, doctor_assigned);
                if (patient_number_returned < 0)
                    MessageBox.Show("Something went wrong,Please try agian", "Error");
                else
                    MessageBox.Show("Patient Id Number: " + patient_number_returned, "Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Enter sufficient data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
