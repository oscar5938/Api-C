namespace Api.Models{
    public class Users:Model{
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
    }
}