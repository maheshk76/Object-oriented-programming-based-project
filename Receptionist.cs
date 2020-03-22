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
        Users u = new Users();
        Patient_Management pm = new Patient_Management();
        public Receptionist(string uname)
        {
            InitializeComponent();
            SidePanel.Height = button7.Height;
            rRegister1.BringToFront();
            if (SessionClass.SessionId == 0)
            {
                Login_Form f1 = new Login_Form(0);
                f1.ShowDialog();
            }
            label1.Text = uname;
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
            SessionClass.SessionId = 0;
            u.Logout();
            Login_Form f1 = new Login_Form(0);
            f1.ShowDialog();
        }
    }
}
