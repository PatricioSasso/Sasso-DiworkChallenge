using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public static class Presupuesto
    {
        public static long Guardar(decimal total, long idCliente, long idVehiculo)
        {
            long id = 0;

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { nameof(total), total },
                { nameof(idCliente), idCliente },
                { nameof(idVehiculo), idVehiculo }
            };

            var dtr = DatabaseAccess.executeStoredProcedure("PresupuestoAdd", parametros);

            if(dtr.Read())
            {
                id = Convert.ToInt64(dtr["id"]);
            }

            return id;
        }

        public static void GuardarPresupuestoDefectoRepuesto(long idPresupuesto, long idDesperfecto, long idRepuesto)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { nameof(idPresupuesto), idPresupuesto },
                { nameof(idDesperfecto), idDesperfecto },
                { nameof(idRepuesto), idRepuesto }
            };

            var dtr = DatabaseAccess.executeStoredProcedure("PresupuestoDesperfectoRepuestoAdd", parametros);
        }

        public static List<Modelos.Presupuesto> ObtenerTodos()
        {
            List<Modelos.Presupuesto> presupuestos = new List<Modelos.Presupuesto>();

            var dtr = DatabaseAccess.executeStoredProcedure("PresupuestoLoad");

            while (dtr.Read())
            {
                presupuestos.Add(new Modelos.Presupuesto(Convert.ToInt64(dtr["Id"]),
                                                         Convert.ToDecimal(dtr["Total"]),
                                                         Persistencia.Cliente.Obtener(Convert.ToInt64(dtr["idCliente"])),
                                                         Persistencia.Automovil.Obtener(Convert.ToInt64(dtr["idVehiculo"]))));
            }

            return presupuestos;
        }

        public static string ObtenerPresupuestoPromedioPorMarcaOModelo(string marca, string modelo)
        {
            string presupuesto = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { nameof(marca), marca },
                { nameof(modelo), modelo }
            };

            var dtr = DatabaseAccess.executeStoredProcedure("PresupuestoPromedioPorMarcaOModelo", parametros);

            if (dtr.Read())
            {
                if(!string.IsNullOrEmpty(Convert.ToString(dtr["Promedio"])))
                {
                    presupuesto = "Promedio : " + Convert.ToString(dtr["Promedio"]);
                }
            }

            return presupuesto; 
        }

        public static string ObtenerPresupuestoSumatoriaAutosYMotos()
        {
            string presupuesto = "";

            var dtr = DatabaseAccess.executeStoredProcedure("PresupuestoSumatoriaAutosYMotos");

            if (dtr.Read())
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dtr["Sumatoria"])))
                {
                    presupuesto = "Sumatoria : " + Convert.ToString(dtr["Sumatoria"]);
                }
            }

            return presupuesto;
        }
        
    }
}
