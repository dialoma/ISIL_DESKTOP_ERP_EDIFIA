using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar....
using Edifia_BE;
using System.Configuration;

namespace Edifia_ADO
{
    public class HabitanteADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dtr;

        public DataTable ListarHabitantes()
        {
            DataSet dts = new DataSet();
            cnx.ConnectionString = MiConexion.GetCnx();
            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Tb_Habitante_Listar";
            try
            {

                cmd.Parameters.Clear();
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dts, "Habitantes");
                return dts.Tables["Habitantes"];
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public HabitanteBE ConsultarHabitante(int id)
        {
            try
            {
                HabitanteBE objHabitanteBE = new HabitanteBE();

                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Tb_Habitante_Consultar";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", id);
                cnx.Open();

                dtr = cmd.ExecuteReader();

                if (dtr.HasRows == true)
                {
                    dtr.Read();
                    objHabitanteBE.id = Convert.ToInt32(dtr["id"]);
                    objHabitanteBE.nombre = dtr["nombre"].ToString();
                    objHabitanteBE.apellido = dtr["apellido"].ToString();
                    objHabitanteBE.documento = dtr["documento"].ToString();
                    objHabitanteBE.departamento_id = Convert.ToInt32(dtr["departamento_id"]);
                    objHabitanteBE.numero = Convert.ToInt16(dtr["numero"]);
                    objHabitanteBE.es_propietario = Convert.ToBoolean(dtr["es_propietario"]);
                    objHabitanteBE.fecha_ingreso = dtr["fecha_ingreso"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dtr["fecha_ingreso"]) : null;
                    objHabitanteBE.fecha_egreso = dtr["fecha_egreso"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dtr["fecha_egreso"]) : null;
                    objHabitanteBE.Fec_reg = Convert.ToDateTime(dtr["fec_reg"]);

                    // Asegúrate de que la columna foto existe antes de intentar leerla
                    if (dtr["foto"] != DBNull.Value)
                    {
                        objHabitanteBE.foto = (byte[])dtr["foto"];
                    }
                
            }
                dtr.Close();
                return objHabitanteBE;
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                }
            }
        }


        public Boolean InsertarHabitante(HabitanteBE objHabitanteBE)
        {
            try
            {
                // Validar fechas antes de la inserción
                ValidarFecha(objHabitanteBE.fecha_ingreso, "La fecha de ingreso no es válida.");

                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Tb_Habitante_Insertar";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@nombre", objHabitanteBE.nombre);
                cmd.Parameters.AddWithValue("@apellido", objHabitanteBE.apellido);
                cmd.Parameters.AddWithValue("@documento", objHabitanteBE.documento);
                cmd.Parameters.AddWithValue("@es_propietario", objHabitanteBE.es_propietario);
                cmd.Parameters.AddWithValue("@fecha_ingreso", objHabitanteBE.fecha_ingreso.HasValue ? (object)objHabitanteBE.fecha_ingreso.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@departamento_id", objHabitanteBE.departamento_id);
                cmd.Parameters.AddWithValue("@foto", objHabitanteBE.foto != null ? (object)objHabitanteBE.foto : DBNull.Value);
                cmd.Parameters.AddWithValue("@usuario_registro", objHabitanteBE.Usu_Registro); // Asegúrate de proporcionar este parámetro

                cnx.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                }
            }
        }

        private void ValidarFecha(DateTime? fecha, string mensajeError)
        {
            if (fecha.HasValue && (fecha.Value < new DateTime(1753, 1, 1) || fecha.Value > new DateTime(9999, 12, 31)))
            {
                throw new Exception(mensajeError);
            }
        }




        public Boolean ActualizarHabitante(HabitanteBE objHabitanteBE)
        {
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Tb_Habitante_Actualizar";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@id", objHabitanteBE.id);
                cmd.Parameters.AddWithValue("@nombre", objHabitanteBE.nombre);
                cmd.Parameters.AddWithValue("@apellido", objHabitanteBE.apellido);
                cmd.Parameters.AddWithValue("@documento", objHabitanteBE.documento);
                cmd.Parameters.AddWithValue("@departamento_id", objHabitanteBE.departamento_id);
                cmd.Parameters.AddWithValue("@es_propietario", objHabitanteBE.es_propietario);
                cmd.Parameters.AddWithValue("@fecha_Ingreso", objHabitanteBE.fecha_ingreso.HasValue ? (object)objHabitanteBE.fecha_ingreso.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@fecha_Egreso", objHabitanteBE.fecha_egreso.HasValue ? (object)objHabitanteBE.fecha_egreso.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@foto", objHabitanteBE.foto);
                cmd.Parameters.AddWithValue("@usuario_ult_mod", objHabitanteBE.Usu_Ult_Mod);

                cnx.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                }
            }
        }

        public Boolean EliminarHabitante(String strCodigo)
        {
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Tb_Habitante_Eliminar";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@id", strCodigo);

                cnx.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                }
            }
        }

        public DataTable ListarHabitantesReporteExcel()
        {
            DataSet dts = new DataSet();
            cnx.ConnectionString = MiConexion.GetCnx();
            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Tb_Habitante_Listar_reporte_excel";  // Aquí se usa el nuevo nombre del procedimiento almacenado
            try
            {
                cmd.Parameters.Clear();
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dts, "Habitantes");
                return dts.Tables["Habitantes"];
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

