using System;
using System.Data;
using System.Windows.Forms;
using Edifia_BL;

namespace Edifia_GUI
{
    public partial class VisitaMan01 : Form
    {
        VisitaBL objVisitaBL = new VisitaBL();
        DataView dtv;

        public VisitaMan01()
        {
            InitializeComponent();
        }

        private void VisitaMan01_Load(object sender, EventArgs e)
        {
            dtgDatos.AutoGenerateColumns = false;
            CargarDatosVisita("");
        }

        private void CargarDatosVisita(string strFiltro)
        {
            try
            {
                // Obtener los datos desde el BL
                DataTable dt = objVisitaBL.ListarVisita();

                // Crear nuevas columnas como cadenas
                dt.Columns.Add("departamento_numero_str", typeof(string));
                dt.Columns.Add("documento_str", typeof(string));

                // Llenar las nuevas columnas con los valores convertidos
                foreach (DataRow row in dt.Rows)
                {
                    row["departamento_numero_str"] = row["departamento_numero"].ToString();
                    row["documento_str"] = row["documento"].ToString();
                }

                // Crear una vista de datos
                dtv = new DataView(dt);

                // Aplicar filtro si es necesario
                if (!string.IsNullOrEmpty(strFiltro))
                {
                    dtv.RowFilter = "nombre_visita LIKE '%" + strFiltro + "%' OR " +
                                    "departamento_numero_str LIKE '%" + strFiltro + "%' OR " +
                                    "documento_str LIKE '%" + strFiltro + "%' OR " +
                                    "edificio LIKE '%" + strFiltro + "%' OR " +
                                    "propietario_nombre LIKE '%" + strFiltro + "%' OR " +
                                    "area_comun_nombre LIKE '%" + strFiltro + "%' OR " +
                                    "proposito LIKE '%" + strFiltro + "%'";
                }

                // Asignar la vista al DataGridView
                dtgDatos.DataSource = dtv;

                // Ocultar la columna ID
                if (dtgDatos.Columns.Contains("id"))
                {
                    dtgDatos.Columns["id"].Visible = false;
                }

                // Actualizar el conteo de registros
                lblRegistros.Text = dtv.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarDatosVisita(txtFiltro.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                VisitaMan02 objVisitaMan02 = new VisitaMan02();
                objVisitaMan02.ShowDialog();
                CargarDatosVisita(txtFiltro.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay una fila seleccionada
                if (dtgDatos.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una visita para actualizar.");
                    return;
                }

                // Verificar si la celda "id" tiene un valor
                var idCell = dtgDatos.CurrentRow.Cells["id"].Value;
                if (idCell == null)
                {
                    MessageBox.Show("El registro seleccionado no tiene un ID válido.");
                    return;
                }

                // Convertir el valor de la celda a un entero
                int idVisita;
                if (!int.TryParse(idCell.ToString(), out idVisita))
                {
                    MessageBox.Show("El ID de la visita no es válido.");
                    return;
                }

                // Crear una instancia de VisitaMan03 y establecer el ID
                VisitaMan03 objVisitaMan03 = new VisitaMan03();
                objVisitaMan03.id = idVisita; // Establecer el ID correctamente

                // Mostrar el formulario de actualización
                objVisitaMan03.ShowDialog();

                // Al cerrar el formulario de actualización, se actualiza el DataGridView
                CargarDatosVisita(txtFiltro.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgDatos.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una visita para eliminar.");
                    return;
                }

                // Verificar si la celda "id" tiene un valor
                var idCell = dtgDatos.CurrentRow.Cells["id"].Value;
                if (idCell == null)
                {
                    MessageBox.Show("El registro seleccionado no tiene un ID válido.");
                    return;
                }

                // Convertir el valor de la celda a un entero
                int idVisita = Convert.ToInt32(idCell);

                DialogResult vrpta = MessageBox.Show("¿Está seguro que desea eliminar el registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (vrpta == DialogResult.Yes)
                {
                    if (objVisitaBL.EliminarVisita(idVisita.ToString())) 
                    {
                        CargarDatosVisita(txtFiltro.Text.Trim());
                    }
                    else
                    {
                        throw new Exception("No se logró ejecutar la acción. Consulte con el administrador.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
