using EE;
using SIGAV_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios.Multi_idioma;
using SIGAV_Interfaces;

namespace SIGAV
{
    public partial class Clientes : Form, IObservador
    {
        public Clientes()
        {
            InitializeComponent();
            ActualizarDGV();
        }
        BLL_Cliente bll_cliente = new BLL_Cliente();
        BE_Cliente ee_Cliente = new BE_Cliente();
        BE_Idioma eE_Idioma = new BE_Idioma();
        public void ActualizarDGV()
        {
            DGVCliente.DataSource = null;
            DGVCliente.DataSource = bll_cliente.ListarClientes();
            DGVCliente.ForeColor = Color.Black;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                ee_Cliente.Nombre = txtNombreCliente.Text;
                ee_Cliente.Apellido = txtApellidoCliente.Text;
                ee_Cliente.Direccion = txtDireccion.Text;
                ee_Cliente.Telefono = Convert.ToInt32(txtTelefonoCliente.Text);
                ee_Cliente.Celular = Convert.ToInt32(txtCelular.Text);
                ee_Cliente.Fecha_Nac = Convert.ToDateTime(DTPFechaNac.Value);
                ee_Cliente.Nombre_Empresa = txtEmpresa.Text;
                bll_cliente.CrearCliente(ee_Cliente);
                ActualizarDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            ActualizarDGV();
            ChangeLenguage();
        }

        private void BtnUpdateCliente_Click(object sender, EventArgs e)
        {
            try
            {
                bll_cliente.UpdateCliente(ee_Cliente);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteCliente_Click(object sender, EventArgs e)
        {
            try
            {
                bll_cliente.EliminarCliente(ee_Cliente);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGVCliente_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                ee_Cliente = (BE_Cliente)DGVCliente.Rows[e.RowIndex].DataBoundItem;
                txtIDCliente.Text = Convert.ToString(ee_Cliente.ID);
                txtNombreCliente.Text = ee_Cliente.Nombre;
                txtApellidoCliente.Text = ee_Cliente.Apellido;
                txtDireccion.Text = ee_Cliente.Direccion;
                txtTelefonoCliente.Text = Convert.ToString(ee_Cliente.Telefono);
                txtCelular.Text = Convert.ToString(ee_Cliente.Celular);
                DTPFechaNac.Value = Convert.ToDateTime(ee_Cliente.Fecha_Nac);
                txtEmpresa.Text = ee_Cliente.Nombre_Empresa;
            }
            
        }

        void ChangeLenguage()
        {
            eE_Idioma = EE.BE_Idioma.SharedData.SelectedLenguage;
            Traduccion.Añadir(this);
            Traduccion.Idioma(eE_Idioma);
            update();
        }
        public void update()
        {
            Traduccion.Traducir(this);
        }
    }
}
