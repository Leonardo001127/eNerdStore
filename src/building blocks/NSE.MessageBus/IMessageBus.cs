using EasyNetQ;
using NSE.Core.Messages.Integration;
using System;
using System.Threading.Tasks;

namespace NSE.MessageBus
{
    public interface IMessageBus : IDisposable
    {

        IAdvancedBus advancedBus { get; }
        bool IsConnected { get; }
        void Publish<T>(T message) where T : IntegrationEvent;
        Task PublishAsync<T>(T message) where T : IntegrationEvent;
        void Subscribe<T>(string subscriptionId, Action<T> message) where T : class;

        Task SubscribeAsync<T>(string subscriptionId, Func<T, Task> message) where T : class;

        TResponse Request<TRequest, TResponse>(TRequest message)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest message)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;




    }
}