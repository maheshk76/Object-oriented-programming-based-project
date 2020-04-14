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
    public partial class Doctor : Form
    {
        readonly Users u = new Users();
        readonly DoctorFunctions df = new DoctorFunctions();
        public Doctor()
        {
            InitializeComponent(); notify();
            SidePanel.Height = button7.Height;
            dAppointment.BringToFront();
            if (SessionClass.SessionId == 0)
            {
                Login_Form f1 = new Login_Form(0);
                f1.ShowDialog();
            }
            label1.Text = SessionClass.SessionName;
        }
        public void notify()
        {
            if (df.GetAllAppointments("", true).Rows.Count == 0)
                radioButton1.Hide();
            else
                radioButton1.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button1.Top;
            duserdetails.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button5.Top;
            dPrescription.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button3.Top;
            dreport.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Show All Appointments
            SidePanel.Top = button7.Top;
            dAppointment.BringToFront();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Resetting the Global variable
            //Logout
            this.Hide();
            u.Logout();
            Login_Form f1 = new Login_Form(0);
            f1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button2.Top;
            dtest1.BringToFront();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            notify();
        }
    }
}
