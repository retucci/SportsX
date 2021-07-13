using Microsoft.EntityFrameworkCore;
using SportsXDomain;

namespace SportsXRepository
{
     public class SportsXContext : DbContext
    {
        public SportsXContext(DbContextOptions<SportsXContext> options) : base (options)
        {
            
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteTelefone> ClienteTelefones { get; set; }
    }
}