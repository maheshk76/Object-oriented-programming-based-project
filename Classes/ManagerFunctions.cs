using DotLiquid.Tags;
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
    interface IManager
    {
        void UpdateStockRequests(string value);
        int CheckExistenceOfStock(string Val, bool flg);
        void AddStocks(string Val, bool flg, int quan);
        DataTable GetAllRequests(bool flg, bool new_req);
        DataTable GetAllUsers(string searchValue);
        DataTable GetAttandance(string searchVal, bool flg);
    }
    class ManagerFunctions:MakeConnection,IManager
    {
        DataTable dt;
        DoctorFunctions df = new DoctorFunctions();
        public void UpdateStockRequests(string Val)
        {
            cmd.CommandText = "update StockRequests set Delivered='true' where Name=@Val";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public int CheckExistenceOfStock(string Val,bool flg)
        {
            try
            {
                if (flg)
                    cmd.CommandText = "select * from Medicine_Stock where Name=@Val";
                else
                    cmd.CommandText = "select * from Equipments_Stock where Name=@Val";
                con.Open();
                cmd.Parameters.AddWithValue("@val", Val);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                    return Convert.ToInt32(r["Quantity"]);
                return -99;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public void AddStocks(string Val,bool flg,int quan)
        {
            //flg=true=>medi stock,false=>equip stock
            try
            {
                cmd.Connection = con;
                if (Val.Length.Equals(0) || quan <= 0)
                    throw new ArgumentNullException();
                int stock_exist = CheckExistenceOfStock(Val, flg);
                Console.WriteLine(stock_exist);
                if (flg)
                {
                    if (stock_exist > 0)
                    {
                        quan += stock_exist;
                        cmd.CommandText = "update Medicine_Stock set Quantity=@quan where Name=@Val";
                    }
                    else
                        cmd.CommandText = "insert into Medicine_Stock(Name,Quantity,Price_per_piece,MFG_Date,Expiry_Date) Values(@Val,@quan,'50','10-04-2020','10-04-2025')";
                }
                else
                {
                    if (stock_exist > 0)
                    {
                        quan += stock_exist;
                        cmd.CommandText = "update Equipments_Stock set Quantity=@quan where Name=@Val";
                    }
                    else
                        cmd.CommandText = "insert into Equipments_Stock(Name,Price,Quantity) Values(@Val,'10K',@quan)";
                }
                con.Open();
                cmd.Parameters.AddWithValue("@quan", quan);
                cmd.ExecuteNonQuery();
                con.Close();
                UpdateStockRequests(Val);
                cmd.Parameters.RemoveAt("@val");
                cmd.Parameters.RemoveAt("@quan");
                MessageBox.Show("Success", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                Console.WriteLine(etype);
                if (etype.Equals("System.FormatException") || etype.Equals("System.ArgumentNullException"))
                    MessageBox.Show("Enter valid data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               else
                    MessageBox.Show("Something went wrong,Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public DataTable GetAllRequests(bool flg,bool new_req)
        {
            dt = new DataTable();
            cmd.Connection = con;
            con.Open();
            if (new_req)
                cmd.CommandText = "select * from StockRequests where Delivered='false'";
            else
                cmd.CommandText = "select * from StockRequests where Flag=@flg";
            cmd.Parameters.AddWithValue("@flg", flg);
            dt.Load(cmd.ExecuteReader());
            cmd.Parameters.RemoveAt("@flg");
            con.Close();
            dt = df.ReverseRowsInDataTable(dt);
            return dt;
        }
        public DataTable GetAllUsers()
        {
            dt = new DataTable();
            cmd.Connection = con;
            cmd.CommandText = "select * from Users where not Role='Admin'";
            con.Open();
            dt.Load(cmd.ExecuteReader());
            dt.Columns.Remove("Password");
            con.Close();
            return dt;
        }
        //overloading
        public DataTable GetAllUsers(string searchValue)
        {
            dt = new DataTable();
            cmd.Connection = con;
            cmd.CommandText = "select * from Users where (Id like @searchValue+'%' or Name like @searchValue+'%') and not Role='Admin'";
            con.Open();
            cmd.Parameters.AddWithValue("@searchValue", searchValue);
            dt.Load(cmd.ExecuteReader());
            dt.Columns.Remove("Password");
            cmd.Parameters.RemoveAt("@searchValue");
            con.Close();
            return dt;

        }
        public DataTable GetAttandance(string searchVal,bool flg)
        {
            try
            {
                dt = new DataTable();
                DateTime DATE=DateTime.Now.Date;
                cmd.Connection = con;
                con.Open();
                if (flg)
                    cmd.CommandText = "select * from Attendance_Manager where Uid like @searchVal+'%' or UName like @searchVal+'%'";
                else
                {
                    DATE = Convert.ToDateTime(searchVal).Date;
                    cmd.CommandText = "select * from Attendance_Manager where Date=@DATE";
                }
                cmd.Parameters.AddWithValue("@DATE",DATE);
                cmd.Parameters.AddWithValue("@searchVal", searchVal);
                SqlDataReader r = cmd.ExecuteReader();
                cmd.Parameters.RemoveAt("@DATE");
                cmd.Parameters.RemoveAt("@searchVal");
                dt.Load(r);
                dt.Columns.Remove("Id");
                dt.Columns[1].ColumnName = "Username";
                dt.Columns[0].ColumnName = "UserId";
                con.Close();
                return dt;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
