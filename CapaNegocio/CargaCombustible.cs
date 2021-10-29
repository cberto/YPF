using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    class CargaCombustible
    {
        private int id;
        private Combustible combustible;
        private double precio;
        private double cantidad;
        private DateTime fechaHora;
        private TipoPago tipoPago;
        private Empleado empleado;
        private int fkCombustible;
        private int fkPago;
        private int fkEmpleado;


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

        public CargaCombustible()
        {
            id = 0;
            precio = 0;
            cantidad = 0;
            fechaHora = DateTime.Today;
            fkCombustible = 0;
            fkEmpleado = 0;
            fkPago = 0;
        }

        public CargaCombustible(int id, double precio, double cantidad, DateTime fechaHora, int fkCombustible, int fkPago, int fkEmpleado)
        {
            this.id = id;
            this.combustible = Combustible.BuscarPorId(fkCombustible);
            this.precio = precio;
            this.cantidad = cantidad;
            this.fechaHora = fechaHora;
            this.tipoPago = TipoPago.BuscarPorId(fkPago);
            this.empleado = Empleado.BuscarPorDNI(fkEmpleado);
            this.fkCombustible = fkCombustible;
            this.fkPago = fkPago;
            this.fkEmpleado = fkEmpleado;
        }

        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eCargaCombustible fila = new eCargaCombustible();

            if (this.id != 0)
            {
                var res = from x in dc.eCargaCombustibles where x.id == this.id select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                }
                else
                    throw new Exception("Id no encontrado en CargaCombustible");
            }


            fila.precio = precio;
            fila.cantidad = cantidad;
            fila.fechaHora = fechaHora;
            fila.fkCombustible = fkCombustible;
            fila.fkEmpleado = fkEmpleado;
            fila.fkPago = fkPago;


            if (this.id == 0)
                dc.eCargaCombustibles.InsertOnSubmit(fila);

            dc.SubmitChanges();
            this.id = fila.id;
        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCargaCombustibles where x.id == this.id select x;
            if (res.Count() > 0)
            {
                dc.eCargaCombustibles.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("CargaCombustible no encontrada");

        }

        public static List<CargaCombustible> Buscar(string buscado = "")
        {
            List<CargaCombustible> cargas = new List<CargaCombustible>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCargaCombustibles
                      where buscado == ""
                      || x.id.ToString() == buscado.Trim()
                      || x.precio.ToString() == buscado.Trim()
                      || x.cantidad.ToString() == buscado.Trim()
                      || x.eCombustible.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eEmpleado.apyn.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eTipoPago.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eEmpleado.dni.ToString() == buscado.Trim()
                      select x;

            foreach (eCargaCombustible em in res)
            {
                cargas.Add(new CargaCombustible(em.id, (Double)em.precio, (Double)em.cantidad, (DateTime)em.fechaHora, (int)em.fkCombustible, (int)em.fkPago, (int)em.fkEmpleado));
            }

            return cargas;
        }

        public static CargaCombustible BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCargaCombustibles
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new CargaCombustible(x.id, (Double)x.precio, (Double)x.cantidad, (DateTime)x.fechaHora, (int)x.fkCombustible, (int)x.fkPago, (int)x.fkEmpleado);
            }
            return null;
        }
    }
}
