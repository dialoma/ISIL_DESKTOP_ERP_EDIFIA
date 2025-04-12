using Edifia_BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edifia_GUI
{
    public partial class HabitanteMan01 : Form
    {

        //1. Cargamos el BL y la grilla
        HabitanteBL objHabitanteBL = new HabitanteBL();
        DataView dtv;


        public HabitanteMan01()
        {
            InitializeComponent();
        }

        private void HabitanteMan01_Load(object sender, EventArgs e)
        {
            // dtgDatos es el nombre del DataGridView
            dtgDatos.AutoGenerateColumns = false;
            dtgDatos.DataSource = objHabitanteBL.ListarHabitante();
            lblRegistros.Text = dtgDatos.Rows.Count.ToString();

        }
        private void CargarDatosHabitante(String strFiltro)
        {
            dtv = new DataView(objHabitanteBL.ListarHabitante());
            dtv.RowFilter = "apellido like '%" + strFiltro + "%'";
            dtgDatos.DataSource = dtv;
            lblRegistros.Text = dtgDatos.Rows.Count.ToString();

            // Configurar las columnas del DataGridView
            if (dtgDatos.Columns["id"] == null)
            {
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "id", DataPropertyName = "id", HeaderText = "ID", Visible = false });
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "nombre", DataPropertyName = "nombre", HeaderText = "Nombre" });
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "apellido", DataPropertyName = "apellido", HeaderText = "Apellido" });
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "documento", DataPropertyName = "documento", HeaderText = "Documento" });
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "es_propietario", DataPropertyName = "es_propietario", HeaderText = "Es Propietario" });
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", HeaderText = "Estado" });
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "fecha_ingreso", DataPropertyName = "fecha_ingreso", HeaderText = "Fecha Ingreso" });
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "fecha_egreso", DataPropertyName = "fecha_egreso", HeaderText = "Fecha Egreso" });
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Departamento", DataPropertyName = "numero", HeaderText = "Departamento" });
                dtgDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Edificio", DataPropertyName = "Edificio", HeaderText = "Edificio" });
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarDatosHabitante(txtFiltro.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }

        }
        //Abrir el form de Agregar NUevo Habitante
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                HabitanteMan02 frmHabitanteMan = new HabitanteMan02();
                frmHabitanteMan.ShowDialog();

                CargarDatosHabitante(txtFiltro.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //Abrir el form de Actualizar Habitante
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que haya una fila seleccionada
                if (dtgDatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un habitante", "Aviso",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Obtener el ID del habitante seleccionado

                int id = Convert.ToInt32(dtgDatos.SelectedRows[0].Cells["id"].Value);


                // Abrir el formulario de actualización
                HabitanteMan03 frm = new HabitanteMan03(id);
                frm.Codigo = id;
                frm.ShowDialog();
                

                // Recargar datos después de cerrar el formulario
                CargarDatosHabitante(txtFiltro.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void dtgDatos_DoubleClick(object sender, EventArgs e)
        {
            btnActualizar.PerformClick();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult vrpta = MessageBox.Show("¿Seguro de eliminar el registro?", "Menesaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (vrpta == DialogResult.Yes)
                {
                    String strCodigo = dtgDatos.CurrentRow.Cells[0].Value.ToString();
                    if (objHabitanteBL.EliminarHabitante(strCodigo) == true)
                    {
                        CargarDatosHabitante(txtFiltro.Text.Trim());
                    }
                    else
                    {
                        throw new Exception("No se logro ejectuar la accion. Consulte con IT");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
