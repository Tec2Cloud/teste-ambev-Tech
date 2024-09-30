using Business.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ILogger<EventPublisher> _logger;

        public EventPublisher(ILogger<EventPublisher> logger)
        {
            _logger = logger;
        }

        public void Publish(string eventName, object data)
        {
            _logger.LogInformation("Evento publicado: {EventName} com dados: {Data}", eventName, data);
        }
    }
}
