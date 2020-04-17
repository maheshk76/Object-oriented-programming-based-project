using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital.UserForms
{
    public partial class Admin : Form
    {
        Users u = new Users();
        public Admin()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button4.Top;
           hire.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button1.Top;
            duserdetails.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button7.Top;
            allusers.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            u.Logout();
            Login_Form f1 = new Login_Form(0);
            f1.ShowDialog();
        }
    }
}
