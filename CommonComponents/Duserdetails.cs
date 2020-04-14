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
        public DataSet FlipDataSet(DataSet my_DataSet)
        {
            DataSet ds = new DataSet();

            foreach (DataTable dt in my_DataSet.Tables)
            {
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
            }

            return ds;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = df.GetPatient(searchTextbox.Text, true);
            /*SearchResultGridView.DataSource = dt;
            if (dt != null)
                SearchResultGridView.Columns.Remove("Details");*/
            dt.Columns.Remove("Details");
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            DataSet new_ds=FlipDataSet(ds);
            DataView my_DataView = new_ds.Tables[0].DefaultView;
            this.SearchResultGridView.DataSource = my_DataView;
            for (int i = 0; i < SearchResultGridView.Columns.Count; i++)
                SearchResultGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SearchResultGridView.Columns["0"].DefaultCellStyle.BackColor = Color.LightBlue;
            SearchResultGridView.Columns["0"].Width = 250;

        }
    }
}
