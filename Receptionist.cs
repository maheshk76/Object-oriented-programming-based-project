using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class Receptionist : Form
    {
        int Uid_sess = 0;
        Users u = new Users();
        Patient_Management pm = new Patient_Management();
        public Receptionist(int id, string uname)
        {
            InitializeComponent();
            SidePanel.Height = button7.Height;
            rRegister1.BringToFront();
            label1.Text = uname;
            Uid_sess = id;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button7.Height;
            SidePanel.Top = button7.Top;
            rRegister1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            userControl11.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button5.Height;
            SidePanel.Top = button5.Top;
            rPayment1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Uid_sess = 0;
            u.Logout();
            Login_Form f1 = new Login_Form(Uid_sess);
            f1.ShowDialog();
            //Chemist f4 = new Chemist();
            //f4.ShowDialog();
        }
    }
}
