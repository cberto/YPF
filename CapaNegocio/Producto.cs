using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class Producto
    {
        private int id;
        private string nombre;
        private TipoProducto tipoProducto;
        private Marca marca;
        private int valor;
        private int stock;
        private int fkMarca;
        private int fkTipo;

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

        public TipoProducto TipoProducto
        {
            get
            {
                return tipoProducto;
            }

            set
            {
                tipoProducto = value;
                FkTipo = value.Id;

            }
        }

        public Marca Marca
        {
            get
            {
                return marca;
            }

            set
            {
                marca = value;
                FkMarca = value.Id;
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

        public int Stock
        {
            get
            {
                return stock;
            }

            set
            {
                stock = value;
            }
        }

        public int FkMarca
        {
            get
            {
                return fkMarca;
            }

            set
            {
                fkMarca = value;
            }
        }

        public int FkTipo
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

        public Producto()
        {
            id = 0;
            nombre = "";
            valor = 0;
            stock = 0;
            fkMarca = 0;
            fkTipo = 0;
        }

        public Producto(int id, string nombre, int valor, int stock, int fkMarca, int fkTipo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.TipoProducto = TipoProducto.BuscarPorId(fkTipo);
            this.Marca = Marca.BuscarPorId(fkMarca);
            this.Valor = valor;
            this.Stock = stock;
            this.fkMarca = fkMarca;
            this.fkTipo = fkTipo;
        }

        public void Guardar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            eProducto fila = new eProducto();

            if (this.id != 0) //detectamos que es uno nuevo
            {
                var res = from x in dc.eProductos where x.id == this.id select x;
                if (res.Count() > 0)
                {
                    fila = res.First();
                }
                else //no lo encontramos.. mostramos error
                    throw new Exception("Id no encontrado en Producto");
            }

            
            fila.nombre = nombre;
            fila.valor = valor;
            fila.stock = stock;
            fila.fkTipo = fkTipo;
            fila.fkMarca = fkMarca;
            

            if (this.id == 0)
                dc.eProductos.InsertOnSubmit(fila);

            dc.SubmitChanges();
            this.id = fila.id;
        }

        public void Eliminar()
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eProductos where x.id == this.id select x;
            if (res.Count() > 0)
            {
                dc.eProductos.DeleteOnSubmit(res.First());
                dc.SubmitChanges();
            }
            else
                throw new Exception("Producto no encontrada");

        }

        public static List<Producto> Buscar(string buscado = "")
        {
            List<Producto> productos = new List<Producto>();
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eProductos
                      where buscado == ""
                      || x.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.id.ToString() == buscado.Trim()
                      || x.eMarca.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.eTipoProducto.nombre.ToLower().Trim().Contains(buscado.ToLower().Trim())
                      || x.valor.ToString() == buscado.Trim()
                      || x.stock.ToString() == buscado.Trim()
                      select x;

            foreach (eProducto em in res)
            {
                productos.Add(new Producto(em.id, em.nombre, (int)em.valor, (int)em.stock, (int)em.fkMarca, (int)em.fkTipo));
            }

            return productos;
        }

        public static Producto BuscarPorId(int id)
        {
            DCDataContext dc = new DCDataContext(Conexion.DarStrConexion());
            var res = from x in dc.eProductos
                      where x.id == id
                      select x;
            if (res.Count() > 0)
            {
                var x = res.First();
                return new Producto(x.id, x.nombre, (int)x.valor, (int)x.stock, (int)x.fkMarca, (int)x.fkTipo);
            }
            return null;
        }

        public override string ToString()
        {
            return string.Concat(marca.Nombre, " - ", nombre);
        }
    }
}
