﻿using System;
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
    public partial class Laboratorian : Form
    {
        readonly Users u = new Users();
        readonly Patient_Management pm = new Patient_Management();
        public Laboratorian()
        {
            InitializeComponent();


            SidePanel.Height = button7.Height;
            lReport1.BringToFront();
            if (SessionClass.SessionId == 0)
            {
                Login_Form f1 = new Login_Form(0);
                f1.ShowDialog();
            }

            label1.Text = "        " + SessionClass.SessionId.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button7.Top;
            lReport1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button1.Top;
            userControl12.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button5.Top;
            lInventory1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Logout
            SessionClass.SessionId = 0;
            u.Logout();
            Login_Form f1 = new Login_Form(0);
            f1.ShowDialog();
        }
    }
}