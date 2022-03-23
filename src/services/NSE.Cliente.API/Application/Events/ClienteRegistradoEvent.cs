using NSE.Core.Message;
using System;

namespace NSE.Clientes.API.Application.Events
{
    public class ClienteRegistradoEvent : Event
    {

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public ClienteRegistradoEvent(Guid _id, string _nome, string _email, string _cpf)
        {
            AggregateId = Id = _id;
            Nome = _nome;
            Email = _email;
            Cpf = _cpf;

        }
    }
}
