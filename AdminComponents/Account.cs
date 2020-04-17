using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital.AdminComponents
{
    public partial class Account : UserControl
    {
        Patient_Management pm = new Patient_Management();

        public Account()
        {
            InitializeComponent();
            dataGridView1.DataSource = pm.GetBills("", out bool x, false);

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
