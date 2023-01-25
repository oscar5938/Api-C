using BeatStars.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static BeatStars.Servicios.ServiceUsabilidad;

namespace BeatStars.Servicios
{
    public class ServiceAuth
    {

        private static List<Usuario> allUsers = new List<Usuario>();
        public static List<Usuario> listCuentas = new List<Usuario>();
        public static List<Beat> listaBeats = new List<Beat>();
        public static string lang = Environment.GetEnvironmentVariable("LANGUAGE"); //Recogemos la variable de entorno
        static DirectoryInfo d = new DirectoryInfo($@"jsons/");//Comprobar si existe carpeta de jsons

        public static List<Compra> listCompras = new List<Compra>();
        public static void DataCall()
        {


            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            FileInfo[] FileUsers = d.GetFiles("users.json"); //Getting Text files
            FileInfo[] FileBeats = d.GetFiles("beats.json"); //Getting Text files
            FileInfo[] FileCompra = d.GetFiles("compra.json"); //Getting Text files

            if (FileUsers.Length != 0) listCuentas = ReadUserJson("users.json", $@"jsons/");
            if (FileBeats.Length != 0) listaBeats = ReadBeatJson("beats.json", $@"jsons/");
            if (FileCompra.Length != 0) listCompras = ReadCompraJson("users.json", $@"jsons/");

            MenuAuth();
        }

        static void MenuAuth()
        {
            Console.Clear();
            Console.WriteLine("BeatStars | Lang: " + lang);
            Console.WriteLine($"\n1 - Login \n" +
                $"2 - Registrarse \n" +
                $"3 - Mostrar cuentas \n" +
                $"0 - Salir \n"
                );
            Console.WriteLine("Escoga opción:");

            int opcionSel = VerifyNum();

            Console.Clear();

            // Variables para comprobar que se introduce un num valido
            switch (opcionSel)
            {
                case 1:  //Login
                    Login(listCuentas);
                    break;
                case 2:   //Registrarse
                    Register(listCuentas);
                    break;
                case 3:   //Mostrar cuentas
                    for (int i = 0; i < listCuentas.Count; i++)
                    {
                        Console.WriteLine("ID:" + i + ", Email: " + listCuentas[i].Email);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("*****Pulse una tecla para continuar...*****");
                    Console.ReadKey();
                    break;
                case 0: //Terminar programa
                    TerminarPrograma();
                    break;
            }
            //No habrá tiempo de espera en éstas opciones
            /*if (opcionSel != 1 || opcionSel != 2 || opcionSel != 3)
            {
                System.Threading.Thread.Sleep(2000);
            }*/
            Console.Clear();
        }

        public static void Login(List<Usuario> Listacuentas)
        {
            Console.WriteLine("Escribe tu correo");
            string correo = Console.ReadLine();
            Console.WriteLine("Escribe tu contraseña");
            string pwd = Console.ReadLine();
            bool volver = false;
            foreach (var User in Listacuentas)
            {
                if (User.Password == pwd && User.Email == correo)
                {
                    MenuUser(User.NombreCuenta);
                    volver = true;
                }
            }
            if (volver == false)
            {
                Console.WriteLine("Usuario o contraseña incorrecto");
                MenuAuth();
            }
        }

        public static bool checkExistUsers()
        {
            bool exist = false;
            FileInfo[] FileUsers = d.GetFiles("users.json"); //Getting Text files
            exist = FileUsers.Length != 0 ? true : false;
            return exist;
        }

        public static void Register(List<Usuario> cuentas)
        {
            Cuadrado("Registro");
            string email;
            do
            {
                Console.WriteLine("Introduce el correo:");
                email = Console.ReadLine();
            } while (!CheckEmailPatern(email) || (!CheckEmailExistente(email)));

            string nombreCuenta;
            do
            {
                Console.WriteLine("Introduce nombre de cuenta:");
                nombreCuenta = Console.ReadLine();
            } while (!CheckNombreCuentaExistente(nombreCuenta));
            string pwd= CreatePassword();
            int id = 0;
            foreach (var User in cuentas)
            {
                id++;
            }
            cuentas.Add(new Usuario(nombreCuenta, pwd, email, id));

            //Se guardará automáticamente el usuario tras cada registro en la BBDD de jsons
            Cuadrado("Registro correcto!");
            //string accountEmail = $"{email}.json";
            string jsonString = JsonSerializer.Serialize(cuentas);

            Console.Clear();

            Console.WriteLine($"{email} registrado correctamente en la BBDD");
            Console.WriteLine($"Users.json actualizado.");
            File.WriteAllText($@"jsons/" + "users.json", jsonString); //Guardar en raiz en vez de en bin\Debug\netcoreapp3.1
            MenuUser(nombreCuenta);
        }

        public static List<Usuario> ReadUserJson(string fileName, string route)
        {
            Console.WriteLine("Seleccionado" + fileName);
            string jsonString = File.ReadAllText(route + fileName);

            List<Usuario> listaLeida = JsonSerializer.Deserialize<List<Usuario>>(jsonString);
            List<Usuario> listaCreada = new List<Usuario>();

            foreach (var user in listaLeida)
            {
                listaCreada.Add(user);
            }

            //Console.WriteLine("*****Pulse una tecla para continuar...*****");
            //Console.ReadKey();
            return listaCreada;
        }

        public static List<Beat> ReadBeatJson(string fileName, string route)
        {
            Console.WriteLine("Seleccionado" + fileName);
            string jsonString = File.ReadAllText(route + fileName);

            List<Beat> listaLeida = JsonSerializer.Deserialize<List<Beat>>(jsonString);
            List<Beat> listaBeats = new List<Beat>();

            foreach (var beat in listaLeida)
            {
                listaBeats.Add(beat);
            }

            //Console.WriteLine("*****Pulse una tecla para continuar...*****");
            //Console.ReadKey();
            return listaBeats;
        }

        public static List<Compra> ReadCompraJson(string fileName, string route)
        {
            Console.WriteLine("Seleccionado" + fileName);
            string jsonString = File.ReadAllText(route + fileName);

            List<Compra> listaLeida = JsonSerializer.Deserialize<List<Compra>>(jsonString);
            List<Compra> listaCompras = new List<Compra>();

            foreach (var compra in listaLeida)
            {
                listaCompras.Add(compra);
            }

            //Console.WriteLine("*****Pulse una tecla para continuar...*****");
            //Console.ReadKey();
            return listaCompras;
        }



    }
}
