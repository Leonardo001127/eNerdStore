using FluentValidation.Results;
using MediatR;
using NSE.Core.Message;
using System;
using System.Collections.Generic; 
using System.Text;
using System.Threading.Tasks;

namespace NSE.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }


        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await mediator.Publish(evento); 
        }

        public async Task<ValidationResult> EnviarCommando<T>(T comando) where T : Command
        {
            return await mediator.Send(comando);           
            
        }
    }
}
