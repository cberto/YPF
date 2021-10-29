using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace TrabajoYpf
{
    public partial class FormTipoProducto : Form
    {
        private TipoProducto t = new TipoProducto();
        public FormTipoProducto()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoProducto tp = new TipoProducto();
                tp.Id = t.Id;
                tp.Nombre = txtNombre.Text;
                tp.Descripcion = txtDesc.Text;
                tp.Guardar();
                ZonaDatos(false);
                MessageBox.Show("Se guardo correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Buscar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ZonaDatos(bool mostrar)
        {
            txtNombre.Text = "";
            txtDesc.Text = "";
            pnlDatos.Enabled = mostrar;

        }

        private void Buscar()
        {
            dgvTipo.DataSource = null;
            dgvTipo.DataSource = TipoProducto.Buscar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ZonaDatos(true);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            TipoProducto tp = ObtenerSeleccionado();
            if (tp != null)
            {
                ZonaDatos(true);
                t.Id = tp.Id;
                txtNombre.Text = tp.Nombre;
                txtDesc.Text = tp.Descripcion;
            }
            else
                MessageBox.Show("Seleccione una marca", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private TipoProducto ObtenerSeleccionado()
        {
            if (dgvTipo.CurrentRow != null)
                return dgvTipo.CurrentRow.DataBoundItem as TipoProducto;

            return null;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ZonaDatos(false);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el tipo de producto?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TipoProducto m = ObtenerSeleccionado();
                if (m != null)
                {
                    m.Eliminar();
                    Buscar();
                }
                else
                    MessageBox.Show("Seleccione un tipo de producto", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvTipo.DataSource = null;
            dgvTipo.DataSource = TipoProducto.Buscar(txtBuscar.Text);
        }

        private void FormTipoProducto_Load(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
