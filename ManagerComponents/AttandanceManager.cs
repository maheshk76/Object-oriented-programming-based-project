using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital.Classes;

namespace Hospital.ManagerComponents
{
    public partial class AttandanceManager : UserControl
    {
        ManagerFunctions mf = new ManagerFunctions();
        public AttandanceManager()
        {
            InitializeComponent(); ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(label8, "Showing available users on that day");
            label4.Text =mf.GetAllUsers("").Rows.Count.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataTable dt=mf.GetAttandance(dateTimePicker1.Text);
            SearchResultGridView.DataSource = dt;
            for (int i = 0; i < SearchResultGridView.Columns.Count; i++)
                SearchResultGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if(dt!=null)
            label6.Text = dt.Rows.Count.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
