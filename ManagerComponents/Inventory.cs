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
        readonly ManagerFunctions mf = new ManagerFunctions();
        readonly ChemistFunctions cf = new ChemistFunctions();
        DataTable dt = new DataTable();
        public Inventory()
        {
            InitializeComponent();
        }
        public void Refres(bool t)
        {
            dataGridView1.DataSource = cf.GetAllStock(t);
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Refres(radioButton1.Checked);
            dt = mf.GetAllRequests(radioButton1.Checked, false);
            if (dt != null)
            {
                dt.Columns.Remove("Flag");
                dt.Columns.Remove("Id");
                gridview.DataSource = dt;
                for (int i = 0; i < gridview.Columns.Count; i++)
                    gridview.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gridview.Columns[3].DisplayIndex = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mf.AddStocks(text1.Text, checkBox1.Checked, Convert.ToInt32(numericUpDown1.Value));
            radioButton1_CheckedChanged(sender, e);
        }
    }
}
