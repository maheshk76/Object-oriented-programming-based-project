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
        public void MakeBill(string pid,int amt,int choice)
        {
            //choice = 2 ==> Medi bill,3=> Rent,4=>Other
            con.Open();
            cmd.CommandText = "select * from Patient_Bills where PId=@pid";
            SqlDataReader r = cmd.ExecuteReader();
            long bill = 0;
            long tot = 0;
            while (r.Read())
            {
                bill = Convert.ToInt32(r["Medicines_Bill"]);
                tot = Convert.ToInt32(r["Total"]);
            }
            bill += amt;
            tot += amt;
            con.Close();
            con.Open();
            if(choice==2)
            cmd.CommandText = "update Patient_Bills set Medicines_Bill=@bill,Total=@tot where PId=@pid";
            if(choice==3)
                cmd.CommandText = "update Patient_Bills set Rent_Bill=@bill,Total=@tot where PId=@pid";
            if(choice==4)
                cmd.CommandText = "update Patient_Bills set Other_Fees=@bill,Total=@tot where PId=@pid";
            cmd.Parameters.AddWithValue("@bill", bill);
            cmd.Parameters.AddWithValue("@tot", tot);
            cmd.ExecuteNonQuery();
            cmd.Parameters.RemoveAt("@bill");
            cmd.Parameters.RemoveAt("@tot");
            con.Close();
        }
        public bool UpdateStock(string pid,string med,int quan)
        {
            con.Open();
            cmd.CommandText = "select * from Medicine_Stock where Name=@med";
            SqlDataReader r = cmd.ExecuteReader();
            int stock = 0;
            int prc = 0;
            while (r.Read())
            {
                stock = Convert.ToInt32(r["Quantity"]);
                prc = Convert.ToInt32(r["Price_per_piece"]);
            }
            stock = stock - quan;
            if (stock <= 0)
            {
                MessageBox.Show("Not Available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            con.Close();
            MakeBill(pid, quan*prc,2);
            con.Open();
            cmd.CommandText = "update Medicine_Stock set Quantity=@stock";
            cmd.Parameters.AddWithValue("@stock", stock);
            cmd.ExecuteNonQuery();
            cmd.Parameters.RemoveAt("@stock");
            cmd.Parameters.RemoveAt("@med");
            con.Close();
            return true;
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
                if (!(UpdateStock(pid,med,quan)))
                    return null;
                //retrie opr
                con.Open();
                cmd.CommandText = "select * from Patient_Treatment where PId=@pid";
                SqlDataReader r = cmd.ExecuteReader();
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
                DateTime date = DateTime.Now.Date;
                if (quantity <= 0)
                    throw new ArgumentNullException();
                cmd.Connection = con;
                cmd.CommandText = "insert into StockRequests(Name,Quantity,Flag,Date,Delivered) Values(@name,@quantity,@flg,@date,'false')";
                con.Open();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@flg", flg);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.ExecuteNonQuery();
                cmd.Parameters.RemoveAt("@name");
                cmd.Parameters.RemoveAt("@quantity");
                cmd.Parameters.RemoveAt("@flg");
                cmd.Parameters.RemoveAt("@date");
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
