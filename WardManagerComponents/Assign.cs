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
    public partial class Assign : UserControl
    {
        WardManagerFunctions wf = new WardManagerFunctions();
        public Assign()
        {
            InitializeComponent();
        }
        private void PId_KeyPress(object s,KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void Assign_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text=="" || comboBox2.Text == "" || !comboBox2.Items.Contains(Convert.ToInt32(comboBox2.Text)) || PId.Equals(""))
                MessageBox.Show("Enter in valid data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                wf.AssignRoom(Convert.ToInt32(PId.Text), comboBox1.Text,Convert.ToInt32(comboBox2.Text));
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.DataSource=wf.ShowAvailableRooms(comboBox1.Text);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
