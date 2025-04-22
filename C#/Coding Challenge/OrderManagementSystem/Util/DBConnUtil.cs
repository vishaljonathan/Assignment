using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection(string configFile)
        {
            SqlConnection sqlConnection;
            string connstr = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=OMS;Integrated Security=True;"; ;
            return new SqlConnection(connstr);
        }
    }
}
