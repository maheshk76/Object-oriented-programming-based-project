using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital.Classes
{
    class ChemistFunctions:MakeConnection
    {
        public DataTable AddToList(string pid,string med,int quan)
        {
            DataTable dt = new DataTable();
            DateTime date = DateTime.Now.Date;
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "insert into Patient_Treatment(PId,Medicines) Values(@pid,@med)";
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@med", med);
            cmd.ExecuteNonQuery();
            //stock change Opr
            con.Close();
            con.Open();
            cmd.CommandText = "select * from Medicine_Stock where Name=@med";
            SqlDataReader r = cmd.ExecuteReader();
            int stock=0;
            while (r.Read())
                stock = Convert.ToInt32(r["Quantity"]);
            stock = stock - @quan;
            if (stock <= 0)
            {
                MessageBox.Show("Not Available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            cmd.CommandText = "update Medicine_Stock set Quantity=@stock";
            cmd.Parameters.AddWithValue("@stock", stock);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Parameters.RemoveAt("@stock");
            cmd.Parameters.RemoveAt("@med");
            //retrie opr
            con.Open();
            cmd.CommandText = "select * from Patient_Treatment where PId=@pid";
            r= cmd.ExecuteReader();
            dt.Load(r);
            cmd.Parameters.RemoveAt("@pid");
            con.Close();
            return dt;
        }
        public List<string> GetMedicines()
        {
            List<string> l = new List<string>();
            try
            {
                cmd.Connection = con;
                cmd.CommandText = "select * from Medicine_Stock where Quantity>0";
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    l.Add(r["Name"].ToString());
                }
                return l;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong,Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public void RequestMedicineStock(string name,int quantity)
        {

        }
    }
}
