using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace Presentacion
{
    public class VehiculoView
    {
        public VehiculoView(string patente, string modelo, string marca, string tipoVehiculo)
        {
            Patente = patente;
            Modelo = modelo;
            Marca = marca;
            TipoVehiculo = tipoVehiculo;
        }

        public VehiculoView(Vehiculo vehiculo)
        {
            Patente = vehiculo.Patente;
            Modelo = vehiculo.Modelo;
            Marca = vehiculo.Marca;
            TipoVehiculo = vehiculo.GetType().Name;
        }

        public string Patente { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string TipoVehiculo { get; set; }
    }
}
