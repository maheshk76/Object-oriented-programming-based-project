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
    interface IAdmin
    {
          int HireUser(string a, string b, string c,
            string contact, string addr, string gender, string qual, string exp, int sal, string role);
            void Dismiss(int x);
    }
    class AdminFunctions:MakeConnection,IAdmin
    {
         public  int HireUser(string name, string pass, string email,
            string contact, string addr, string gender, string qual, string exp, int sal, string role)
        {
            try
            {
                cmd.Connection = con;
                cmd.CommandText = "insert into Users(Name,Password,Address,Email,Contact" +
                    ",Gender,Qualification,Experience,Salary,Role) Values(@name,@pass,@addr,@email,@contact,@gender,@qual,@exp,@sal,@role)";
                con.Open();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@pass",pass);
                cmd.Parameters.AddWithValue("@addr", addr);
                cmd.Parameters.AddWithValue("@email",email);
                cmd.Parameters.AddWithValue("@contact",contact);
                cmd.Parameters.AddWithValue("@qual", qual);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@exp", exp);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@sal", sal);
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.CommandText = "select * from Users where Contact=@contact";
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                int UID = -1;
                while (r.Read())
                    UID = Convert.ToInt32(r["Id"]);
                con.Close();
                cmd.Parameters.RemoveAt("@name");
                cmd.Parameters.RemoveAt("@pass");
                cmd.Parameters.RemoveAt("@addr");
                cmd.Parameters.RemoveAt("@email");
                cmd.Parameters.RemoveAt("@contact");
                cmd.Parameters.RemoveAt("@gender");
                cmd.Parameters.RemoveAt("@qual");
                cmd.Parameters.RemoveAt("@exp");
                cmd.Parameters.RemoveAt("@role");
                cmd.Parameters.RemoveAt("@sal");
                return UID;
            }
            catch (Exception)
            {
                return -99;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public void Dismiss(int Uid)
        {
            cmd.Connection = con;
            cmd.CommandText = "delete from Users where Id="+Uid;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully dismissed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
