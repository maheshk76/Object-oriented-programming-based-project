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
            DataTable dt = df.GetAllAppointments();
            dataGridView1.DataSource = dt;
        }
    }
}
