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
        public void Attendance(string user_name,string role,int Uid)
        {
            cmd.Connection = con;
            DateTime dt = DateTime.Now.Date;
            Console.WriteLine(dt);
            bool flg =true;
            cmd.CommandText = "Select * from Attendance_Manager where Uid=@Uid and Date=@dt";
            con.Open();
            cmd.Parameters.AddWithValue("@Uid", Uid);
            cmd.Parameters.AddWithValue("@dt",dt);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                if (Convert.ToInt32(r["Uid"]) == Uid && Convert.ToDateTime(r["Date"])==dt)
                    flg =false;
            }
            con.Close();
            if (flg)
            {
                cmd.CommandText = "insert into Attendance_Manager(Uid,UName,Role,Date,Present) Values(@Uid,@user_name,@role,@dt,'true')";
                con.Open();
                cmd.Parameters.AddWithValue("@user_name", user_name);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public string Login(int user_id, string pass,out string role)
        {
            cmd.Connection = con;
            cmd.CommandText = "Select * from Users where Id=@user_id";
            cmd.Parameters.AddWithValue("@user_id",user_id);
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                int x = Convert.ToInt32(r["Id"]);//UserId
                string y = r["Password"].ToString();
                string rol = r["Role"].ToString();
                string  UserName =r["Name"].ToString();//username
                if (y != pass || x == 0)
                {
                    role = "";
                    cmd.Parameters.Clear();
                    return "";
                }
                else
                {
                    con.Close();
                    Attendance(UserName, rol,user_id);
                    role = rol;
                    return UserName;
                }
            }
            role = "";
            cmd.Parameters.Clear();
            return "";
        }
        public void Logout()
        {
            //Clearing the Parameters
            SessionClass.SessionId = 0;
            cmd.Parameters.Clear();
        }
    }
}
