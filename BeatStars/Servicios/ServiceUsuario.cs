using System;
using BeatStars.Clases;
using System.IO;
using System.Text.Json;

namespace BeatStars.Servicios
{
    public class ServiceUsuario
    {
        public static void AñadirDinero(string nombre){
            decimal dinero=0;
            try {
            Console.WriteLine("¿Cuanto dinero quieres añadir?:");
            dinero = decimal.Parse(Console.ReadLine());
            }catch(Exception e){
                Console.Clear();
                Console.WriteLine("Ups, parece que ha ocurrido un error; intentelo de nuevo");
                AñadirDinero(nombre);
            }
            ServiceAuth.listCuentas.Find(user => user.NombreCuenta == nombre).Riñonera += dinero;
            
            string jsonStringUsers = JsonSerializer.Serialize(ServiceAuth.listCuentas);
            File.WriteAllText( $@"jsons/" + "users.json", jsonStringUsers);
            ServiceUsabilidad.MenuUser(nombre);
        }

        public static void ListaBeatsComprados(string nombre){
            var Usuario = ServiceAuth.listCuentas.Find(user => user.NombreCuenta == nombre);
            foreach(var beat in Usuario.ListaBeatsComprados){
            Console.WriteLine(beat.Nombre);
            }
            Console.WriteLine("*****Pulse una tecla para continuar...*****");
            Console.ReadKey();
            ServiceUsabilidad.MenuUser(nombre);
        }
    }
}