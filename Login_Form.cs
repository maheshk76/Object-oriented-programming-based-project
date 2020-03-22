using System;
using System.Windows.Forms;

namespace Hospital
{
    public partial class Login_Form : Form
    {
        public Login_Form(int id)
        {
            InitializeComponent(); 
            SessionClass.SessionId =id;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Panel is for Fortis hospital staff member only.\nCheck your internet connection.\n Be sure before submitting sign in details.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Exit the Application
            this.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Users u = new Users();
            string uname = textBox1.Text;
            string pass = textBox2.Text;
            string role = "";
            int user_id = u.Login(uname, pass, out role);
            SessionClass.SessionId = user_id;
            Console.WriteLine(user_id);
            if (user_id > 0)
            {
                this.Hide();
                switch (role)
                {
                    case "Doctor":
                        Doctor f2 = new Doctor(uname);
                        f2.ShowDialog();
                        break;
                    case "Laboratorian":
                        Laboratorian lr = new Laboratorian(uname);
                        lr.ShowDialog();
                        break;
                    case "Chemist":
                        Chemist ch = new Chemist(uname);
                        ch.ShowDialog();
                        break;
                    case "Receptionist":
                        Receptionist rc = new Receptionist(uname);
                        rc.ShowDialog();
                        break;
                    case "Manager":
                       // Manager m = new Manager(uname);
                        //m.ShowDialog();
                        break;
                    case "Nurse":
                        //Nurse nu= new Nurse(uname);
                        //nu.ShowDialog();
                        break;
                    case "Ward_Manager":
                        //Ward_Manager wm = new Ward_Manager(uname);
                        //wm.ShowDialog();
                        break;
                    case "Admin":
                        //Admin a=new Admin(uname);
                        // a.ShowDialog();
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
