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

namespace Hospital.WardManagerComponents
{
    public partial class Wards : UserControl
    {
        WardManagerFunctions wf = new WardManagerFunctions();
        public Wards()
        {
            InitializeComponent();
            dataGridView1.DataSource = wf.GetWards();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
    }
}
