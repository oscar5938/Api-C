using Microsoft.EntityFrameworkCore;
namespace Api.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext>options)
        : base(options)
        {
        }
        DbSet<User> User {get; set;} = null!;
        DbSet<Compra> Compra {get; set;} = null!;
        DbSet<Beat> Beat {get; set;} = null!;
        
        // DbSet<UserBeat> UserBeat {get; set;} = null!;
        // DbSet<BeatCompra> BeatCompra {get; set;} = null!;
    }
}