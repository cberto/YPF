using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    class Empleado
    {
        private int dni;
        private string apyn;
        private DateTime fecnac;
        private Localidad localidad;
        private int fkLocalidad;


        public int Dni
        {
            get
            {
                return dni;
            }

            set
            {
                dni = value;
            }
        }

        public string Apyn
        {
            get
            {
                return apyn;
            }

            set
            {
                apyn = value;
            }
        }

        public DateTime Fecnac
        {
            get
            {
                return fecnac;
            }

            set
            {
                fecnac = value;
            }
        }

        public Localidad Localidad
        {
            get
            {
                return localidad;
            }

            set
            {
                localidad = value;
                FkLocalidad = value.Id;
            }
        }

        public int FkLocalidad
        {
            get
            {
                return fkLocalidad;
            }

            set
            {
                fkLocalidad = value;
            }
        }

        public Empleado()
        {
            dni = 0;
            apyn = "";
            fecnac = DateTime.Today;
            fkLocalidad = 0;
        }

        public Empleado(int dni, string apyn, DateTime fecnac, int fkLocalidad)
        {
            this.dni = dni;
            this.apyn = apyn;
            this.fecnac = fecnac;
            this.localidad = Localidad.BuscarPorId(fkLocalidad);
            this.fkLocalidad = fkLocalidad;
        }

        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eEmpleado fila = new eEmpleado();

            if (this.dni != 0) //detectamos que es uno nuevo
            {
                var res = from x in dc.eEmpleados where x.dni == this.dni select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                }
                else //no lo encontramos.. mostramos error
                    throw new Exception("Id no encontrado en Empleado");
            }


            fila.apyn = apyn;
            fila.fecnac = fecnac;
            fila.fkLocalidad = fkLocalidad;


            if (this.dni == 0)
                dc.eEmpleados.InsertOnSubmit(fila);

            dc.SubmitChanges();
            this.dni = fila.dni;
        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eEmpleados where x.dni == this.dni select x;
            if (res.Count() > 0)
            {
                dc.eEmpleados.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Empleado no encontrado");

        }

        public static List<Empleado> Buscar(string buscado = "")
        {
            List<Empleado> empleados = new List<Empleado>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eEmpleados
                      where buscado == ""
                      || x.apyn.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.dni.ToString() == buscado.Trim()
                      || x.eLocalidad.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      select x;

            foreach (eEmpleado em in res)
            {
                empleados.Add(new Empleado(em.dni, em.apyn, (DateTime)em.fecnac, (int)em.fkLocalidad));
            }

            return empleados;
        }

        public static Empleado BuscarPorDNI(int dni)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eEmpleados
                      where x.dni == dni
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new Empleado(x.dni, x.apyn, (DateTime)x.fecnac, (int)x.fkLocalidad);
            }
            return null;
        }
    }
}
