using NSE.Core.Message;
using System;

namespace NSE.Clientes.API.Application.Commands
{
    public class RegistrarClienteCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegistrarClienteCommand(string _nome, string _email, string _cpf, Guid _id)
        {
            AggregateId = Id = _id;
            Nome = _nome;
            Email = _email;
            Cpf = _cpf;

        }
    }
}
