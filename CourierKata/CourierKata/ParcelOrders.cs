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
                foreach (Dimensions dim in parcels)
                {
                    if ((dim.Length >= 10 && dim.Width >= 10 && dim.Height >= 10) &&
                        (dim.Length < 50 && dim.Width < 50 && dim.Height < 50))
                    {
                        costs.Costs.Add(new Cost(8, ParcelType.Medium));
                        costs.Total += 8;
                    }
                    else if ((dim.Length >= 50 && dim.Width >= 50 && dim.Height >= 50) &&
                            (dim.Length < 100 && dim.Width < 100 && dim.Height < 100))
                    {
                        costs.Costs.Add(new Cost(15, ParcelType.Large));
                        costs.Total += 15;
                    }
                    else if (dim.Length >= 100 && dim.Width >= 100 && dim.Height >= 100)
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
