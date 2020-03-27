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
    public partial class AllUsers : UserControl
    {
        ManagerFunctions mf = new ManagerFunctions();
        public AllUsers()
        {
            InitializeComponent();
            SearchResultGridView.DataSource = mf.GetAllUsers("");
            for (int i = 0; i < SearchResultGridView.Columns.Count; i++)
                SearchResultGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void searchTextbox_TextChanged(object sender, EventArgs e)
        {
            SearchResultGridView.DataSource = mf.GetAllUsers(searchTextbox.Text);
        }
    }
}
