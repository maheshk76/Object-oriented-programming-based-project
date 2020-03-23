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
        public DAppointment()
        {
            InitializeComponent();
            //loading all appointments
            DataTable dt = df.GetAllAppointments("");
            ToolTip toolTip1 =new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(label1,"Showing appointments which are not approved");
            
            dataGridView1.DataSource = dt;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void TextBox_Changed(object sender, EventArgs e)
        {
            //Search Appointment
            DataTable dt = df.GetAllAppointments(searchTextbox.Text);
            dataGridView1.DataSource = dt;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}