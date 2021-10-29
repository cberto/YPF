using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class Ciudad
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

        public Ciudad()
        {
            id = 0;
            nombre = "";
            descripcion = "";

        }

        public Ciudad(int id, string nombre, string descripcion)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }

        public eCiudad Guardar(DCDataContext dcOri)
        {
            DCDataContext dc = dcOri;
            eCiudad fila = new eCiudad();
            fila.nombre = this.nombre;
            fila.descripcion = this.descripcion;

            if (this.id == 0)
            {
                dc.eCiudads.InsertOnSubmit(fila);
            }
            else
            {
                var res = from x in dc.eCiudads where x.id == this.id select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                    fila.nombre = this.nombre;
                    fila.descripcion = this.descripcion;
                }
                else
                    throw new Exception("Id no encontrado en Ciudad");

            }

            return fila;
        }
        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eCiudad f = Guardar(dc);
            dc.SubmitChanges();
            this.id = f.id;

        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCiudads where x.id == this.id select x;
            if (res.Count() > 0)
            {
                dc.eCiudads.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Ciudad no encontrada");

        }

        public static List<Ciudad> Buscar(string buscado = "")
        {
            List<Ciudad> marcas = new List<Ciudad>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCiudads
                      where buscado == ""
                      || x.descripcion.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.id.ToString() == buscado.Trim()
                      select x;

            foreach (eCiudad em in res)
            {
                marcas.Add(new Ciudad(em.id, em.nombre, em.descripcion));
            }

            return marcas;
        }

        public override string ToString()
        {
            return nombre;
        }

        public static Ciudad BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCiudads
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new Ciudad(x.id, x.nombre, x.descripcion);
            }
            return null;
        }
    }
}
