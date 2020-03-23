using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    class Users:MakeConnection
    {
        public void Attendance(string user_name,string role,int user_id)
        {
            cmd.Connection = con;
            DateTime dt = DateTime.Now.Date;
            Console.WriteLine(dt);
            bool flg =true;
            cmd.CommandText = "Select * from Attendance_Manager where Uid=@user_id and Date=@dt";
            con.Open();
            cmd.Parameters.AddWithValue("@dt",dt);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                if (Convert.ToInt32(r["Uid"]) == user_id && Convert.ToDateTime(r["Date"])==dt)
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

            role = "";
            try
            {
                cmd.Connection = con;
                cmd.CommandText = "Select * from Users where Id=@user_id";
                cmd.Parameters.AddWithValue("@user_id", user_id);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int x = Convert.ToInt32(r["Id"]);//UserId
                    string y = r["Password"].ToString();
                    string rol = r["Role"].ToString();
                    string UserName = r["Name"].ToString();//username
                    if (y != pass || x == 0)
                    {
                        cmd.Parameters.Clear();
                        return "";
                    }
                    else
                    {
                        con.Close();
                        Attendance(UserName, rol, user_id);
                        role = rol;
                        return UserName;
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                MessageBox.Show("Please try after some time", "Error");
                return "";
            }

        }
        public void Logout()
        {
            //Clearing the Parameters
            SessionClass.SessionId = 0;
            cmd.Parameters.Clear();
        }
    }
}
