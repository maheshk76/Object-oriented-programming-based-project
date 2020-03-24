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
    class Patient_Management:MakeConnection
    {
        public List<string> GetDoctorList(string DoctorName)
        {
            //For assigning a doctor
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
                MessageBox.Show("Please try again", "Error");
                return l;
            }
            finally{
                con.Close();
            }
        }
        public void AddtoAppointments(int pid,string pname,string dname,DateTime appointdate)
        {
            int did = Convert.ToInt32(GetDoctorList(dname)[0]);
            
           try
            {
                cmd.Connection = con;
                cmd.CommandText = "insert into Appointsments(PatientId,PName,Doctor_Assigned,DoctorId,Approved_or_not,Date_of_Appoint) Values(@pid,@pname,@dname,@did,'false',@appointdate)";
                con.Open();
                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.Parameters.AddWithValue("@did", did);
                cmd.Parameters.AddWithValue("@dname", dname);

                cmd.Parameters.AddWithValue("@appointdate", appointdate);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                
            }
        }
        public int RegisterNewPatient(string pname,string gname,string paddress,int page,string PEmail,string pcontact, string pgender,DateTime bdate,string doctor_assinged)
        {
            DateTime adddate = DateTime.Now.Date;
            cmd.Connection = con;
            try
            {
                if (page <= 0)
                    return -99;
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
            //Add to aapointment table
                AddtoAppointments(Patient_Id, pname, doctor_assinged,adddate);
                return Patient_Id;
            }
            catch (Exception)
            {
                return -99;
            }

        }
        public int DischargePatient(int Pid,string pname)
        {
            DateTime disdate = DateTime.Now.Date;
            cmd.Connection = con;
            cmd.CommandText = "insert into Patient_Record(DisDate) Values(@disdate) where Id=@Pid and PName=@pname ";
            con.Open();
            cmd.Parameters.AddWithValue("@disdate",disdate);
            cmd.ExecuteNonQuery();
            con.Close();
            return 1;
        }       
    }
}
