using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace MW_Pagenation_MVC_DB.Repository
{
    public static class AllDataContext
    {
        private static string ConnectionString = "";
        public static SqlConnection GetConnection()
        {
            ConnectionString = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("Pagenation");
            return new SqlConnection(ConnectionString);
        }
    }
}
