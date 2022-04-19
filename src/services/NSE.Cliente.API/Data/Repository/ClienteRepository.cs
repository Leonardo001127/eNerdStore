using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Models;
using NSE.Core.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        public ClienteContext context { get; set; }
       

        public ClienteRepository(ClienteContext context)
        {
            this.context = context;
             
        }
        public IUnitOfWork work => this.context;

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await context.Clientes.AsNoTracking().ToListAsync();
        }

        public Cliente GetByCpf(string cpf)
        {
            return context.Clientes.FirstOrDefault(x => x.Cpf.Numero == cpf);
        }

        public void Adicionar(Cliente cliente)
        {
            context.Clientes.Add(cliente);
        }
         
        public void Dispose()
        { 
            context.Dispose();
        }
    }
}
