using Hospital.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class Manager : Form
    {
        readonly Users u = new Users();
        readonly ManagerFunctions mf = new ManagerFunctions();
        public Manager()
        {
            InitializeComponent();
            Refres();
            SidePanel.Height = button7.Height;
            if (SessionClass.SessionId == 0)
            {
                Login_Form f1 = new Login_Form(0);
                f1.ShowDialog();
            }

            label1.Text = "        " + SessionClass.SessionId.ToString();
        }
        public void Refres()
        {
            if (mf.GetAllRequests(false, true).Rows.Count == 0)
                radioButton1.Hide();
            else
                radioButton1.Show();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button7.Top;
            allusers.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button1.Top;
            duserdetails.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button3.Top;
            attandance.BringToFront();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            u.Logout();
            Login_Form f1 = new Login_Form(0);
            f1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button2.Top;
            inven.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Refres();
        }
    }
}
