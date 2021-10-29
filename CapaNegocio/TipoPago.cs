using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    class TipoPago
    {
        private int id;
        private string nombre;
        private string descripcion;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

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

        public TipoPago()
        {
            id = 0;
            nombre = "";
            descripcion = "";

        }

        public TipoPago(int id, string nombre, string descripcion)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }

        public static TipoPago BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eTipoPagos
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new TipoPago(x.id, x.nombre, x.descripcion);
            }
            return null;
        }
    }
}
