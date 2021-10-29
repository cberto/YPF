using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    class CargaCombustibleSocio
    {
        private int id;
        private Combustible combustible;
        private double precio;
        private double cantidad;
        private DateTime fechaHora;
        private TipoPago tipoPago;
        private Empleado empleado;
        private int puntos;
        private Socio socio;
        private int fkCombustible;
        private int fkPago;
        private int fkEmpleado;
        private int fkSocio;


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

        public Combustible Combustible
        {
            get
            {
                return combustible;
            }

            set
            {
                combustible = value;
                FkCombustible = value.Id;

            }
        }

        public double Precio
        {
            get
            {
                return precio;
            }

            set
            {
                precio = value;
            }
        }

        public double Cantidad
        {
            get
            {
                return cantidad;
            }

            set
            {
                cantidad = value;
            }
        }

        public DateTime FechaHora
        {
            get
            {
                return fechaHora;
            }

            set
            {
                fechaHora = value;
            }
        }

        public TipoPago TipoPago
        {
            get
            {
                return tipoPago;
            }

            set
            {
                tipoPago = value;
                FkPago = value.Id;

            }
        }

        public Empleado Empleado
        {
            get
            {
                return empleado;
            }

            set
            {
                empleado = value;
                FkEmpleado = value.Dni;
            }
        }

        public int FkCombustible
        {
            get
            {
                return fkCombustible;
            }

            set
            {
                fkCombustible = value;
            }
        }

        public int FkPago
        {
            get
            {
                return fkPago;
            }

            set
            {
                fkPago = value;
            }
        }

        public int FkEmpleado
        {
            get
            {
                return fkEmpleado;
            }

            set
            {
                fkEmpleado = value;
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

        public CargaCombustibleSocio()
        {
            id = 0;
            precio = 0;
            cantidad = 0;
            fechaHora = DateTime.Today;
            fkCombustible = 0;
            fkEmpleado = 0;
            fkPago = 0;
            puntos = 0;
            fkSocio = 0;
        }

        public CargaCombustibleSocio(int id, double precio, double cantidad, DateTime fechaHora, int puntos, int fkCombustible, int fkPago, int fkEmpleado, int fkSocio)
        {
            this.id = id;
            this.combustible = Combustible.BuscarPorId(fkCombustible);
            this.precio = precio;
            this.cantidad = cantidad;
            this.fechaHora = fechaHora;
            this.tipoPago = TipoPago.BuscarPorId(fkPago);
            this.empleado = Empleado.BuscarPorDNI(fkEmpleado);
            this.puntos = puntos;
            this.socio = Socio.BuscarPorNumSocio(fkSocio);
            this.fkCombustible = fkCombustible;
            this.fkPago = fkPago;
            this.fkEmpleado = fkEmpleado;
            this.fkSocio = fkSocio;
        }

        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eCargaCombustibleSocio fila = new eCargaCombustibleSocio();

            if (this.id != 0)
            {
                var res = from x in dc.eCargaCombustibleSocios where x.id == this.id select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                }
                else
                    throw new Exception("Id no encontrado en CargaCombustibleSocio");
            }


            fila.precio = precio;
            fila.cantidad = cantidad;
            fila.fechaHora = fechaHora;
            fila.puntos = puntos;
            fila.fkCombustible = fkCombustible;
            fila.fkEmpleado = fkEmpleado;
            fila.fkPago = fkPago;
            fila.fkSocio = fkSocio;


            if (this.id == 0)
                dc.eCargaCombustibleSocios.InsertOnSubmit(fila);

            dc.SubmitChanges();
            this.id = fila.id;
        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCargaCombustibleSocios where x.id == this.id select x;
            if (res.Count() > 0)
            {
                dc.eCargaCombustibleSocios.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("CargaCombustibleSocio no encontrada");

        }

        public static List<CargaCombustibleSocio> Buscar(string buscado = "")
        {
            List<CargaCombustibleSocio> cargasS = new List<CargaCombustibleSocio>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCargaCombustibleSocios
                      where buscado == ""
                      || x.id.ToString() == buscado.Trim()
                      || x.precio.ToString() == buscado.Trim()
                      || x.puntos.ToString() == buscado.Trim()
                      || x.cantidad.ToString() == buscado.Trim()
                      || x.eCombustible.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eEmpleado.apyn.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eSocio.apyn.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eTipoPago.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eEmpleado.dni.ToString() == buscado.Trim()
                      || x.eSocio.numSocio.ToString() == buscado.Trim()
                      select x;

            foreach (eCargaCombustibleSocio em in res)
            {
                cargasS.Add(new CargaCombustibleSocio(em.id, (Double)em.precio, (Double)em.cantidad, (DateTime)em.fechaHora, (int)em.puntos, (int)em.fkCombustible, (int)em.fkPago, (int)em.fkEmpleado, (int)em.fkSocio));
            }

            return cargasS;
        }

        public static CargaCombustibleSocio BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCargaCombustibleSocios
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new CargaCombustibleSocio(x.id, (Double)x.precio, (Double)x.cantidad, (DateTime)x.fechaHora, (int)x.puntos, (int)x.fkCombustible, (int)x.fkPago, (int)x.fkEmpleado, (int)x.fkSocio);
            }
            return null;
        }
    }
}
