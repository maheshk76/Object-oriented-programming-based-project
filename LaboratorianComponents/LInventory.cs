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
    public partial class LInventory : UserControl
    {
        ChemistFunctions cf = new ChemistFunctions();
        public LInventory()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cf.RequestStock(textBox2.Text, Convert.ToInt32(numericUpDown1.Value), false);
        }
    }
}
