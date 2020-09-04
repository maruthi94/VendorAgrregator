using System;

namespace VendorAggregator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var hub = new OrderHub();

            Console.WriteLine("Hey, please enter your Details");
            Console.WriteLine("Enter Name");
            var name = Console.ReadLine().Trim();
            Console.WriteLine("Enter Address");
            var address = Console.ReadLine().Trim();

            var cus = new Customer
            {
                Name = name,
                Address = address,
                Id = 123
            };

            Console.WriteLine("Please enter how much quantity of milk you want in liters");
            var milkQuantity = int.Parse(Console.ReadLine().Trim());
            var request = new CustomerRequest { Customer = cus, MilkQuantity = milkQuantity };
            Console.WriteLine("\nBelow are the quatations from the milk vendors:");

            static void eventHandler(object sender, NotificationEvent args)
            {
                Console.WriteLine(args.DeserializeMessage<Quotation>().ToString());
            }

            hub.Messenger.EventBus<Quotation>().Subscribe(eventHandler);
            hub.Messenger.EventBus<CustomerRequest>().Publish(null, new NotificationEvent(new DateTime(), request));
            hub.Messenger.EventBus<Quotation>().UnSubscribe(eventHandler);
        }
    }
}