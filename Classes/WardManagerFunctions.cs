using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Classes
{
    class WardManagerFunctions:MakeConnection
    {
        public DataTable GetWards()
        {
            DataTable dt = new DataTable();
            cmd.Connection = con;
            cmd.CommandText = "select * from WardManager";
            con.Open();
            dt.Load(cmd.ExecuteReader());
            dt.Columns.Remove("Id");
            con.Close();
            return dt;

        }
    }
}
