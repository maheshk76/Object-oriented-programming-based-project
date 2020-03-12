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
    public partial class Form2 : Form
    {
        int Uid_sess = 0;
        public Form2(int id,string uname)
        {
            InitializeComponent();
            SidePanel.Height = button7.Height;
            label1.Text = uname;
            Uid_sess = id;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Logout
            Uid_sess = 0;
            this.Hide();
            Form1 f1 = new Form1(Uid_sess);
            f1.ShowDialog();
        }

        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pname, gname, paddress, pgender,pemail,pdetails;
            int page, pcontact;
            DateTime adddate;

            //New Patient
            Patient_Management pm = new Patient_Management();
            pm.RegisterNewPatient(pname, gname, paddress, page, pemail, pcontact, pgender,pdetails adddate);
        }
    }
}
