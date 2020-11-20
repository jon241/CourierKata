using System.Collections.Generic;

namespace CourierKata
{
    public class ItemisedSummary
    {
        public decimal Total { get; internal set; }
        public List<ParcelItem> Items { get; }
        public decimal SpeedyShipping { get; set; }

        public ItemisedSummary()
        {
            Items = new List<ParcelItem>();
        }
    }
}