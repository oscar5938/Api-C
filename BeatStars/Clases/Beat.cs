using System;
using System.Collections.Generic;
using System.Text;

namespace BeatStars.Clases
{
    public class Beat
    {
        public int ID { get; set;}
        public bool Premium { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public decimal Precio { get; set; }
        public static int NumeroPedidos { get; set; }

        public Beat(){}
        public Beat(bool premium, string nombre, string tipo, decimal precio, int id){
            this.Premium=premium;
            this.Nombre=nombre;
            this.Tipo=tipo;
            this.Precio=precio;
            this.ID=id;
        }

    }
}
