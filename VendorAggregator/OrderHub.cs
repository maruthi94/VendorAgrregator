using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using VendorAggregator.Vendors;

namespace VendorAggregator
{
    internal class OrderHub
    {
        public MessageBroker Messenger { get; private set; }

        private readonly List<Vendor> vendors = new List<Vendor>();

        public OrderHub()
        {
            Messenger = new MessageBroker();
            RegisterMessageChannels();
            RegisterVendors();
        }

        private void RegisterMessageChannels()
        {
            Messenger.AddEventBus<CustomerRequest>();
            Messenger.AddEventBus<Quotation>();
        }

        private void RegisterVendors()
        {
            Type verdorType = typeof(Vendor);
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> impls = assembly.GetTypes().Where(t => t.IsSubclassOf(verdorType));
            foreach (Type impl in impls)
            {
                vendors.Add(Activator.CreateInstance(impl, Messenger) as Vendor);
            }
        }
    }

    internal delegate void Notify<in T>(object p, T notification);

    internal class MessageBroker
    {
        private readonly Dictionary<string, IEventBus<NotificationEvent>> _channels = new Dictionary<string, IEventBus<NotificationEvent>>();

        public IEventBus<NotificationEvent> EventBus<T>() => _channels.GetValueOrDefault(typeof(T).Name);

        public void AddEventBus<T>() => _channels.Add(typeof(T).Name, new EventBus<NotificationEvent>());
    }

    internal class EventBus<T> : IEventBus<T>
    {
        private event Notify<T> OnPublish;

        public void Subscribe(Notify<T> handler) => OnPublish += handler;

        public void UnSubscribe(Notify<T> handler) => OnPublish += handler;

        public void Publish(object publisher, T eventArgs) => OnPublish?.Invoke(publisher, eventArgs);
    }
}