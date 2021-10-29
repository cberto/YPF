using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public static class Conexion
    {
        /*private static string server = @"SQLSERVER\SQLSERVER";
        private static string db = @"TrabajoYpf";
        private static string usuario = @"BDJorge";
        private static string clave = @"123";

        public static string DarStrConexion()
        {
            return string.Concat(@"Data Source=", server, ";Initial Catalog=", db,
                ";Persist Security Info=False;User ID=", usuario, ";Password=", clave);
        }*/
        private static string server = @"(localdb)\Servidor";
        private static string db = @"TrabajoYpf";

        public static string DarStrConexion()
        {
            return string.Concat(@"Data Source=", server, ";Initial Catalog=", db,
                ";Integrated Security = True");
        }

    }
}
