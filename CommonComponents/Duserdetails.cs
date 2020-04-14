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
    public partial class UserControl1 : UserControl
    {
        DoctorFunctions df = new DoctorFunctions();
        public UserControl1()
        {
            InitializeComponent();
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(button3, "Provide full ID number for specific result");
        }
        public DataSet FlipDataTable(DataTable dt)
        {
            DataSet ds = new DataSet();
                DataTable table = new DataTable();
                for (int i = 0; i <= dt.Rows.Count; i++)
                { table.Columns.Add(Convert.ToString(i)); }
                DataRow r;
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    r = table.NewRow();
                    r[0] = dt.Columns[k].ToString();
                    for (int j = 1; j <= dt.Rows.Count; j++)
                    { r[j] = dt.Rows[j - 1][k]; }
                    table.Rows.Add(r);
                }
                ds.Tables.Add(table);
            return ds;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = df.GetPatient(searchTextbox.Text, true);
            if (dt != null)
            {
                dt.Columns.Remove("Details");
                DataSet new_ds = FlipDataTable(dt);
                this.SearchResultGridView.DataSource = new_ds.Tables[0].DefaultView;
                for (int i = 0; i < SearchResultGridView.Columns.Count; i++)
                    SearchResultGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                SearchResultGridView.Columns["0"].DefaultCellStyle.BackColor = Color.LightBlue;
                SearchResultGridView.Columns["0"].Width = 200;
            }
        }
    }
}
