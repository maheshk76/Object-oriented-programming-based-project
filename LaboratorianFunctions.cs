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
    class LaboratorianFunctions:MakeConnection
    {
        DataTable dt;
        DoctorFunctions df = new DoctorFunctions();
        public void MakeTestResults(string PatientID,string TestResult)
        {
            try
            {
                dt = df.GetPatientReport(PatientID);
                string RES_ID=(from DataRow dr in dt.Rows where dr["Result"].ToString().Length.Equals(0)
                             select dr["Id"]).FirstOrDefault().ToString();
                Console.WriteLine(RES_ID);
                DateTime date = DateTime.Now.Date;
                cmd.Connection = con;
                if (!(RES_ID.Equals(null)))
                {
                    cmd.CommandText = "update PatientDiagnosis set Result=@TestResult,TestDate=@date where Id=@RES_ID";
                    con.Open();
                    cmd.Parameters.AddWithValue("@TestResult", TestResult);
                    cmd.Parameters.AddWithValue("@RES_ID",RES_ID);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                Console.WriteLine(etype);
                if (etype.Equals("System.FormatException"))
                    MessageBox.Show("Enter valid data");
                else if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info");
                else if (etype.Equals("System.NullReferenceException"))
                    MessageBox.Show("No any pending test(s) found", "Info");
                else
                    MessageBox.Show("Please try again later", "Error");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                if (cmd.Parameters.Contains("@RES_ID"))
                    cmd.Parameters.RemoveAt("@RES_ID");
                if (cmd.Parameters.Contains("@TestResult"))
                    cmd.Parameters.RemoveAt("@TestResult");
                if (cmd.Parameters.Contains("@date"))
                    cmd.Parameters.RemoveAt("@date");
            }
        }
    }
}
