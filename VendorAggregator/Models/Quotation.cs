namespace VendorAggregator
{
    internal class Quotation
    {
        public int Amount { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }

        public override string ToString()
        {
            return $"Name: {VendorName} -- Address: {VendorAddress} -- Amount: {Amount}";
        }
    }
}