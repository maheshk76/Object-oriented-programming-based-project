using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Hospital
{
    class DoctorFunctions:MakeConnection
    {
        public DataTable SearchPatient(string searchString)
        {
            DataTable g = new DataTable();
            cmd.Connection = con;
           cmd.CommandText = "select * from Patient_Record WHERE PName like @searchString+'%' or PAddress like @searchString+'%' or PContact like @searchString+'%'";
            
            cmd.Parameters.AddWithValue("@searchString", searchString);
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();
            g.Load(r);
            if (g.Rows.Count == 0)
                MessageBox.Show("No data found");
            con.Close();
            cmd.Parameters.RemoveAt("@searchString");
            return g;
        }
        public DataTable GetAllAppointments()
        {
            DataTable dt = new DataTable();
            cmd.Connection = con;
            cmd.CommandText = "select * from Appointsments where Approved_or_not='false'";
            con.Open();
            cmd.Parameters.AddWithValue("@Approved_or_not",false);
            SqlDataReader r = cmd.ExecuteReader();
            dt.Load(r);
            con.Close();
            return dt;
        }
        public DataTable GetPatient(int P_id,bool PData)
        {
            DataTable dt = new DataTable();
            cmd.Connection = con;
            if(PData)
                cmd.CommandText = "select * from Patient_Record where Id=@P_id";
            else
                cmd.CommandText = "select * from Patient_Presc where PId=@P_id";
            con.Open();
            cmd.Parameters.AddWithValue("@P_id", P_id);
            SqlDataReader r =cmd.ExecuteReader();
            dt.Load(r);
            con.Close();
            cmd.Parameters.RemoveAt("@P_id");
            return dt;
        }
        public void MakePrescription(int p_id,string medicine)
        {
            cmd.Connection = con;
           // try
            //{
                DataTable x = GetPatient(p_id,true);
                string pa_name = (from DataRow dr in x.Rows
                                  where (int)dr["Id"] == p_id
                                  select (string)dr["PName"]).FirstOrDefault();
                Console.WriteLine(pa_name);
            Console.WriteLine();
            int Uid = SessionClass.SessionId;
           
            cmd.CommandText = "select * from Users where Id=@Uid";
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                string uname = r["Name"].ToString();
            }
            con.Close();
                                  //x.Rows[0]["PName"].ToString();
                cmd.CommandText = "insert into Patient_Presc(PId,PName,Prescprition,Did,Dname) Values(@p_id,@pa_name,@medicine,@Uid,@uname)";
                con.Open();
                cmd.Parameters.AddWithValue("@p_id", p_id);
                cmd.Parameters.AddWithValue("@pa_name", pa_name);
                cmd.Parameters.AddWithValue("@medicine", medicine);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Success", "Done");
           /* }
            catch (Exception e)
            {
                MessageBox.Show("Patient data is not available", "Error");
            }*/
        }
    }
}
