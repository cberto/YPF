using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class TipoCombustible
    {
        private string nombre;
        private string descripcion;

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public TipoCombustible()
        {
            nombre = "";
            descripcion = "";
        }

        public TipoCombustible(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public static TipoCombustible BuscarPorFk(string nombre)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eTipoCombustibles
                      where x.nombre == nombre
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new TipoCombustible(x.nombre, x.descripcion);
            }
            return null;
        }
    }
}
