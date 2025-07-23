using MySql.Data.MySqlClient;
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
        private string connectionString = "Server=127.0.0.1;Database=sistema_ventas;User ID=root;Password=Fallout4@;Port=3306;";


        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
