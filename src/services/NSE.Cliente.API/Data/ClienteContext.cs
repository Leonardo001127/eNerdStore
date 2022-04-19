using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Models;
using NSE.Core;
using NSE.Core.Data;
using NSE.Core.Mediator;
using NSE.Core.Message;
using System.Linq;
using System.Threading.Tasks;


namespace NSE.Clientes.API.Data
{
    public class ClienteContext: DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ClienteContext(DbContextOptions<ClienteContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<Event>();
            builder.Ignore<ValidationResult>();

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
            var result = await this.SaveChangesAsync() > 0;

            if(result)
                await _mediatorHandler.PublicarEventos(this);
            else
            {

            }
            return result;

        }
    }
    public static class MediatorExtensions
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediatorHandler, T ctx)
            where T : DbContext
        {
            
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            domainEntities.ToList().ForEach(x => x.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (@event) =>
                {
                    await mediatorHandler.PublicarEvento(@event);
                });

            await Task.WhenAll(tasks);
        }
    }
}
