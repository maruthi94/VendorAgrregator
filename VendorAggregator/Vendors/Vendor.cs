namespace VendorAggregator.Vendors
{
    internal abstract class Vendor
    {
        protected readonly MessageBroker _messageBroker;

        protected Vendor(MessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
            _messageBroker.EventBus<CustomerRequest>().Subscribe(OnCustomerRequestReceived);
        }

        public abstract void OnCustomerRequestReceived(object sender, NotificationEvent customerRequestEvent);

        ~Vendor()
        {
            _messageBroker.EventBus<CustomerRequest>().UnSubscribe(OnCustomerRequestReceived);
        }
    }
}