using System;
using System.Text.Json;

namespace VendorAggregator
{
    public class NotificationEvent
    {
        public string Message { get; private set; }

        public DateTime Date { get; private set; }

        public NotificationEvent(DateTime dateTime, object message)
        {
            Date = dateTime;
            Message = Serialize(message);
        }

        public T DeserializeMessage<T>()
        {
            var result = JsonSerializer.Deserialize<T>(Message);
            return result;
        }

        private string Serialize(object message)
        {
            return JsonSerializer.Serialize(message);
        }
    }
}