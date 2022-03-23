using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Models;
using NSE.Core.Data;
using NSE.Core.Message;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Data
{
    public class CatalogoContext: DbContext, IUnitOfWork
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<Event>();
            builder.Ignore<ValidationResult>();
            foreach (var property in builder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(x=> x.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }
            builder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
