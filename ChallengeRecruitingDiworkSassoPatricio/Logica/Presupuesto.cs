using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public static class Presupuesto
    {
        public static decimal Calcular(List<Modelos.Desperfecto> desperfectos)
        {
            decimal total = 0;

            foreach (var desperfecto in desperfectos)
            {
                total += desperfecto.CostoManoObra;
                total += desperfecto.TiempoTrabajoEstimado * 130;

                foreach (var repuesto in desperfecto.Repuestos)
                {
                    total += repuesto.Precio;
                }
            }

            total *= 1.1M;

            return total;
        }

        public static long Guardar(decimal total, long idCliente, long idVehiculo)
        {
            return Persistencia.Presupuesto.Guardar(total, idCliente, idVehiculo);
        }

        public static void GuardarPresupuestoDesperfectoRepuesto(long idPresupuesto, long idDesperfecto, long idRepuesto)
        {
            Persistencia.Presupuesto.GuardarPresupuestoDefectoRepuesto(idPresupuesto, idDesperfecto, idRepuesto);
        }

        public static List<Modelos.Presupuesto> ObtenerTodos()
        {
            return Persistencia.Presupuesto.ObtenerTodos();
        }

        public static string ObtenerPresupuestoPromedioPorMarcaOModelo(string marca, string modelo)
        {
            return Persistencia.Presupuesto.ObtenerPresupuestoPromedioPorMarcaOModelo(marca, modelo);
        }

        public static string ObtenerPresupuestoSumatoriaAutosYMotos()
        {
            return Persistencia.Presupuesto.ObtenerPresupuestoSumatoriaAutosYMotos();
        }
    }
}
