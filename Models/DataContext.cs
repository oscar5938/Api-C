using Microsoft.EntityFrameworkCore;
namespace Api.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
       public DbSet<Users> Users {get; set;} //= null!;
        DbSet<Compra> Compras {get; set;} = null!;
        DbSet<Beat> Beats {get; set;} = null!;
        //DbSet<UserBeat> UserBeats {get; set;} = null!;
        //DbSet<BeatCompraUser> BeatCompraUsers {get; set;} = null!;
    }
}