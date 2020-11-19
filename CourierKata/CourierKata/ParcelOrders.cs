using System;
using System.Collections.Generic;

namespace CourierKata
{
    public class ParcelOrders
    {
        public TotalCosts CalculateCosts(ICollection<Dimensions> parcels)
        {
            if (parcels == null)
                throw new ArgumentNullException(nameof(parcels));

            // cheapest option for parcel should be selected
            var costs = new TotalCosts();

            if (parcels.Count > 0)
            {
                foreach (Dimensions dims in parcels)
                {
                    if (dims.IsInRange(10, 50))
                    {
                        costs.Costs.Add(new Cost(8, ParcelType.Medium));
                        costs.Total += 8;
                    }
                    else if (dims.IsInRange(50, 100))
                    {
                        costs.Costs.Add(new Cost(15, ParcelType.Large));
                        costs.Total += 15;
                    }
                    else if (dims.IsMinimum(100))
                    {
                        costs.Costs.Add(new Cost(25, ParcelType.XL));
                        costs.Total += 25;
                    }
                    else
                    {
                        costs.Costs.Add(new Cost(3, ParcelType.Small));
                        costs.Total += 3;
                    }
                }
            }

            return costs;
        }
    }
}
