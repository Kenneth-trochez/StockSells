using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSells
{
    public class ConexionBD
    {
        private readonly string connectionString = "Server=JOSE;Database=API_BD;Integrated Security=True;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(connectionString);
        }
    }
}
