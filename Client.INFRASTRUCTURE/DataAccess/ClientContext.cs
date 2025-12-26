
using ClientSi.Domain.Entities;
using ClientSi.INFRASTRUCTURE.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ClientSi.INFRASTRUCTURE.DataAccess
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options)
            : base(options)
        {
        }
        public DbSet<Client> Clients => Set<Client>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonneConfiguration());
        
        }
    }
}
