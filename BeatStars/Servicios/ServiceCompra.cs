using System;
using BeatStars.Clases;


namespace BeatStars.Servicios
{
    public class ServiceCompra
    {
        public static void ListaCompras(string nombre){
            var Usuario = ServiceAuth.listCuentas.Find(user => user.NombreCuenta == nombre);
            foreach(var Compra in Usuario.ListaCompras){
            Console.WriteLine($"Hiciste una compra a {Compra.CorreoUsuario}, de {Compra.Total}€ el {Compra.FechaCompra}");
            }
            Console.WriteLine("*****Pulse una tecla para continuar...*****");
            Console.ReadKey();
            ServiceUsabilidad.MenuUser(nombre);
        }
    }
}