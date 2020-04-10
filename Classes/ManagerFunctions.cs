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
    class ManagerFunctions:MakeConnection
    {
        DataTable dt;
        DoctorFunctions df = new DoctorFunctions();
        public void AddStocks(string Val,bool flg,int quan)
        {
            cmd.Connection = con;
            con.Open();
            if(flg)
                cmd.CommandText = "insert into Medicine_Stock(Name,Quantity,Price_per_piece,MFG_Date,Expiry_Date) Values(@Val,@quan,'50','10-04-2020','10-04-2025')";
          
            cmd.Parameters.AddWithValue("@val", Val);
            cmd.Parameters.AddWithValue("@quan", quan);
            cmd.ExecuteNonQuery();
            cmd.Parameters.RemoveAt("@val");
            cmd.Parameters.RemoveAt("@quan");
            con.Close();
            MessageBox.Show("Success", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public DataTable GetAllRequests(bool flg,bool all)
        {
            dt = new DataTable();
            cmd.Connection = con;
            con.Open();
            if (all)
                cmd.CommandText = "select * from StockManager where Delivered='false'";
            else
                cmd.CommandText = "select * from StockManager where Flag=@flg";
            cmd.Parameters.AddWithValue("@flg", flg);
            dt.Load(cmd.ExecuteReader());
            cmd.Parameters.RemoveAt("@flg");
            con.Close();
            dt = df.ReverseRowsInDataTable(dt);
            return dt;
        }
        bool isNum(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (char.IsDigit(s[i]) == false)
                    return false;
            return true;
        }
        public DataTable GetAllUsers(string searchValue)
        {
            dt = new DataTable();
            cmd.Connection = con;
            if (searchValue.Length.Equals(0))
                cmd.CommandText = "select * from Users";
            else
                cmd.CommandText = "select * from Users where Id like @searchValue+'%' or Name like @searchValue+'%'";
            con.Open();
            cmd.Parameters.AddWithValue("@searchValue", searchValue);
            dt.Load(cmd.ExecuteReader());
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
