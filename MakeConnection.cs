using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Hospital
{
    public class MakeConnection
    {
        public static string cn = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=MyProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public SqlConnection con = new SqlConnection(cn);
        public static SqlCommand cmd = new SqlCommand();
    }
}