using System;
using System.Collections.Generic;
using System.Text;


namespace BeatStars.Clases
{
    public class Usuario
    {
        public int ID { get; set; }
        public string NombreCuenta { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal Riñonera { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set;}
        public string Rol { get; set; }
        public List<Beat> ListaBeatsPublicados { get; set; }
        public List<Beat> ListaBeatsComprados { get; set;}
        public List<Compra> ListaCompras { get; set;}

        public Usuario() {}
        public Usuario(string NombreCuenta, string Password, string Email, int id)
        {
            this.ID = id;
            this.Password = Password;
            this.NombreCuenta = NombreCuenta;
            this.Email = Email;
            this.Riñonera = 0;
            this.Estado = true;
            this.FechaCreacion = DateTime.Now;
            this.Rol = "Estandar";
            this.ListaBeatsPublicados = new List<Beat>();
            this.ListaBeatsComprados = new List<Beat>();
            this.ListaCompras = new List<Compra>();
        }
    }
}
