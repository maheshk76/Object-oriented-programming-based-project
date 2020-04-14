using Hospital.UserForms;
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
                string uname = u.Login(user_id, pass, out string role);
                if (uname.Equals(""))
                    throw new FormatException();
                SessionClass.SessionId = user_id;
                SessionClass.SessionName = uname;
                this.Hide();
                switch (role)
                    {
                        case "Doctor":
                            Doctor doctor = new Doctor();
                            doctor.ShowDialog();
                            break;
                        case "Laboratorian":
                            Laboratorian laboratorian = new Laboratorian();
                            laboratorian.ShowDialog();
                            break;
                        case "Chemist":
                            Chemist chemist = new Chemist();
                            chemist.ShowDialog();
                            break;
                        case "Receptionist":
                            Receptionist receptionist = new Receptionist();
                            receptionist.ShowDialog();
                            break;
                        case "Manager":
                            Manager manager = new Manager();
                            manager.ShowDialog();
                            break;
                        case "Nurse":
                            Nurse nurse= new Nurse();
                            nurse.ShowDialog();
                            break;
                        case "WardManager":
                            WardManager wm = new WardManager();
                            wm.ShowDialog();
                            break;
                        case "Admin":
                            //Admin a=new Admin();
                            // a.ShowDialog();
                            break;
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                if (ex.GetType().ToString().Equals("System.FormatException"))
                    MessageBox.Show("User doesn't exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                 MessageBox.Show("Something went wrong please try agian later", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                label8.Text = "NOT FOUND";
            }
        }
    }
}
