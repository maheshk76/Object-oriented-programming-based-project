using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class Users:MakeConnection
    {
        public void Attendance(string uname,string role,int Uid)
        {
            cmd.Connection = con;
            DateTime dt = DateTime.Now;
            cmd.CommandText = "insert into Attendance_Manager(Uid,UName,Role,Date,Present) Values(@Uid,@uname,@role,@dt,'true')";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public int Login(string uname, string pass,out string role)
        {
            cmd.Connection = con;
            cmd.CommandText = "Select * from Users where Name=@uname";
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@uname";
                p.Value = uname;
                cmd.Parameters.Add(p);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string x = r["Name"].ToString();
                    string y = r["Password"].ToString();
                    string rol = r["Role"].ToString();
                    int Uid = Convert.ToInt32(r["Id"]);
                    Console.WriteLine(x, y);
                if (y != pass || x == null)
                {
                    role = "";
                    return -99;
                }
                else
                {
                    Attendance(uname, rol, Uid);
                    role = rol;
                    return Uid;
                }
            }
                role = "";
            return -99;
        }
    }
}
