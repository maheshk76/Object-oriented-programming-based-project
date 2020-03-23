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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = df.GetPatient(searchTextbox.Text, true);
            SearchResultGridView.DataSource = dt;
            if (dt != null)
            {
                SearchResultGridView.Columns[8].Width = 200;
                SearchResultGridView.Columns[8].DisplayIndex = 0;
            }
        }
    }
}
