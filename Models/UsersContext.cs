using Microsoft.EntityFrameworkCore;
namespace Api.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
        {
        }
        DbSet<User> Users {get; set;} = null!;
        DbSet<Compra> Compras {get; set;} = null!;
        DbSet<Beat> Beats {get; set;} = null!;
    }
}