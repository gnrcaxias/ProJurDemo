using System.Configuration;
using System.Data.SqlClient;

namespace ProJur.DataAccess
{
    public class Configuracao
    {

        public static string getEnderecoFisicoSite()
        {
            return ConfigurationManager.AppSettings["EnderecoFisicoSite"];
        }

        public static string getEnderecoVirtualSite()
        {
            return ConfigurationManager.AppSettings["EnderecoVirtualSite"];
        }

        public static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ProJurConnectionString"].ConnectionString;
        }

        public SqlConnection getConnectionObject()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ProJurConnectionString"].ConnectionString);
        }

        public static string getEnderecoVirtualUpload()
        {
            return (getEnderecoVirtualSite() + "/Uploads");
        }

    }
}
