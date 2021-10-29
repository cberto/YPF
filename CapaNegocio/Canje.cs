using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Canje
    {
        private int numCanje;
        private Producto producto;
        private Socio socio;
        private int valor;
        private LugarRetiro lugarRetiro;
        private int fkProducto;
        private int fkSocio;
        private int fkLugar;

        public int NumCanje
        {
            get
            {
                return numCanje;
            }

            set
            {
                numCanje = value;
            }
        }

        public Producto Producto
        {
            get
            {
                return producto;
            }

            set
            {
                producto = value;
                FkProducto = value.Id;
            }
        }

        public Socio Socio
        {
            get
            {
                return socio;
            }

            set
            {
                socio = value;
                fkSocio = value.NumSocio;
            }
        }

        public int Valor
        {
            get
            {
                return valor;
            }

            set
            {
                valor = value;
            }
        }

        public LugarRetiro LugarRetiro
        {
            get
            {
                return lugarRetiro;
            }

            set
            {
                lugarRetiro = value;
                FkLugar = value.Id;
            }
        }
        [Browsable(false)]
        public int FkProducto
        {
            get
            {
                return fkProducto;
            }

            set
            {
                fkProducto = value;
            }
        }
        [Browsable(false)]
        public int FkSocio
        {
            get
            {
                return fkSocio;
            }

            set
            {
                fkSocio = value;
            }
        }
        [Browsable(false)]
        public int FkLugar
        {
            get
            {
                return fkLugar;
            }

            set
            {
                fkLugar = value;
            }
        }

        public Canje()
        {
            numCanje = 0;
            valor = 0;
            fkLugar = 0;
            fkProducto = 0;
            fkSocio = 0;
        }

        public Canje(int numCanje, int fkProducto, int fkSocio, int fkLugar)
        {
            this.numCanje = numCanje;
            this.producto = Producto.BuscarPorId(fkProducto);
            this.socio = Socio.BuscarPorNumSocio(fkSocio);
            this.valor = Producto.Valor;
            this.lugarRetiro = LugarRetiro.BuscarPorId(fkLugar);
            this.fkProducto = fkProducto;
            this.fkSocio = fkSocio;
            this.fkLugar = fkLugar;
        }

        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eCanje fila = new eCanje();

            if (this.numCanje != 0)
            {
                var res = from x in dc.eCanjes where x.numCanje == this.numCanje select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                }
                else
                    throw new Exception("Id no encontrado en Canje");
            }

            
            fila.fkLugar = fkLugar;
            fila.fkSocio = fkSocio;
            fila.fkProducto = fkProducto;


            if (this.numCanje == 0)
                dc.eCanjes.InsertOnSubmit(fila);

            dc.SubmitChanges();
            this.numCanje = fila.numCanje;
        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCanjes where x.numCanje == this.numCanje select x;
            if (res.Count() > 0)
            {
                dc.eCanjes.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Canje no encontrado");

        }

        public static List<Canje> Buscar(string buscado = "")
        {
            List<Canje> cargas = new List<Canje>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCanjes
                      where buscado == ""
                      || x.numCanje.ToString() == buscado.Trim()
                      || x.valor.ToString() == buscado.Trim()
                      || x.eLugarRetiro.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eSocio.apyn.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eProducto.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eSocio.numSocio.ToString() == buscado.Trim()
                      select x;

            foreach (eCanje em in res)
            {
                cargas.Add(new Canje(em.numCanje, (int)em.fkProducto, (int)em.fkSocio, (int)em.fkLugar));
            }

            return cargas;
        }

        public static Canje BuscarPorId(int numCanje)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCanjes
                      where x.numCanje == numCanje
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new Canje(x.numCanje, (int)x.fkProducto, (int)x.fkSocio, (int)x.fkLugar);
            }
            return null;
        }
    }
}
