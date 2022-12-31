using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public static class Moto
    {
        public static long Guardar(string marca, string modelo, string patente, string cilindrada)
        {
            long idVehiculo = Persistencia.Vehiculo.Guardar(marca, modelo, patente);
            Persistencia.Moto.Guardar(cilindrada, idVehiculo);
            return idVehiculo;
        }
    }
}
