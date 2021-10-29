using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class Marca
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

        public Marca()
        {
            id = 0;
            nombre = "";
            descripcion = "";
        }

        public Marca(int id, string nombre, string descripcion)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }

        public eMarca Guardar(DCDataContext dcOri)
        {
            DCDataContext dc = dcOri;
            eMarca fila = new eMarca();
            fila.nombre = this.nombre;
            fila.descripcion = this.descripcion;

            if (this.id == 0)
            {
                dc.eMarcas.InsertOnSubmit(fila);
            }
            else
            {
                var res = from x in dc.eMarcas where x.id == this.id select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                    fila.nombre = this.nombre;
                    fila.descripcion = this.descripcion;
                }
                else
                    throw new Exception("Id no encontrado en Marca");

            }

            return fila;
        }
        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eMarca f = Guardar(dc);
            dc.SubmitChanges();
            this.id = f.id;

        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eMarcas where x.id == this.id select x;
            if (res.Count() > 0)
            {
                dc.eMarcas.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Marca no encontrada");

        }

        public static List<Marca> Buscar(string buscado = "")
        {
            List<Marca> marcas = new List<Marca>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eMarcas
                      where buscado == ""
                      || x.descripcion.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.id.ToString() == buscado.Trim()
                      select x;

            foreach (eMarca em in res)
            {
                marcas.Add(new Marca(em.id, em.nombre, em.descripcion));
            }

            return marcas;
        }

        public override string ToString()
        {
            return nombre;
        }

        public static Marca BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eMarcas
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new Marca(x.id, x.nombre, x.descripcion);
            }
            return null;
        }

    }
}
