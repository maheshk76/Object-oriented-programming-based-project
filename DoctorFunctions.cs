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
            return dt;
        }
        public SqlDataReader GetPatient(int P_id)
        {
            cmd.Connection = con;
            cmd.CommandText = "select * from Patient_Record where Id=@P_id";
            con.Open();
            cmd.Parameters.AddWithValue("@P_id", P_id);
            SqlDataReader r =cmd.ExecuteReader();
            while(r.Read())
                return r;
            return r;
        }
        public void MakePrescription(int p_id,string medicine)
        {
            cmd.Connection = con;
            try
            {
                SqlDataReader x = GetPatient(p_id);
                string pa_name = x["PName"].ToString();
                cmd.CommandText = "insert into Patient_Presc(PId,PName,Prescprition) Values(@p_id,@pa_name,@medicine)";
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
                MessageBox.Show("Patient data is not available", "Error");
            }
        }
    }
}
