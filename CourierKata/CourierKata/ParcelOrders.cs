using System;
using System.Collections.Generic;

namespace CourierKata
{
    public class ParcelOrders
    {
        private Dictionary<ParcelType, int> _parcelCosts;

        public ParcelOrders(Dictionary<ParcelType, int> parcelCosts)
        {
            _parcelCosts = parcelCosts;
        }

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
                        costs.Costs.Add(new Cost(_parcelCosts[ParcelType.Medium], ParcelType.Medium));
                        costs.Total += _parcelCosts[ParcelType.Medium];
                    }
                    else if (dims.IsInRange(50, 100))
                    {
                        costs.Costs.Add(new Cost(_parcelCosts[ParcelType.Large], ParcelType.Large));
                        costs.Total += _parcelCosts[ParcelType.Large];
                    }
                    else if (dims.IsMinimum(100))
                    {
                        costs.Costs.Add(new Cost(_parcelCosts[ParcelType.XL], ParcelType.XL));
                        costs.Total += _parcelCosts[ParcelType.XL];
                    }
                    else
                    {
                        costs.Costs.Add(new Cost(_parcelCosts[ParcelType.Small], ParcelType.Small));
                        costs.Total += _parcelCosts[ParcelType.Small];
                    }
                }
            }

            return costs;
        }
    }
}
