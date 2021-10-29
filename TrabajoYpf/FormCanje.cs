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
    public partial class FormCanje : Form
    {
        private Canje objeto;
        public FormCanje()
        {
            InitializeComponent();
        }

        private void FormCanje_Load(object sender, EventArgs e)
        {
            cmbProd.DataSource = Producto.Buscar();
            cmbLugar.DataSource = LugarRetiro.Buscar();
            cmbSocio.DataSource = Socio.Buscar();
            dgv.DataSource = null;
            dgv.DataSource = Canje.Buscar();
            ZonaDatos(false);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ZonaDatos(true);
            objeto = new Canje();
        }

        private void ZonaDatos(bool mostrar)
        {
            numValor.Value = 0;
            cmbProd.Text = "";
            cmbLugar.Text = "";
            cmbSocio.Text = "";
            pnlDatos.Enabled = mostrar;

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow != null)
            {
                ZonaDatos(true);
                objeto = dgv.CurrentRow.DataBoundItem as Canje;
                CargarSeleccionado();

            }
            else
                MessageBox.Show("Seleccione un canje", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarSeleccionado()
        {
            numValor.Value = objeto.Valor;
            cmbProd.Text = objeto.Producto.ToString();
            cmbLugar.Text = objeto.LugarRetiro.ToString();
            cmbSocio.Text = objeto.Socio.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow != null)
            {
                objeto = dgv.CurrentRow.DataBoundItem as Canje;
                if (MessageBox.Show("¿Desea eliminar a " + objeto.ToString() + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    objeto.Eliminar();
                }
            }
            else
                MessageBox.Show("Seleccione un canje", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                objeto.Valor = (int)numValor.Value;
                objeto.Producto = cmbProd.SelectedItem as Producto;
                objeto.LugarRetiro = cmbLugar.SelectedItem as LugarRetiro;
                objeto.Socio = cmbSocio.SelectedItem as Socio;
                objeto.Guardar();
                ZonaDatos(false);
                Buscar(txtBuscar.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Buscar(string buscado)
        {
            dgv.DataSource = Canje.Buscar(buscado);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar(txtBuscar.Text);
        }

        private void pnlDatos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            numValor.Value = (cmbProd.SelectedItem as Producto).Valor;
        }
    }
}
