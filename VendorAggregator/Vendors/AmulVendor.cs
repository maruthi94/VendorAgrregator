using System;

namespace VendorAggregator.Vendors
{
    internal class AmulVendor : Vendor
    {
        public AmulVendor(MessageBroker messageBroker) : base(messageBroker)
        {
        }

        public override void OnCustomerRequestReceived(object sender, NotificationEvent customerRequestEvent)
        {
            var request = customerRequestEvent.DeserializeMessage<CustomerRequest>();

            var message = new Quotation
            {
                VendorAddress = "BTM Layout",
                VendorName = nameof(AmulVendor),
                Amount = request.MilkQuantity * 50
            };

            _messageBroker.EventBus<Quotation>().Publish(this, new NotificationEvent(new DateTime(), message));
        }
    }
}