using Edifia_ADO;
using Edifia_BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edifia_BL
{
    public class HabitanteBL
    {
        HabitanteADO objHabitanteADO = new HabitanteADO();

        public DataTable ListarHabitante()
        {
            return objHabitanteADO.ListarHabitantes();
        }
        public HabitanteBE ConsultarHabitante(int id)
        {
            return objHabitanteADO.ConsultarHabitante(id);
        }

        public Boolean ActualizarHabitante(HabitanteBE objHabitanteBE)
        {
            return objHabitanteADO.ActualizarHabitante(objHabitanteBE);
        }
        public Boolean EliminarHabitante(String strCodigo)
        {
            return objHabitanteADO.EliminarHabitante(strCodigo);
        }
        public Boolean InsertarHabitante(HabitanteBE objHabitanteBE)
        {
            return objHabitanteADO.InsertarHabitante(objHabitanteBE);
        }

        public DataTable ListarHabitantesReporteExcel()
        {
            try
            {
                // Llamamos al método de la capa ADO y obtenemos el DataTable
                return objHabitanteADO.ListarHabitantesReporteExcel();
            }
            catch (Exception ex)
            {
                // Manejo de la excepción y re-lanzamiento de la misma
                throw new Exception("Error al listar los habitantes para el reporte Excel: " + ex.Message);
            }
        }

    }

}
