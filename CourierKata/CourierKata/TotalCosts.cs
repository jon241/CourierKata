using System.Collections.Generic;

namespace CourierKata
{
    public class TotalCosts
    {
        public decimal Total { get; internal set; }
        public List<Cost> Costs { get; }
        public decimal SpeedyShipping { get; set; }

        public TotalCosts()
        {
            Costs = new List<Cost>();
        }
    }
}