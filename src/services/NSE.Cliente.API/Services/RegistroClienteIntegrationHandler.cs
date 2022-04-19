using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;

        private IServiceProvider _services;

        public RegistroClienteIntegrationHandler(IServiceProvider services, IMessageBus bus)
        {
            _services = services;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async proc =>
               await Registrar(proc));
        }
        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();

            _bus.advancedBus.Connected += OnConnect;

            return Task.CompletedTask;
        }
      
        public async Task<ResponseMessage> Registrar(UsuarioRegistradoIntegrationEvent usuario)
        {
            var result = new ValidationResult();
            using (var scope = _services.CreateAsyncScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                result = await mediator.EnviarCommando(new RegistrarClienteCommand(usuario.Nome, usuario.Email, usuario.Cpf, usuario.Id));
                
            }
            return new ResponseMessage(result);
        }
    }
}
