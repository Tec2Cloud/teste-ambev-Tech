namespace Business.Interfaces
{
    public interface IEventPublisher
    {
        void Publish(string eventName, object data);
    }
}
