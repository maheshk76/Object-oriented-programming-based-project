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
            try
            {
                int user_id = Convert.ToInt32(textBox1.Text);
                string pass = textBox2.Text;
                string role = "";//returned value will be stored
                string uname = u.Login(user_id, pass, out role);
                if (uname.Equals(""))
                    throw new Exception();
                SessionClass.SessionId = user_id;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                    MessageBox.Show("abhi program me thoda chainge hai,sunday ko mast naa-dhoke aa", "Babu bhaiyaa",MessageBoxButtons.OK,MessageBoxIcon.Error);
                label8.Text = "NOT FOUND";
            }
        }
    }
}
