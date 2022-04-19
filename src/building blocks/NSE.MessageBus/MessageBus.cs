using EasyNetQ;
using NSE.Core.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading.Tasks;

namespace NSE.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus bus;
        private IAdvancedBus _advancedBus;
        public IAdvancedBus advancedBus => bus.Advanced;
        private readonly string _connectionString;

        public MessageBus(string conn)
        {
            _connectionString = conn;
            TryConnect();
            
        }

        public bool IsConnected => bus?.Advanced.IsConnected ?? false;



        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            bus.PubSub.Publish(message);
            
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await bus.PubSub.PublishAsync(message);

        }

        public TResponse Request<TRequest, TResponse>(TRequest message)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return bus.Rpc.Request<TRequest, TResponse>(message);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest message)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return await bus.Rpc.RequestAsync<TRequest, TResponse>(message);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return bus.Rpc.Respond(responder);
        }

        public async Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return await bus.Rpc.RespondAsync(responder);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> message) where T : class
        {

            TryConnect();
            bus.PubSub.Subscribe(subscriptionId,message);

        }

        public async Task SubscribeAsync<T>(string subscriptionId, Func<T, Task> message) where T : class
        {
            TryConnect();
            await bus.PubSub.SubscribeAsync(subscriptionId, message);

        }


        private void TryConnect()
        {
            if (IsConnected) return;

            var polly = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));

            polly.Execute(() =>
            {
                bus = RabbitHutch.CreateBus(_connectionString);
                _advancedBus = bus.Advanced;
                _advancedBus.Disconnected += OnDisconnect;
            });
            
        }
        private void OnDisconnect(object o, EventArgs e)
        {
            var polly = Policy.Handle<EasyNetQException>()
              .Or<BrokerUnreachableException>()
              .RetryForever();

            polly.Execute(TryConnect);  
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}