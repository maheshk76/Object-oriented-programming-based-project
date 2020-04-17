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
        DataTable dt;
        public Wards()
        {
            InitializeComponent();
            dt= wf.GetWards();
            dataGridView1.DataSource = dt;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
        }
        private void Wards_Load(object sender, EventArgs e)
        {
            label5.Text = (from DataRow dr in wf.GetRooms().Rows
                           where Convert.ToBoolean(dr["Assigned"]) == true
                           select Convert.ToBoolean(dr["Assigned"])).Count().ToString();
            label4.Text =(Convert.ToInt32(dt.Compute("SUM(TotalRooms)", string.Empty))-Convert.ToInt32(label5.Text)).ToString();
        }

    }
}
