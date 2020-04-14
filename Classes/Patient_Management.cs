using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
namespace Hospital
{
    class Patient_Management:MakeConnection
    {
        readonly DoctorFunctions df = new DoctorFunctions();
        public DataTable GetBills(string query,out bool stat)
        {
            stat = false;
            try
            {
                DataTable dt = new DataTable();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select * from Patient_Bills where PId=@query";
                cmd.Parameters.AddWithValue("@query", query);
                SqlDataReader r = cmd.ExecuteReader();
                dt.Load(r);
                stat = (from DataRow dr in dt.Rows where dr["PId"].ToString() == query
                        select Convert.ToBoolean(dr["Payment_Status"])).FirstOrDefault();
                con.Close();
                return dt;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.FormatException"))
                    MessageBox.Show("Enter valid data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Something went wrong,Please try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                if (cmd.Parameters.Contains("@query"))
                cmd.Parameters.RemoveAt("@query");
            }
        }
        public List<string> GetDoctorList(string DoctorName)
        {
            //Returns a list to receptionist For assigning a doctor
            List<string> l = new List<string>();
            try
            {
                cmd.Connection = con;
                cmd.CommandText = "select * from Users where Role='Doctor'";
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    //return all doctor
                    if (DoctorName.Length.Equals(0))
                        l.Add(r["Name"].ToString());
                    //return selected doctor
                    else
                    {
                        if (r["Name"].ToString().Equals(DoctorName))
                            l.Add(r["Id"].ToString());
                    }
                }
                return l;
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong,Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return l;
            }
            finally{
                if(con.State==ConnectionState.Open)
                    con.Close();
            }
        }
        public void AddtoAppointments(string PID,string pname,string dname)
        {
            int flg =0;
            DateTime appointdate = DateTime.Now.Date;
            try
            {
                cmd.Connection = con;
                int pid = Convert.ToInt32(PID);
                DataTable x=df.GetAllAppointments(PID, false);
                if ((from DataRow dr in x.Rows select Convert.ToInt32(dr["PatientId"])).FirstOrDefault() == pid)
                {
                    pname = (from DataRow dr in x.Rows where Convert.ToInt32(dr["PatientId"]) == pid select dr["Patient Name"].ToString()).FirstOrDefault();
                    dname = (from DataRow dr in x.Rows where Convert.ToInt32(dr["PatientId"]) == pid select dr["Doctor_Assigned"].ToString()).FirstOrDefault();
                    flg = 1;
                    if ((from DataRow dr in x.Rows select Convert.ToDateTime(dr["Date of Appointment"])).FirstOrDefault() == appointdate)
                        flg = 2;
                }
                if (!(flg.Equals(2)))
                {
                    int did = Convert.ToInt32(GetDoctorList(dname)[0]);
                    cmd.CommandText = "insert into Appointsments(PatientId,PName,Doctor_Assigned,DoctorId,Approved_or_not,Date_of_Appoint) Values(@pid,@pname,@dname,@did,'false',@appointdate)";
                    con.Open();
                    cmd.Parameters.AddWithValue("@pid", pid);
                    cmd.Parameters.AddWithValue("@did", did);
                    cmd.Parameters.AddWithValue("@pname", pname);
                    cmd.Parameters.AddWithValue("@dname", dname);
                    cmd.Parameters.AddWithValue("@appointdate", appointdate);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmd.Parameters.RemoveAt("@pid");
                    cmd.Parameters.RemoveAt("@did");
                    cmd.Parameters.RemoveAt("@pname");
                    cmd.Parameters.RemoveAt("@dname");
                    cmd.Parameters.RemoveAt("@appointdate");
                }
                if (flg.Equals(2))
                    MessageBox.Show("Appointment exists for today", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (flg.Equals(1))
                    MessageBox.Show("New Appointment added", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.FormatException"))
                    MessageBox.Show("Enter valid data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Something went wrong,Please try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public int RegisterNewPatient(string pname,string gname,string paddress,int page,string PEmail,string pcontact, string pgender,DateTime bdate,string doctor_assinged)
        {
            DateTime adddate = DateTime.Now.Date;
            try
            {
                cmd.Connection = con;
                cmd.CommandText = "insert into Patient_Record(PName,GuardianName,PAddress,PAge,PEmail,PContact,PGender,AddDate,Birthdate) Values(@pname,@gname,@paddress,@page,@PEmail,@pcontact,@pgender,@adddate,@bdate)";
                con.Open();
                cmd.Parameters.AddWithValue("@pname", pname);
                cmd.Parameters.AddWithValue("@gname", gname);
                cmd.Parameters.AddWithValue("@paddress", paddress);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@PEmail", PEmail);
                cmd.Parameters.AddWithValue("@pcontact", pcontact);
                cmd.Parameters.AddWithValue("@pgender", pgender);
                cmd.Parameters.AddWithValue("@adddate", adddate);
                cmd.Parameters.AddWithValue("@bdate", bdate);
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.CommandText = "select * from Patient_record where PContact=@pcontact";
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                int Patient_Id = -1;
                while (r.Read())
                    Patient_Id = Convert.ToInt32(r["Id"]);
                con.Close();
                cmd.Parameters.RemoveAt("@pname");
                cmd.Parameters.RemoveAt("@gname");
                cmd.Parameters.RemoveAt("@paddress");
                cmd.Parameters.RemoveAt("@page");
                cmd.Parameters.RemoveAt("@PEmail");
                cmd.Parameters.RemoveAt("@pcontact");
                cmd.Parameters.RemoveAt("@pgender");
                cmd.Parameters.RemoveAt("@adddate");
                cmd.Parameters.RemoveAt("@bdate");
                //Add to aapointment table
                AddtoAppointments(Patient_Id.ToString(), pname, doctor_assinged);
                con.Open();
                cmd.Parameters.AddWithValue("@Pat", Patient_Id);
                cmd.CommandText = "insert into Patient_Bills(PId,Total,Medicines_Bill,Rent_Bill,Other_Fees) Values(@Patient_Id,0,0,0)";
                cmd.ExecuteNonQuery();
                
                con.Close();
                cmd.Parameters.RemoveAt("@Pat");
               return Patient_Id;
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
        public void DischargePatient(string PID)
        {
            try { 
            int Pid = Convert.ToInt32(PID);
            DateTime disdate = DateTime.Now.Date;
            cmd.Connection = con;
            cmd.CommandText = "update Patient_Record set DisDate=@disdate where Id=@Pid";
            con.Open();
            cmd.Parameters.AddWithValue("@disdate",disdate);
            cmd.Parameters.AddWithValue("@Pid",Pid);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Parameters.RemoveAt("@disdate");
            cmd.Parameters.RemoveAt("@Pid");
            MessageBox.Show("Success", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.FormatException"))
                    MessageBox.Show("Enter valid data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Something went wrong,Please try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }  
    }
}
