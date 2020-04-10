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
    public partial class Inventory: UserControl
    {
        ManagerFunctions mf = new ManagerFunctions();
        DataTable dt = new DataTable();
        public Inventory()
        {
            InitializeComponent();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                dt = mf.GetAllRequests(true,false);
            else
                dt = mf.GetAllRequests(false,false);
            if (dt != null)
            {
                dt.Columns.Remove("Flag");
                gridview.DataSource = dt;
                for (int i = 0; i < gridview.Columns.Count; i++)
                    gridview.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string val = text1.Text;
            int quan = Convert.ToInt32(numericUpDown1.Value);
            bool med = checkBox1.Checked;
            mf.AddStocks(val, med, quan);
        }
    }
}
