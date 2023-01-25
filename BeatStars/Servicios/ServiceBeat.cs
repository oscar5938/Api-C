using System;
using System.Collections.Generic;
using BeatStars.Clases;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace BeatStars.Servicios
{
    public class ServiceBeat
    {
        public static List<Beat> SubirBeat(string nombreUsuario, List<Beat> ListaBeats)
        {
            Console.WriteLine("Escribe el nombre de tu beat");
            string nombre = Console.ReadLine();
            int genero;
            string tipo;
            Console.Clear();
            try
            {
                Console.WriteLine("¿A que genero pertenece?\n 1: Pop\n 2: Rock \n 3: Regueton\n 4: Jazz \n 5: Tecno \n 6: Hardcore");
                genero = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Debes introducir un numero entre el 1 y el 6: ");
                return SubirBeat(nombreUsuario, ListaBeats);
            }

            switch (genero)
            {
                case 1:
                    tipo = "Pop";
                    break;
                case 2:
                    tipo = "Rock";
                    break;
                case 3:
                    tipo = "Rap";
                    break;
                case 4:
                    tipo = "Jazz";
                    break;
                case 5:
                    tipo = "Tecno";
                    break;
                case 6:
                    tipo = "Hardcore";
                    break;
                default:
                    Console.WriteLine("Debes introducir un numero entre el 1 y el 6: ");
                    return SubirBeat(nombreUsuario, ListaBeats);
            }
            string prem = "";
            int abombao = 0;
            while (!prem.Equals("s") && !prem.Equals("n"))
            {
                if (abombao > 0) { Console.WriteLine("Es 's' o 'n' abombao, no es tan dificil"); }
                Console.WriteLine("¿Va a ser premium? \n Introduce s/n: ");
                prem = Console.ReadLine();
                abombao++;
                Console.Clear();
            }
            Console.WriteLine("¿A cuanto lo quieres vender?");
            bool premium = false;
            decimal dineros = -1;
            if (prem.Equals("s"))
            {
                premium = true;
                while (dineros < 30)
                {
                    Console.WriteLine("El precio debe ser superior a 30 Euros: ");
                    dineros = decimal.Parse(Console.ReadLine());
                    Console.Clear();
                }
            }
            else
            {
                premium = false;
                while (dineros < 0 || dineros > 30)
                {
                    Console.WriteLine("(El precio debe ser inferior o igual a 30 Euros): ");
                    dineros = decimal.Parse(Console.ReadLine());
                    Console.Clear();
                }
            }
            Console.WriteLine("Gracias por aportar tu granito de arena a esta plataforma.");

            int id = 0;
            foreach (var beat in ListaBeats)
            {
                id++;
            }
            Beat nuevoBeat = new Beat(premium, nombre, tipo, dineros, id);
            foreach (var User in ServiceAuth.listCuentas)
            {
                if (User.NombreCuenta == nombreUsuario)
                {
                    User.ListaBeatsPublicados.Add(nuevoBeat);
                    User.ListaBeatsComprados.Add(nuevoBeat);
                }
            }
            ListaBeats.Add(nuevoBeat);

            string jsonStringBeats = JsonSerializer.Serialize(ListaBeats);
            File.WriteAllText($@"jsons/" + "beats.json", jsonStringBeats);
            string jsonStringUsers = JsonSerializer.Serialize(ServiceAuth.listCuentas);
            File.WriteAllText($@"jsons/" + "users.json", jsonStringUsers);
            return ListaBeats;
        }

        public static void ListaBeats()
        {
            Console.Clear();
            foreach (Beat a in ServiceAuth.listaBeats)
            {
                Console.WriteLine($"{a.ID} \t {a.Nombre} de tipo {a.Tipo} con precio {a.Precio}");
            }
             Console.WriteLine("*****Pulse una tecla para continuar...*****");
            Console.ReadKey();
        }
        public static void BorrarBeat(string nombreUsuario)
        {
            var beatsUsuario = ServiceAuth.listCuentas.Find(user => user.NombreCuenta == nombreUsuario).ListaBeatsPublicados;

            if (beatsUsuario.Any())
            {
                Console.WriteLine("Elige el ID del Beat que quieres eliminar: ");
                foreach (Beat a in beatsUsuario)
                {
                    Console.WriteLine($"{a.ID}-->{a.Nombre}");
                }

                Console.WriteLine("Escribe el ID de tu beat");
                string idBeatSeleccionado = Console.ReadLine();
                bool encontrado = false;

                foreach (Beat a in beatsUsuario)
                {
                    if (int.Parse(idBeatSeleccionado) == a.ID)
                    {
                        beatsUsuario.Remove(a);
                        Console.WriteLine($"El Beat {a.Nombre} ha sido borrado.");
                        encontrado = true;

                        //Serializar listCuentas y sobrescribir json
                        File.WriteAllText($@"jsons/" + "users.json", JsonSerializer.Serialize(ServiceAuth.listCuentas));

                        break;
                    }
                }
                if (!encontrado)
                {
                    Console.WriteLine($"El ID selccionado no existe en su lista de Beats publicados... vuelva a intentarlo.");
                }
            }
            else
            {
                Console.WriteLine($"Todavía no tienes Beats publicados... publique alguno y vuelva a intentarlo!");
            }
            ServiceUsabilidad.MenuUser(nombreUsuario);
        }

        public static void CompraBeat(string nombre, string nombreBeneficiado)
        {
            string masprod = "";
            bool repite = true;
            string beatElegido = "";
            int numeroBeats = 0;
            decimal total = 0;
            int metodopago = 0;
            bool tipoPago = false;
            List<Beat> carrito = new List<Beat>();
            var Usuario = ServiceAuth.listCuentas.Find(user => user.NombreCuenta == nombre);
            var megabeat = ServiceAuth.listaBeats.Find(beat => beat.Nombre == beatElegido);
            while (repite)
            {
                masprod = "";
                beatElegido = "";
                megabeat = null;
                Console.WriteLine("Elige el Beat que quieres comprar: ");
                foreach (Beat a in ServiceAuth.listaBeats)
                {
                    Console.WriteLine($"{a.Nombre} de tipo {a.Tipo} con precio {a.Precio}");
                }
                int contador = 0;
                while (megabeat == null)
                {
                    if (contador > 0)
                    {
                        Console.Clear();
                        foreach (Beat a in ServiceAuth.listaBeats)
                        {
                            Console.WriteLine($"{a.Nombre} de tipo {a.Tipo} con precio {a.Precio}");
                        }
                        Console.WriteLine("No se ha encontrado el beat");
                    }
                    contador++;
                    Console.WriteLine("Escribe el beat que quieres añadir o 0 para volver al menu:");
                    beatElegido = Console.ReadLine();
                    if (beatElegido.Equals("0"))
                    {
                        ServiceUsabilidad.MenuUser(nombre);
                    }
                    megabeat = ServiceAuth.listaBeats.Find(beat => beat.Nombre == beatElegido);
                }
                numeroBeats++;
                total += megabeat.Precio;
                carrito.Add(megabeat);
                while (!masprod.Equals("s") && !masprod.Equals("n"))
                {
                    Console.WriteLine($"Llevas {numeroBeats} articulo en el carrito por {total} \n¿Quieres añadir más?\n Inserte:  s/n");
                    masprod = Console.ReadLine();
                    Console.Clear();
                }
                if (masprod.Equals("n"))
                {
                    repite = false;
                }
            }

            if (Usuario.Riñonera < total)
            {
                Console.WriteLine("No tienes suficiente dinero; añade antes de comprar");
                ServiceUsabilidad.MenuUser(nombre);
            }
            while (metodopago != 1 && metodopago != 2)
            {
                Console.WriteLine("¿Va a pagar con paypal o con tarjeta?\n Inserte 1/2 (paypal)/(tarjeta)");
                metodopago = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            if (metodopago == 1)
            {
                tipoPago = false;
            }
            else
            {
                tipoPago = true;
            }

            int idcompra = 0;
            foreach (var compras in ServiceAuth.listCompras)
            {
                idcompra++;
            }

            Compra compra = new Compra(tipoPago, total, Usuario.Email, DateTime.Now, carrito, idcompra);
            ServiceAuth.listCompras.Add(compra);
            Console.WriteLine(megabeat.Nombre);

            var ComprasUsuario = ServiceAuth.listCuentas.Find(user => user.NombreCuenta == nombre).ListaCompras;
            ComprasUsuario.Add(compra);

            var BeatsBeneficiado = ServiceAuth.listCuentas.Find(user => user.NombreCuenta == nombreBeneficiado).ListaBeatsComprados;
            foreach (var a in carrito)
            {
                BeatsBeneficiado.Add(a);
            }

            string jsonStringUsers = JsonSerializer.Serialize(ServiceAuth.listCuentas);
            File.WriteAllText($@"jsons/" + "users.json", jsonStringUsers);
            string jsonStringBeats = JsonSerializer.Serialize(ServiceAuth.listCompras);
            File.WriteAllText($@"jsons/" + "compra.json", jsonStringBeats);
            ServiceUsabilidad.MenuUser(nombre);
        }

    }
}
