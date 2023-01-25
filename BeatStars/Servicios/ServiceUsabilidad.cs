using BeatStars.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BeatStars.Servicios
{
    public class ServiceUsabilidad
    {

        static DateTime dateTime = DateTime.UtcNow.Date;

        public static void TerminarPrograma()
        {
            System.Environment.Exit(0);
        }
        public static void Cuadrado(string texto)
        {
            int tam = 1; //int.Parse(Console.ReadLine());
            int i, j;
            for (i = 1; i <= tam; i++) // 15 lineas
            {
                for (j = 1; j <= 40; j++)
                {
                    // números a cada línea
                    Console.Write("*", j);
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine("\t\t" + texto);

            for (i = 1; i <= tam; i++) // 15 lineas
            {
                for (j = 1; j <= 40; j++)
                {
                    // números a cada línea
                    Console.Write("*", j);
                }
                Console.WriteLine(" ");
            }
        }

        public static bool CheckEmailPatern(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            else
            {
                Console.Clear();
                Cuadrado("Registro");
                Console.WriteLine("Email no valido, vuelve a intentarlo...");
                return false;
            }
        }

        public static bool CheckPassword(string pwd1, string pwd2)
        {
            if (pwd1 == pwd2)
            {
                return true;
            }
            else
            {
                Console.Clear();
                Cuadrado("Registro");
                Console.WriteLine("Las contraseñas no coinciden, vuelve a intentarlo...");
                return false;
            }
        }

        public static string CreatePassword(){
             string pwd, pwd2;
            do
            {
                string message="Introduce una contraseña";
                do{ Console.WriteLine(message);
                pwd = Console.ReadLine();
                message="Contraseña invalida; Escriba una contraseña más segura";} while (pwd.Length!=4);
                Console.WriteLine("Repite la contraseña");
                pwd2 = Console.ReadLine();
            } while (!CheckPassword(pwd, pwd2));
            return pwd;
        }

        public static bool CheckNombreCuentaExistente(string nombreCuenta)
        {
            bool check = true;

            bool exist = ServiceAuth.checkExistUsers();
            if (exist == true)
            {
                string jsonString = File.ReadAllText($@"jsons/users.json");
                List<Usuario> listaLeida = JsonSerializer.Deserialize<List<Usuario>>(jsonString);

                foreach (var user in listaLeida)
                {
                    if (user.NombreCuenta == nombreCuenta)
                    {
                        Console.Clear();
                        Cuadrado("Registro");
                        Console.WriteLine("El nombre de usuario ya existe, vuelve a intentarlo...");
                        EscribirLog("RegistrarUsuario/El nombre de usuario [" + nombreCuenta + "] ya existe, vuelve a intentarlo...");
                        check = false;
                        break;
                    }
                }
                return check;
            }

            return check;
        }

        public static bool CheckEmailExistente(string email)
        {
            bool checkExiste = true; //PASAR A FALSE SI YA EXISTE
            string jsonString = "";

            DirectoryInfo d = new DirectoryInfo($@"jsons/");//Comprobar si existe carpeta de jsons
            FileInfo[] FileUsers = d.GetFiles("users.json"); //Getting Text files

            if (FileUsers.Length != 0)
            {
                jsonString = File.ReadAllText($@"jsons/users.json");
                List<Usuario> listaLeida = JsonSerializer.Deserialize<List<Usuario>>(jsonString);
                foreach (var user in listaLeida)
                {
                    if (user.Email == email)
                    {
                        Console.Clear();
                        Cuadrado("Registro");
                        Console.WriteLine("Email ya registrado, vuelve a intentarlo...");
                        checkExiste = false;
                        break;
                    }
                }
                return checkExiste;
            }
            else
            {
                if (ServiceAuth.listCuentas.Any())
                {
                    foreach (var x in ServiceAuth.listCuentas)
                    {
                        if (email == x.Email)
                        {
                            Console.Clear();
                            Cuadrado("Registro");
                            Console.WriteLine("Email ya registrado, vuelve a intentarlo...");
                            checkExiste = false;
                            break;
                        }
                    }
                    return checkExiste;
                }
                else
                {
                    {
                        Console.WriteLine("¡Todavía no existen usuarios así que, enhorabuena eres el primero!");
                        return checkExiste;
                    }

                }
            }
        }
        public static void EscribirLog(string texto)
        {
            StringBuilder sb = new StringBuilder();
            texto = FechaActual() + texto;
            sb.Append(texto);
            sb.Append(Environment.NewLine);
            File.AppendAllText("./logs/errors/" + dateTime.ToString("dd-MM-yyyy") + ".txt", sb.ToString());
            sb.Clear();
        }

        public static string FechaActual()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string parseDate = dateTime.ToString("hh:mm") + "/";
            return parseDate;
        }

        public static void Compra()
        {

        }

        public static string nombreBeneficiado()
        {
            Console.WriteLine("¿A quien quieres regalar beats?\n Escribe su nombre de usuario");
            string nombre = Console.ReadLine();
            var nick = ServiceAuth.listCuentas.Find(user => user.NombreCuenta == nombre);
            while (nick == null)
            {
                Console.Clear();
                Console.WriteLine("No se reconoce al usuario\n Por favor, introduzca de nuevo su nombre:");
                nombre = Console.ReadLine();
                nick = ServiceAuth.listCuentas.Find(user => user.NombreCuenta == nombre);
            }
            return nombre;
        }

        public static int VerifyNum(){
            int opcionSel=0;
            bool optionCheck=false;
          do
            {
                try
                {
                    opcionSel = Int32.Parse(Console.ReadLine());
                    optionCheck = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Debe escribir un número");
                }
            } while (!optionCheck);
            return opcionSel;
        }


        public static int MenuUser(string nombreUsuario)
        {
            Console.WriteLine($"Bievenido a BeatStar {nombreUsuario}; ¿Que desea hacer?");
            Console.WriteLine($"1 - Lista Beats \n"
            + "2 - Comprar \n"
            + "3 - Regalar \n"
            + "4 - Subir Beat\n"
            + "5 - Borrar Beat\n"
            + "6 - Lista beats comprados \n"
            + "7 - Lista pedidos\n"
            + "8 - Añadir dinero\n"
            + "0 - Salir \n");

            Console.WriteLine("Escoja opción:");
            int opcionSel = VerifyNum();
            Console.Clear();

            // Variables para comprobar que se introduce un num valido
            switch (opcionSel)
            {
                case 1:  //Listado
                    ServiceBeat.ListaBeats();
                    MenuUser(nombreUsuario);
                    break;
                case 2:   //Comprar
                    string nombreRegalo = nombreUsuario;
                    ServiceBeat.CompraBeat(nombreUsuario, nombreRegalo);
                    break;
                case 3:   //Regalar
                    ServiceBeat.CompraBeat(nombreUsuario, nombreBeneficiado());
                    break;
                case 4:   //Subir
                    ServiceBeat.SubirBeat(nombreUsuario, ServiceAuth.listaBeats);
                    MenuUser(nombreUsuario);
                    break;
                case 5:   //Borrar
                    ServiceBeat.BorrarBeat(nombreUsuario);
                    break;
                case 6:   // Lista beats comprados
                    ServiceUsuario.ListaBeatsComprados(nombreUsuario);
                    break;
                case 7:   // Lista pedidos
                    ServiceCompra.ListaCompras(nombreUsuario);
                    break;
                case 8:   //Añadir dinero
                    ServiceUsuario.AñadirDinero(nombreUsuario);
                    break;
                case 0:   //Salir
                    TerminarPrograma();
                    break;
            }
            //No habrá tiempo de espera en éstas opciones
            if (opcionSel != 1 || opcionSel != 2)
            {
                System.Threading.Thread.Sleep(2000);
            }
            Console.Clear();
            return opcionSel;
        }

    }
}
