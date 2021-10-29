using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class TipoProducto
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

        public TipoProducto()
        {
            id = 0;
            nombre = "";
            descripcion = "";
        }

        public TipoProducto(int id, string nombre, string descripcion)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }

        public eTipoProducto Guardar(DCDataContext dcOri)
        {
            DCDataContext dc = dcOri;
            eTipoProducto fila = new eTipoProducto();
            fila.nombre = this.nombre;
            fila.descripcion = this.descripcion;

            if (this.id == 0)
            {
                dc.eTipoProductos.InsertOnSubmit(fila);
            }
            else
            {
                var res = from x in dc.eTipoProductos where x.id == this.id select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                    fila.nombre = this.nombre;
                    fila.descripcion = this.descripcion;
                }
                else
                    throw new Exception("Id no encontrado en TipoProducto");

            }

            return fila;
        }
        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eTipoProducto f = Guardar(dc);
            dc.SubmitChanges();
            this.id = f.id;

        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eTipoProductos where x.id == this.id select x;
            if (res.Count() > 0)
            {
                dc.eTipoProductos.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Tipo de producto no encontrado");

        }

        public static List<TipoProducto> Buscar(string buscado = "")
        {
            List<TipoProducto> tipos = new List<TipoProducto>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eTipoProductos
                      where buscado == ""
                      || x.descripcion.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.id.ToString() == buscado.Trim()
                      select x;

            foreach (eTipoProducto em in res)
            {
                tipos.Add(new TipoProducto(em.id, em.nombre, em.descripcion));
            }

            return tipos;
        }

        public override string ToString()
        {
            return nombre;
        }

        public static TipoProducto BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eTipoProductos
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new TipoProducto(x.id, x.nombre, x.descripcion);
            }
            return null;
        }
    }
}
