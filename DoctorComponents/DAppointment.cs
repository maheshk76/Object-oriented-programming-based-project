using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Hospital
{
    public partial class DAppointment : UserControl
    {
        DoctorFunctions df = new DoctorFunctions();
        Patient_Management pm = new Patient_Management();
        public DAppointment()
        {
            InitializeComponent();
            pm.UpdateAppointData();
            dateTimePicker1.Hide();
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(label1, "Showing appointments for today which are not approved");
            Refres();
        }
        public void Refres()
        {
            //load all appointments
            DataTable dt = df.GetAllAppointments("", true);
            dataGridView1.DataSource = dt;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 380;
        }
        private void TextBox_Changed(object sender, EventArgs e)
        {
            //Search Appointment
            DataTable dt = df.GetAllAppointments(searchTextbox.Text,true);
            dataGridView1.DataSource = dt;
            if (searchTextbox.Text == "")
                Refres();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataTable dt = df.GetAllAppointments(Convert.ToDateTime(dateTimePicker1.Text));
            dataGridView1.DataSource = dt;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Show();
                searchTextbox.Hide();
            }
            else
            {
                searchTextbox.Show();
                dateTimePicker1.Hide();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            searchTextbox.Text = "";
            Refres();
        }
    }
}