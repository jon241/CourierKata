using System;
using System.Collections.Generic;

namespace CourierKata
{
    public class ParcelOrders
    {
        private Dictionary<ParcelType, int> _parcelCosts;

        public ParcelOrders(Dictionary<ParcelType, int> parcelCosts)
        {
            if (parcelCosts == null)
                throw new ArgumentNullException(nameof(parcelCosts));

            _parcelCosts = parcelCosts;
        }

        public TotalCosts CalculateCosts(ICollection<Dimensions> parcels)
        {
            if (parcels == null)
                throw new ArgumentNullException(nameof(parcels));

            // The logic to how the parcel orders are calculated are
            // contained within this class only
            var costs = new TotalCosts();

            if (parcels.Count > 0)
            {
                foreach (Dimensions dims in parcels)
                {
                    if (dims.IsInRange(10, 50))
                    {
                        Cost cost = GetParcelCost(ParcelType.Medium);
                        costs.Costs.Add(cost);
                        costs.Total += cost.Price;
                    }
                    else if (dims.IsInRange(50, 100))
                    {
                        Cost cost = GetParcelCost(ParcelType.Large);
                        costs.Costs.Add(cost);
                        costs.Total += cost.Price;
                    }
                    else if (dims.IsMinimum(100))
                    {
                        Cost cost = GetParcelCost(ParcelType.XL);
                        costs.Costs.Add(cost);
                        costs.Total += cost.Price;
                    }
                    else
                    {
                        Cost cost = GetParcelCost(ParcelType.Small);
                        costs.Costs.Add(cost);
                        costs.Total += cost.Price;
                    }
                }

                costs.SpeedyShipping = costs.Total * 2;
            }

            return costs;
        }

        private Cost GetParcelCost(ParcelType parcelType)
        {
            return new Cost(_parcelCosts[parcelType], parcelType);
        }
    }
}
