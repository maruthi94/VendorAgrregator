namespace VendorAggregator
{
    internal interface IEventBus<T> : ISubscribe<T>, IPublisher<T>
    {
    }

    internal interface ISubscribe<out T>
    {
        public void Subscribe(Notify<T> handler);

        public void UnSubscribe(Notify<T> handler);
    }

    internal interface IPublisher<in T>
    {
        public void Publish(object publisher, T eventArgs);
    }
}