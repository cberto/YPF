using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public static class Conexion
    {
        public static string DarStrConexion()
        {
            return CapaDatos.Conexion.DarStrConexion();
        }
    }
}
