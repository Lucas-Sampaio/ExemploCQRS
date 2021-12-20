using Core.Messages.Integration;
using EasyNetQ;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading.Tasks;

namespace MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;

        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;

        public IAdvancedBus AdvancedBus => _bus?.Advanced;

        private void TryConnect()
        {
            if (IsConnected) return;
            var policy = Policy.Handle<EasyNetQException>().Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_connectionString);
                AdvancedBus.Disconnected += OnDisconnect;
            });

        }
        private void OnDisconnect(object s, EventArgs e)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();
            policy.Execute(TryConnect);
        }
        public void Dispose()
        {
            _bus.Dispose();
        }
        /// <summary>
        /// Publica uma messagem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">Mensagem quem será publicada</param>
        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            _bus.PubSub.Publish(message);
        }
        /// <summary>
        /// Publica uma messagem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">Mensagem quem será publicada</param>
        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await _bus.PubSub.PublishAsync(message);
        }
        /// <summary>
        /// Envia uma mensagem e espera uma resposta
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse">o tipo de resposta de retorno</typeparam>
        /// <param name="request">A Mensagem que será publicada</param>
        /// <returns></returns>
        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Request<TRequest, TResponse>(request);
        }
        /// <summary>
        /// Envia uma mensagem e espera uma resposta
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse">o tipo de resposta de retorno</typeparam>
        /// <param name="request">A Mensagem que será publicada</param>
        /// <returns></returns>
        public Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }
        /// <summary>
        /// Retorna uma resposta de uma mensagem rpc
        /// </summary>
        /// <typeparam name="TRequest">a mensagem de request</typeparam>
        /// <typeparam name="TResponse">a resposta</typeparam>
        /// <param name="responder">configura a resposta</param>
        /// <returns></returns>
        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Respond(responder);

        }
        /// <summary>
        /// Retorna uma resposta de uma mensagem rpc
        /// </summary>
        /// <typeparam name="TRequest">a mensagem de request</typeparam>
        /// <typeparam name="TResponse">a resposta</typeparam>
        /// <param name="responder">configura a resposta</param>
        /// <returns></returns>
        public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.RespondAsync(responder).AsTask();
        }
        /// <summary>
        /// Recebe uma mensagem da fila
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subscriptionId">o identificador da mensagem</param>
        /// <param name="onMessage">executa alguma ação sobre a mensagem recebida</param>
        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.Subscribe(subscriptionId, onMessage);
        }
        /// <summary>
        /// Recebe uma mensagem da fila
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subscriptionId">o identificador da mensagem</param>
        /// <param name="onMessage">executa alguma ação sobre a mensagem recebida</param>
        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }
    }
}
