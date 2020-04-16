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
    class WardManagerFunctions:MakeConnection
    {
        public void AssignRoom(int PatientId,string Roomtype,int roomno)
        {
            cmd.Connection = con;
            DateTime date = DateTime.Now.Date;
            cmd.CommandText = "select * from Rooms where PatientId=@PatientId";
            con.Open();
            cmd.Parameters.AddWithValue("@PatientId", PatientId);
            SqlDataReader r = cmd.ExecuteReader();
            bool flg = false ;
            while (r.Read())
            {
                if (Convert.ToBoolean(r["Assigned"]).Equals(true))
                    flg = true;
            }
            con.Close();
            if (flg)
                MessageBox.Show("Already assigned a room", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                cmd.CommandText = "insert into Rooms(RoomNo,RoomType,WardNo,Assigned,PatientId,Date_of_Assigned)" +
                    " Values(@roomno,@RoomType,'1','true',@PatientId,@date)";
                con.Open();
                cmd.Parameters.AddWithValue("@roomno", roomno);
                cmd.Parameters.AddWithValue("@Roomtype", Roomtype);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Parameters.RemoveAt("@roomno");
                cmd.Parameters.RemoveAt("@RoomType");
                cmd.Parameters.RemoveAt("@date");
                MessageBox.Show("Success", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cmd.Parameters.RemoveAt("@PatientId");
        }
        public List<int> ShowAvailableRooms(string sel)
        {
            List<int> l = new List<int>();
            List<int> temp = new List<int>();
            string opr = "";
            if (sel.Equals("Delux")) opr = "SUM(DeluxRooms)"; else opr = "SUM(NormalRooms)";
            int totalroomtype = Convert.ToInt32(GetWards().Compute(opr, string.Empty));
            cmd.CommandText = "select * from Rooms where RoomType=@sel";
            con.Open();
            cmd.Parameters.AddWithValue("@sel", sel);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                if (Convert.ToBoolean(r["Assigned"]))
                    temp.Add(Convert.ToInt32(r["RoomNo"]));
            }
            for(int i = 1; i <= totalroomtype; i++) {
                if (temp.Contains(i))
                    continue;
                l.Add(i);
            }
            con.Close();
            cmd.Parameters.RemoveAt("@sel");
            return l;
        }
        public DataTable GetWards()
        {
            DataTable dt = new DataTable();
            cmd.Connection = con;
            cmd.CommandText = "select * from WardManager";
            con.Open();
            dt.Load(cmd.ExecuteReader());
            dt.Columns.Remove("Id");
            con.Close();
            return dt;
        }
    }
}
