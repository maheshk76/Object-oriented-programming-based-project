using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class Patient_Management:MakeConnection
    {
        public int RegisterNewPatient(string pname,string gname,string paddress,int page,string PEmail,int pcontact, string pgender,string PDetails, DateTime adddate)
        {
            cmd.Connection = con;
            cmd.CommandText = "insert into Patient_Record(PName,GuardianName,PAddress,PAge,PEmail,PContact,PGender,PDetails,AddDate) Values(@pname,@gname,@paddress,@page,@PEmail,@pcontact,@pgender,@PDetails,@adddate)";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return 1;
        }
        public int DischargePatient(int Pid,string pname)
        {
            cmd.Connection = con;
            cmd.CommandText = "delete from Patient_Record where Id=@Pid and PName=@pname";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return 1;
        }
        public SqlDataReader SearchPatient(string searchString)
        {
            cmd.Connection = con;
            cmd.CommandText = "select * from Patient_Record WHERE PName like '%@searchString%' or PAddress like '%@searchString%' or PContact like '%@searchString%'";
            SqlParameter p = new SqlParameter();
            cmd.Parameters.AddWithValue("@searchString", searchString);
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();
            r.Read();
            return r;
        }
    }
}
