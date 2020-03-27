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
        DataTable dt = new DataTable();
        DoctorFunctions df = new DoctorFunctions();
        public int MakeTestResults(string PatientID,string TestResult)
        {
            try
            {
                dt = df.GetPatientReport(PatientID);
                if (dt == null)
                    return 0;
                string RES_ID=(from DataRow dr in dt.Rows where dr["Result"].ToString().Length.Equals(0)
                             select dr["Id"]).FirstOrDefault().ToString();
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
                    MessageBox.Show("Success", "Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                string etype = ex.GetType().ToString();
                if (etype.Equals("System.FormatException"))
                    MessageBox.Show("Enter valid data","Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (etype.Equals("System.Data.SqlClient.SqlException"))
                    MessageBox.Show("Patient data is not available", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (etype.Equals("System.NullReferenceException"))
                    MessageBox.Show("No any pending test(s) found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Something went wrong,Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
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
