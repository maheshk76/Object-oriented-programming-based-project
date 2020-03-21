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
        public DataTable GetPatient(string searchString,bool PData)
        {
           try
            {
                DataTable dt = new DataTable();
                cmd.Connection = con;
                if (PData)
                    cmd.CommandText = "select * from Patient_Record WHERE Id like @searchString+'%' or PName like @searchString+'%' or PAddress like @searchString+'%' or PContact like @searchString+'%'";
                else
                    cmd.CommandText = "select * from Patient_Presc where PId=@searchString";
                con.Open();
                cmd.Parameters.AddWithValue("@searchString", searchString);
                SqlDataReader r = cmd.ExecuteReader();
                dt.Load(r);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Data Found", "Info");
                    return null;
                }
                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong,Please try again","Error");
                return null;
            }
            finally
            {
                con.Close();
                cmd.Parameters.RemoveAt("@searchString");
            }
        }
        public void MakePrescription(int p_id,string medicine)
        {
            cmd.Connection = con;
            try
            {
                DataTable x = GetPatient(p_id.ToString(),true);
                string pa_name = (from DataRow dr in x.Rows
                                  where (int)dr["Id"] == p_id
                                  select (string)dr["PName"]).FirstOrDefault();
                Console.WriteLine(pa_name);
                Console.WriteLine();
                int Uid = SessionClass.SessionId;
                cmd.CommandText = "select * from Users where Id=@Uid";
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                string uname = "";
                while (r.Read())
                     uname = r["Name"].ToString();
                con.Close();
                cmd.CommandText = "insert into Patient_Presc(PId,PName,Prescprition,Did,Dname) Values(@p_id,@pa_name,@medicine,@Uid,@uname)";
                con.Open();
                cmd.Parameters.AddWithValue("@p_id", p_id);
                cmd.Parameters.AddWithValue("@pa_name", pa_name);
                cmd.Parameters.AddWithValue("@medicine", medicine);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Success", "Done");
            }
            catch (Exception e)
            {
                MessageBox.Show("Patient data is not available", "Info");
            }
        }
    }
}
