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
        public DataTable GetRooms()
        {
            DataTable dt = new DataTable();
            cmd.Connection = con;
            cmd.CommandText = "select * from Rooms";
            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            dt.Columns.Remove("Id");
            dt.Columns[5].ColumnName = "Assigned On";
            return dt;
        }
        public void UpdateToPatientRecord(int PatientId,int roomno)
        {
            cmd.CommandText = "update Patient_Record set RoomNo=@roomno where Id=@PatientId";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void AssignRoom(int PatientId,string Roomtype,int roomno)
        {
            try
            {
                cmd.Connection = con;
                DateTime date = DateTime.Now.Date;
                int wardno = 1;
                if (roomno > 10)
                    wardno = 2;
                if ((from DataRow dr in GetRooms().Rows
                     where Convert.ToInt32(dr["PatientId"]) == PatientId
                     select Convert.ToBoolean(dr["Assigned"])).FirstOrDefault())
                    MessageBox.Show("Already assigned a room", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    cmd.CommandText = "insert into Rooms(RoomNo,RoomType,WardNo,Assigned,PatientId,Date_of_Assigned)" +
                        " Values(@roomno,@RoomType,@wardno,'true',@PatientId,@date)";
                    con.Open();
                    cmd.Parameters.AddWithValue("@PatientId", PatientId);
                    cmd.Parameters.AddWithValue("@roomno", roomno);
                    cmd.Parameters.AddWithValue("@Roomtype", Roomtype);
                    cmd.Parameters.AddWithValue("@wardno", wardno);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    UpdateToPatientRecord(PatientId,roomno);
                    MessageBox.Show("Success", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Something went wrong,Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                if(cmd.Parameters.Contains("@roomno"))
                    cmd.Parameters.RemoveAt("@roomno");
                if (cmd.Parameters.Contains("@RoomType"))
                    cmd.Parameters.RemoveAt("@RoomType");
                if (cmd.Parameters.Contains("@wardno"))
                    cmd.Parameters.RemoveAt("@wardno");
                if (cmd.Parameters.Contains("@date"))
                    cmd.Parameters.RemoveAt("@date");
                if (cmd.Parameters.Contains("@PatientId"))
                    cmd.Parameters.RemoveAt("@PatientId");
            }
        }
        //creates list of available rooms
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
