using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class Tarjeta
    {
        private int numTarjeta;
        private int puntos;
        private DateTime fecVen;

        public int NumTarjeta
        {
            get
            {
                return numTarjeta;
            }

            set
            {
                numTarjeta = value;
            }
        }

        public int Puntos
        {
            get
            {
                return puntos;
            }

            set
            {
                puntos = value;
            }
        }

        public DateTime FecVen
        {
            get
            {
                return fecVen;
            }

            set
            {
                fecVen = value;
            }
        }

        public Tarjeta()
        {
            numTarjeta = 0;
            puntos = 0;
            fecVen = DateTime.Today;

        }
        

        public Tarjeta(int numTarjeta, int puntos, DateTime fecVen)
        {
            this.NumTarjeta = numTarjeta;
            this.Puntos = puntos;
            this.FecVen = fecVen;
        }

        public eTarjeta Guardar(DCDataContext dcOri)
        {
            DCDataContext dc = dcOri;
            eTarjeta fila = new eTarjeta();
            fila.puntos = this.puntos;
            fila.fecVen = this.fecVen;

            if (this.numTarjeta == 0)
            {
                dc.eTarjetas.InsertOnSubmit(fila);
            }
            else
            {
                var res = from x in dc.eTarjetas where x.numTarjeta == this.numTarjeta select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                    fila.puntos = this.puntos;
                    fila.fecVen = this.fecVen;
                }
                else
                    throw new Exception("Id no encontrado en Tarjeta");

            }

            return fila;
        }
        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eTarjeta f = Guardar(dc);
            dc.SubmitChanges();
            this.numTarjeta = f.numTarjeta;

        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eTarjetas where x.numTarjeta == this.numTarjeta select x;
            if (res.Count() > 0)
            {
                dc.eTarjetas.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Tarjeta no encontrada");

        }

        public static List<Tarjeta> Buscar(string buscado = "")
        {
            List<Tarjeta> tarjetas = new List<Tarjeta>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eTarjetas
                      where buscado == ""
                      || x.puntos.ToString() == buscado.Trim()
                      || x.numTarjeta.ToString() == buscado.Trim()
                      select x;

            foreach (eTarjeta em in res)
            {
                tarjetas.Add(new Tarjeta(em.numTarjeta, (int)em.puntos, (DateTime)em.fecVen));
            }

            return tarjetas;
        }

        public override string ToString()
        {
            return Convert.ToString(numTarjeta);
        }

        public static Tarjeta BuscarPorId(int numTarjeta)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eTarjetas
                      where x.numTarjeta == numTarjeta
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new Tarjeta(x.numTarjeta, (int)x.puntos, (DateTime)x.fecVen);
            }
            return null;
        }
    }
}
