using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class Socio
    {
        private int dni;
        private string apyn;
        private DateTime fecnac;
        private Localidad localidad;
        private Tarjeta tarjeta;
        private int numSocio;
        private int fkLocalidad;
        private int fkTarjeta;

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

        public Tarjeta Tarjeta
        {
            get
            {
                return tarjeta;
            }

            set
            {
                tarjeta = value;
                FkTarjeta = value.NumTarjeta;
            }
        }

        public int NumSocio
        {
            get
            {
                return numSocio;
            }

            set
            {
                numSocio = value;
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

        public int FkTarjeta
        {
            get
            {
                return fkTarjeta;
            }

            set
            {
                fkTarjeta = value;
            }
        }

        public Socio()
        {
            dni = 0;
            apyn = "";
            fecnac = DateTime.Today;
            numSocio = 0;
            fkLocalidad = 0;
            fkTarjeta = 0;
        }

        public Socio(int dni, string apyn, DateTime fecnac, int numSocio, int fkLocalidad, int fkTarjeta)
        {
            this.Dni = dni;
            this.Apyn = apyn;
            this.Fecnac = fecnac;
            this.Localidad = Localidad.BuscarPorId(fkLocalidad);
            this.Tarjeta = Tarjeta.BuscarPorId(fkTarjeta);
            this.NumSocio = numSocio;
            this.fkLocalidad = fkLocalidad;
            this.fkTarjeta = fkTarjeta;
        }

        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eSocio fila = new eSocio();

            if (this.numSocio != 0)
            {
                var res = from x in dc.eSocios where x.numSocio == this.numSocio select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                }
                else
                    throw new Exception("Id no encontrado en Socio");
            }


            fila.dni = dni;
            fila.apyn = apyn;
            fila.fkTarjeta = fkTarjeta;
            fila.fecnac = fecnac;
            fila.fkLocalidad = fkLocalidad;

            if (this.numSocio == 0)
                dc.eSocios.InsertOnSubmit(fila);

            dc.SubmitChanges();
            this.numSocio = fila.numSocio;
        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eSocios where x.numSocio == this.numSocio select x;
            if (res.Count() > 0)
            {
                dc.eSocios.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Socio no encontrado");

        }

        public static List<Socio> Buscar(string buscado = "")
        {
            List<Socio> socios = new List<Socio>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eSocios
                      where buscado == ""
                      || x.apyn.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.dni.ToString() == buscado.Trim()
                      || x.eLocalidad.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eTarjeta.numTarjeta.ToString() == buscado.Trim()
                      || x.numSocio.ToString() == buscado.Trim()
                      select x;

            foreach (eSocio em in res)
            {
                socios.Add(new Socio(em.dni, em.apyn, (DateTime)em.fecnac, em.numSocio, (int)em.fkLocalidad, (int)em.fkTarjeta));
            }

            return socios;
        }

        public static Socio BuscarPorNumSocio(int numSocio)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eSocios
                      where x.numSocio == numSocio
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new Socio(x.dni, x.apyn, (DateTime)x.fecnac, x.numSocio, (int)x.fkLocalidad, (int)x.fkTarjeta);
            }
            return null;
        }

        public override string ToString()
        {
            return string.Concat(numSocio, " - ", apyn);
        }
    }
}
