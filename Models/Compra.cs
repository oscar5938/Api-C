  namespace Api.Models{
  public class Compra
    {
        public int ID { get; set;}
        public bool MetodoCompra { get; set;}
        public decimal Total { get;set; }
        public string CorreoUsuario { get; set;}
        public DateTime FechaCompra { get;set; }
        List<Beat> Carrito { get; set; }
    }
    }