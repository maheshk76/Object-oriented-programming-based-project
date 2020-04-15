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
    class DoctorFunctions : MakeConnection
    {
        DataTable dt;
        public DataTable ReverseRowsInDataTable(DataTable inputTable)
        {
            DataTable outputTable = inputTable.Clone();
            for (int i = inputTable.Rows.Count - 1; i >= 0; i--)
                outputTable.ImportRow(inputTable.Rows[i]);
            return outputTable;
        }
        public void AddTestDetails(string PId,string Tests)
        {
            try
            {
                int PID = Convert.ToInt32(PId);
                cmd.Connection = con;
                cmd.CommandText = "insert into PatientDiagnosis(PatientId,Tests) Values(@PID,@Tests)";
                con.Open();
                cmd.Parameters.AddWithValue("@PID", PID);
                cmd.Parameters.AddWithValue("@Tests", Tests);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Success", "Done", MessageBoxButtons.OK, MessageBoxIcon.None);
                con.Close();
                cmd.Parameters.RemoveAt("@PID");
                cmd.Parameters.RemoveAt("@Tests");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Please try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public DataTable GetPatientReport(string Query)
        {
            try
            {
                int query = Convert.ToInt32(Query);
                dt = new DataTable();
                cmd.Connection = con;
                cmd.CommandText = "select * from PatientDiagnosis where PatientId=@query";
                con.Open();
                cmd.Parameters.AddWithValue("@query", query);
                dt.Load(cmd.ExecuteReader());
                cmd.Parameters.RemoveAt("@query");
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
                return dt;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Something went wrong,Please try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public DataTable GetAllAppointments(string ser,bool flg)
        {
            try
            {
                dt = new DataTable();
                cmd.Connection = con;
                int did = SessionClass.SessionId;
                //flg=true for doctor
                if (flg)
                {
                    if (ser.Length.Equals(0))
                        cmd.CommandText = "select PatientId,PName,Date_of_Appoint from Appointsments where Approved_or_not='false' and DoctorId=@did";
                    else
                        cmd.CommandText = "select PatientId,PName,Date_of_Appoint from Appointsments where (PatientId like @ser+'%' or PName like @ser+'%') and DoctorId=@did";
                }
                //flg=false for Receptionist to get all appointments
                else
                    cmd.CommandText = "select PatientId,PName,Date_of_Appoint,Doctor_Assigned from Appointsments where PatientId like @ser+'%' or PName like @ser+'%'";
                
                con.Open();
                cmd.Parameters.AddWithValue("@Approved_or_not", false);
                cmd.Parameters.AddWithValue("@ser", ser);
                cmd.Parameters.AddWithValue("@did", did);
                dt.Load(cmd.ExecuteReader());
                dt.Columns[1].ColumnName = "Patient Name";
                dt.Columns[2].ColumnName = "Date of Appointment";
                con.Close();
                cmd.Parameters.RemoveAt("@Approved_or_not");
                cmd.Parameters.RemoveAt("@did");
                cmd.Parameters.RemoveAt("@ser");
                dt = ReverseRowsInDataTable(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                MessageBox.Show("Something went wrong,Please try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public DataTable GetPatient(string searchString, bool PData)
        {
            try
            {
                dt = new DataTable();
                cmd.Connection = con;
                con.Open();
                //PData=true->Patient Details,PData=false->Presc details
                if (PData)
                    cmd.CommandText = "select * from Patient_Record WHERE Id like @searchString+'%' or PName like @searchString+'%' or PAddress like @searchString+'%' or PContact like @searchString+'%'";
                else
                    cmd.CommandText = "select * from Patient_Presc where PId=@searchString";

                cmd.Parameters.AddWithValue("@searchString", searchString);
                dt.Load(cmd.ExecuteReader());
                con.Close();
                cmd.Parameters.RemoveAt("@searchString");
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data found", "Info",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return null;
                }
                if (PData)
                {
                    dt.Columns[0].ColumnName = "PatientId";
                    dt.Columns[1].ColumnName = "Patient Name";
                    dt.Columns[3].ColumnName = "Residential Address";
                    dt.Columns[4].ColumnName = "Age";
                    dt.Columns[8].ColumnName = "Details";
                    dt.Columns[9].ColumnName = "Addmission Date";
                    dt.Columns[10].ColumnName = "Discharge Date";
                }
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.FormatException"))
                    MessageBox.Show("Enter valid data","Info",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                else if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("No Data Found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Something went wrong,Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public void MakePrescription(string p_id, string medicine)
        {
            try
            {
                DateTime DATE = DateTime.Now.Date;
                cmd.Connection = con;
                cmd.CommandText = "update Appointsments set Approved_or_not='true' where PatientId=@p_id";
                con.Open();
                cmd.Parameters.AddWithValue("@p_id", p_id);
                cmd.ExecuteNonQuery();
                con.Close();
                DataTable x = GetPatient(p_id, true);
                string pa_name = (from DataRow dr in x.Rows
                                  where dr["PatientId"].ToString() == p_id
                                  select (string)dr["Patient Name"]).FirstOrDefault();
                int user_id = SessionClass.SessionId;

                cmd.CommandText = "select * from Users where Id=@user_id";
                cmd.Parameters.AddWithValue("@user_id", user_id);
                
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                string user_name = "";
                while (r.Read())
                    user_name = r["Name"].ToString();
                con.Close();
                cmd.CommandText = "insert into Patient_Presc(PId,PName,Prescprition,Did,Dname,Date) Values(@p_id,@pa_name,@medicine,@user_id,@user_name,@DATE)";
                con.Open();
                cmd.Parameters.AddWithValue("@pa_name", pa_name);
                cmd.Parameters.AddWithValue("@user_name", user_name);
                cmd.Parameters.AddWithValue("@medicine", medicine);
                cmd.Parameters.AddWithValue("@DATE", DATE);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Success", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                
                cmd.Parameters.RemoveAt("@user_id");
                cmd.Parameters.RemoveAt("@pa_name");
                cmd.Parameters.RemoveAt("@user_name");
                cmd.Parameters.RemoveAt("@medicine");
                cmd.Parameters.RemoveAt("@DATE");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.FormatException"))
                    MessageBox.Show("Enter valid data","Info",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Something went wrong,Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                if (cmd.Parameters.Contains("@p_id"))
                    cmd.Parameters.RemoveAt("@p_id");
            }

        }
    }
}
