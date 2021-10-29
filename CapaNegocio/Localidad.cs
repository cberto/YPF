using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class Localidad
    {
        private int id;
        private string nombre;
        private Ciudad ciudad;
        private int fkCiudad;

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

        public Ciudad Ciudad
        {
            get
            {
                return ciudad;
            }

            set
            {
                ciudad = value;
                FkCiudad = value.Id;
            }
        }

        public int FkCiudad
        {
            get
            {
                return fkCiudad;
            }

            set
            {
                fkCiudad = value;
            }
        }

        public Localidad()
        {
            id = 0;
            nombre = "";
            fkCiudad = 0;
        }

        public Localidad(int id, string nombre, int fkCiudad)
        {
            this.id = id;
            this.nombre = nombre;
            this.ciudad = Ciudad.BuscarPorId(fkCiudad);
            this.fkCiudad = fkCiudad;
        }

        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eLocalidad fila = new eLocalidad();

            if (this.id != 0) //detectamos que es uno nuevo
            {
                var res = from x in dc.eLocalidads where x.id == this.id select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                }
                else //no lo encontramos.. mostramos error
                    throw new Exception("Id no encontrado en Localidad");
            }


            
            fila.nombre = nombre;

            if (this.Ciudad.Id == 0)
            {
                fila.eCiudad = this.Ciudad.Guardar(dc);
            }
            else
            {
                fila.fkCiudad = fkCiudad;
            }

            if (this.id == 0)
                dc.eLocalidads.InsertOnSubmit(fila);

            dc.SubmitChanges();
            this.id = fila.id;
        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eLocalidads where x.id == this.id select x;
            if (res.Count() > 0)
            {
                dc.eLocalidads.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Localidad no encontrada");

        }

        public static List<Localidad> Buscar(string buscado = "")
        {
            List<Localidad> localidades = new List<Localidad>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eLocalidads
                      where buscado == ""
                      || x.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.id.ToString() == buscado.Trim()
                      || x.eCiudad.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      select x;

            foreach (eLocalidad em in res)
            {
                localidades.Add(new Localidad(em.id, em.nombre, (int)em.fkCiudad));
            }

            return localidades;
        }

        public static Localidad BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eLocalidads
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new Localidad(x.id, x.nombre, (int)x.fkCiudad);
            }
            return null;
        }
    }
}
