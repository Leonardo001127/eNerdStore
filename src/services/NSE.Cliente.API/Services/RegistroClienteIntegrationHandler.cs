using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private IBus _bus;

        private IServiceProvider _services;

        public RegistroClienteIntegrationHandler(IServiceProvider services)
        { 
            _services = services;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:15672/");

            _bus.Rpc.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async proc =>
            new ResponseMessage(await Registrar(proc)));


            return Task.CompletedTask;
        }

        public async Task<ValidationResult> Registrar(UsuarioRegistradoIntegrationEvent usuario)
        {
            var result = new ValidationResult();
            using (var scope = _services.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                result = await mediator.EnviarCommando(new RegistrarClienteCommand(usuario.Nome, usuario.Email, usuario.Cpf, usuario.Id));
                
            }
            return result;
        }
    }
}
