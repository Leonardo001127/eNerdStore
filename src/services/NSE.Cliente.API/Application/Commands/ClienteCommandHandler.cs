using FluentValidation.Results;
using MediatR;
using NSE.Clientes.API.Application.Events;
using NSE.Clientes.API.Models;
using NSE.Core.Message;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarClienteCommand message,
            CancellationToken cancellation)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            var cliente = new Cliente(message.Id, message.Nome, message.Email, message.Cpf);

            var clienteExists = _clienteRepository.GetByCpf(message.Cpf);

            if (clienteExists == null)
            {
                AddError("Cliente já cadastrado no sistema");
                return message.ValidationResult;
            }


            _clienteRepository.Adicionar(cliente);


            cliente.AddEvent(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));


            return await Persist(_clienteRepository.work);
             
        }
    }
}
