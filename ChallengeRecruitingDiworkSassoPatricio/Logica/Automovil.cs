namespace Logica
{
    public static class Automovil
    {
        public static long Guardar(string marca, string modelo, string patente, short tipo, short cantPuertas)
        {
            long idVehiculo = Persistencia.Vehiculo.Guardar(marca, modelo, patente);
            Persistencia.Automovil.Guardar(tipo, cantPuertas, idVehiculo);
            return idVehiculo;
        }
    }
}