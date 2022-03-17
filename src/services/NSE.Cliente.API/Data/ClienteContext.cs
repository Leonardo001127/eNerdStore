using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Models;
using NSE.Core.Data;
using System.Linq;
using System.Threading.Tasks;


namespace NSE.Clientes.API.Data
{
    public class ClienteContext: DbContext, IUnitOfWork
    {
        public ClienteContext(DbContextOptions<ClienteContext> options)
            : base(options) {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(x=> x.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }
            foreach (var relation in builder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
                relation.DeleteBehavior = DeleteBehavior.ClientSetNull;


            builder.ApplyConfigurationsFromAssembly(typeof(ClienteContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
