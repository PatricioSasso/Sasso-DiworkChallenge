using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Automovil : Vehiculo
    {
        public Automovil()
        {
        }

        public Automovil(long id, string marca, string modelo, string patente)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Patente = patente;
        }

        public Automovil(Enumerables.TipoAutomovil tipoAutomovil, short cantidadPuertas)
        {
            TipoAutomovil = tipoAutomovil;
            CantidadPuertas = cantidadPuertas;
        }

        public Automovil(long id, string marca, string modelo, string patente, Enumerables.TipoAutomovil tipoAutomovil, short cantidadPuertas) : base(id, marca, modelo, patente)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Patente = patente;
            TipoAutomovil = tipoAutomovil;
            CantidadPuertas = cantidadPuertas;
        }

        public Enumerables.TipoAutomovil TipoAutomovil { get; set; }
        public short CantidadPuertas { get; set; }
    }
}
