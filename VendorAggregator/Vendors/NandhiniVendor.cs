using VendorAggregator.Vendors;
using System;

namespace VendorAggregator
{
    internal class NandhiniVendor : Vendor
    {
        public NandhiniVendor(MessageBroker messageBroker) : base(messageBroker)
        {
        }

        public override void OnCustomerRequestReceived(object sender, NotificationEvent customerRequestEvent)
        {
            var request = customerRequestEvent.DeserializeMessage<CustomerRequest>();

            var message = new Quotation
            {
                VendorAddress = "HSR Layout",
                VendorName = nameof(NandhiniVendor),
                Amount = request.MilkQuantity * 44
            };

            _messageBroker.EventBus<Quotation>().Publish(this, new NotificationEvent(new DateTime(), message));
        }
    }
}