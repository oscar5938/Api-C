namespace Api.Models
{
    public class Beat
    {
        public int ID { get; set; }
        public bool Premium { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public decimal Precio { get; set; }
        public static int NumeroPedidos { get; set; }
    }
}