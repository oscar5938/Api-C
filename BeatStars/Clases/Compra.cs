using System;
using System.Collections.Generic;
using System.Text;

namespace BeatStars.Clases
{
    public class Compra
    {
        public int ID { get; set;}
        public bool MetodoCompra { get; set;}
        public decimal Total { get;set; }
        public string CorreoUsuario { get; set;}
        public DateTime FechaCompra { get;set; }
        List<Beat> Carrito { get; set; }


        public Compra(){}
        public Compra(bool metodoCompra, decimal total, string correoUsuario, DateTime fechaCompra, List<Beat> carrito, int id){
            this.MetodoCompra=metodoCompra;
            this.Total=total;
            this.CorreoUsuario=correoUsuario;
            this.FechaCompra=fechaCompra;
            this.ID=id;
        }
    }
}
