using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class LugarRetiro
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

        public LugarRetiro()
        {
            id = 0;
            nombre = "";
            descripcion = "";
        }

        public LugarRetiro(int id, string nombre, string descripcion)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }
        public eLugarRetiro Guardar(DCDataContext dcOri)
        {
            DCDataContext dc = dcOri;
            eLugarRetiro fila = new eLugarRetiro();
            fila.nombre = this.nombre;
            fila.descripcion = this.descripcion;

            if (this.id == 0)
            {
                dc.eLugarRetiros.InsertOnSubmit(fila);
            }
            else
            {
                var res = from x in dc.eLugarRetiros where x.id == this.id select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                    fila.nombre = this.nombre;
                    fila.descripcion = this.descripcion;
                }
                else
                    throw new Exception("Id no encontrado en LugarRetiro");

            }

            return fila;
        }
        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eLugarRetiro f = Guardar(dc);
            dc.SubmitChanges();
            this.id = f.id;

        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eLugarRetiros where x.id == this.id select x;
            if (res.Count() > 0)
            {
                dc.eLugarRetiros.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("LugarRetiro no encontrada");

        }

        public static List<LugarRetiro> Buscar(string buscado = "")
        {
            List<LugarRetiro> lugares = new List<LugarRetiro>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eLugarRetiros
                      where buscado == ""
                      || x.descripcion.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.id.ToString() == buscado.Trim()
                      select x;

            foreach (eLugarRetiro em in res)
            {
                lugares.Add(new LugarRetiro(em.id, em.nombre, em.descripcion));
            }

            return lugares;
        }

        public override string ToString()
        {
            return nombre;
        }

        public static LugarRetiro BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eLugarRetiros
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new LugarRetiro(x.id, x.nombre, x.descripcion);
            }
            return null;
        }
    }
}
