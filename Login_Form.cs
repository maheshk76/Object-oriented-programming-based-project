using System;
using System.Windows.Forms;

namespace Hospital
{

    public partial class Login_Form : Form
    {
        int Uid_sess = 0;
        public Login_Form(int id)
        {
            InitializeComponent();
            Uid_sess = id;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Panel is for Fortis hospital staff member only.\nCheck your internet connection.\n Be sure before submitting sign in details.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Users u = new Users();
            string uname = textBox1.Text;
            string pass = textBox2.Text;
            string role = "";
            int user_id = u.Login(uname, pass, out role);
            Uid_sess = user_id;

            Console.WriteLine(user_id);
            if (user_id > 0)
            {
                this.Hide();
                switch (role)
                {
                    case "Doctor":
                        Doctor f2 = new Doctor(Uid_sess, uname);
                        f2.ShowDialog();
                        break;
                    case "Laboratorian":
                        //Open Labo. form
                        break;
                    case "Chemist":
                        break;
                    case "Receptionist":
                        Receptionist rc = new Receptionist(Uid_sess, uname);
                        rc.ShowDialog();
                        break;
                    case "Manager":
                        break;
                    case "Nurse":
                        break;
                    case "Ward_Manager":
                        break;
                }
            }
            else
            {
                label8.Text = "NOT FOUND";
            }
        }
    }
}
