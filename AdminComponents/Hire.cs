using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital.Classes;

namespace Hospital.AdminComponents
{
    public partial class Hire : UserControl
    {
        AdminFunctions af = new AdminFunctions();
        public Hire()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = Pname.Text;
            string pass = Gname.Text;
            
            string email = mailadd.Text;
            string contact = Phnum.Text;
            string address = Address.Text;
            string gender = "";
            foreach (RadioButton r in groupBox1.Controls)
            {
                if (r.Checked)
                    gender = r.Text;
            }
            string qualification = richTextBox1.Text;
            string experience = richTextBox2.Text;
            int salary =Convert.ToInt32(textBox1.Text);
            string role = comboBox1.Text;
            Console.WriteLine(name, pass, email, contact, address, gender, qualification, experience, salary, role);
            if (FormValidation(name, pass, email, contact, address, gender, qualification,experience,salary,role))
            {
                int user_number_returned = af.HireUser(name, pass, email, contact, address, gender, qualification, experience, salary, role);
                if (user_number_returned < 0)
                    MessageBox.Show("Something went wrong,Please try agian", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("User Id Number: " + user_number_returned, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Enter sufficient data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        bool isNum(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (char.IsDigit(s[i]) == false)
                    return false;
            return true;
        }
        public bool FormValidation(string name, string pass, string email, string contact, string addr,string gender,string qual,string exp,int? sal,string role)
        {
            bool flg = true;

            if (email=="" || name == "" || pass == "" || contact == "" || addr == "" || gender == "" || qual=="" || exp=="" || sal==null
                || sal <= 0 || role == null  || isNum(gender) || !isNum(contact) || contact.Length < 10)
                flg = false;
            return flg;
        }
        private void KeypressAction(object s,KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                MessageBox.Show("Enter valid data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DialogResult k=MessageBox.Show("Are you sure want to delete?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (k == DialogResult.OK)
                    af.Dismiss(Convert.ToInt32(textBox2.Text));
            }
        }
    }
}
