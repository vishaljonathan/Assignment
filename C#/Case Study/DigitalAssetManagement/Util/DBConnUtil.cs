using Microsoft.Data.SqlClient;

namespace DigitalAssetManagement.Util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection(string configFile)
        {
            SqlConnection sqlConnection;
            string connstr = DBPropertyUtil.GetConnectionString(configFile);
            return new SqlConnection(connstr);
        }
    }
}
