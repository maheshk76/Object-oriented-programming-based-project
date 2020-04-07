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

namespace Hospital
{
    public partial class CMedicine : UserControl
    {
        ChemistFunctions cf = new ChemistFunctions();
        public CMedicine()
        {
            InitializeComponent();
            DataTable dt = cf.GetAllMed();
            if (dt != null)
            {
                dt.Columns.Remove("Id");
                dataGridView1.DataSource = dt;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cf.RequestStock(textBox2.Text, Convert.ToInt32(numericUpDown1.Value),true);
        }
    }
}
