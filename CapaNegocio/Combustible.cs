using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    class Combustible
    {
        private int id;
        private string nombre;
        private TipoCombustible tipoDeCombustible;
        private double precioPorLitro;
        private string fkTipo;

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

        public TipoCombustible TipoDeCombustible
        {
            get
            {
                return tipoDeCombustible;
            }

            set
            {
                tipoDeCombustible = value;
                FkTipo = value.Nombre;
            }
        }

        public double PrecioPorLitro
        {
            get
            {
                return precioPorLitro;
            }

            set
            {
                precioPorLitro = value;
            }
        }

        public string FkTipo
        {
            get
            {
                return fkTipo;
            }

            set
            {
                fkTipo = value;
            }
        }

        public Combustible()
        {
            id = 0;
            nombre = "";
            precioPorLitro = 0;
            fkTipo = "";
        }

        public Combustible(int id, string nombre, double precioPorLitro, string fkTipo)
        {
            this.id = id;
            this.nombre = nombre;
            this.tipoDeCombustible = TipoCombustible.BuscarPorFk(fkTipo);
            this.precioPorLitro = precioPorLitro;
            this.fkTipo = fkTipo;
        }

        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eCombustible fila = new eCombustible();

            if (this.id != 0) //detectamos que es uno nuevo
            {
                var res = from x in dc.eCombustibles where x.id == this.id select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                }
                else //no lo encontramos.. mostramos error
                    throw new Exception("Id no encontrado en Combustible");
            }


            fila.nombre = nombre;
            fila.precioPorLitro = precioPorLitro;
            fila.fkTipo = fkTipo;


            if (this.id == 0)
                dc.eCombustibles.InsertOnSubmit(fila);

            dc.SubmitChanges();
            this.id = fila.id;
        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCombustibles where x.id == this.id select x;
            if (res.Count() > 0)
            {
                dc.eCombustibles.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Combustible no encontrado");

        }

        public static List<Combustible> Buscar(string buscado = "")
        {
            List<Combustible> empleados = new List<Combustible>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCombustibles
                      where buscado == ""
                      || x.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.id.ToString() == buscado.Trim()
                      || x.eTipoCombustible.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.precioPorLitro.ToString() == buscado.Trim()
                      select x;

            foreach (eCombustible em in res)
            {
                empleados.Add(new Combustible(em.id, em.nombre, (double)em.precioPorLitro, em.fkTipo));
            }

            return empleados;
        }

        public static Combustible BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eCombustibles
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new Combustible(x.id, x.nombre, (double)x.precioPorLitro, x.fkTipo);
            }
            return null;
        }
    }
}
