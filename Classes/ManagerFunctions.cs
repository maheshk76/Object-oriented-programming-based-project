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
        public DataTable GetAttandance(string dAte)
        {
           //  try
            //{
                dt = new DataTable();
            DateTime date = Convert.ToDateTime(dAte).Date;
                Console.WriteLine(date);
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select * from Attendance_Manager where Date=@date";
                cmd.Parameters.AddWithValue("@date", date);
                SqlDataReader r = cmd.ExecuteReader();
                dt.Load(r);
                cmd.Parameters.RemoveAt("@date");
                con.Close();
            if (dt.Rows.Count == 0)
                MessageBox.Show("Not FOund");
                return dt;
           /* }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }*/
        }
    }
}
