﻿using NSE.Core.DomainObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Models
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void Adicionar(Cliente cliente);

        Task<Cliente> GetByCpf(string Cpf);

        Task<IEnumerable<Cliente>> GetAll();

        
    }
}