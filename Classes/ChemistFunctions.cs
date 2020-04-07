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
        DataTable dt;
        public DataTable GetAllMed()
        {
            dt = new DataTable();
            cmd.Connection=con;
            cmd.CommandText = "select * from Medicine_Stock";
            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
            
        }
        public DataTable AddToList(string pid,string med,int quan)
        {
            try
            {
                dt = new DataTable();
                DateTime date = DateTime.Now.Date;
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "insert into Patient_Treatment(PId,Medicines) Values(@pid,@med)";
                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.Parameters.AddWithValue("@med", med);
                cmd.ExecuteNonQuery();
                con.Close();
                //stock change Opr
                con.Open();
                cmd.CommandText = "select * from Medicine_Stock where Name=@med";
                SqlDataReader r = cmd.ExecuteReader();
                int stock = 0;
                while (r.Read())
                    stock = Convert.ToInt32(r["Quantity"]);
                stock = stock - @quan;
                if (stock <= 0)
                {
                    MessageBox.Show("Not Available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
                con.Close();
                con.Open();
                cmd.CommandText = "update Medicine_Stock set Quantity=@stock";
                cmd.Parameters.AddWithValue("@stock", stock);
                cmd.ExecuteNonQuery();
                cmd.Parameters.RemoveAt("@stock");
                cmd.Parameters.RemoveAt("@med");
                con.Close();
                //retrie opr
                con.Open();
                cmd.CommandText = "select * from Patient_Treatment where PId=@pid";
                r = cmd.ExecuteReader();
                dt.Load(r);
                cmd.Parameters.RemoveAt("@pid");
                con.Close();
                return dt;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.FormatException"))
                    MessageBox.Show("Enter valid data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Something went wrong,Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
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
                    l.Add(r["Name"].ToString());
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
        public void RequestStock(string name, int quantity,bool flg)
        {
            //flg=true ==>medicines
            //flg=false==>things
            try
            {
                if (quantity <= 0)
                    throw new ArgumentNullException();
                cmd.Connection = con;
                cmd.CommandText = "insert into StockManager(Name,Quantity,Flag) Values(@name,@quantity,@flg)";
                con.Open();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@flg", flg);
                cmd.ExecuteNonQuery();
                cmd.Parameters.RemoveAt("@name");
                cmd.Parameters.RemoveAt("@quantity");
                cmd.Parameters.RemoveAt("@flg");
                MessageBox.Show("Success", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.FormatException") || etype.Equals("'System.ArgumentNullException"))
                    MessageBox.Show("Enter valid data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Something went wrong,Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
