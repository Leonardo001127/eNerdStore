using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Models;
using NSE.Core.Data;
using System.Collections.Generic;
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
            return await context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetByCpf(string cpf)
        {
            return await context.Clientes.FirstOrDefaultAsync(x => x.Cpf.Numero == cpf);
        }

        public void Adicionar(Cliente cliente)
        {
            context.Clientes.AddAsync(cliente);
        }
         
        public void Dispose()
        {
            
            context.Dispose();
        }
    }
}
