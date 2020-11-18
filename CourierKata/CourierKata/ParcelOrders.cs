using System;

namespace CourierKata
{
    public class ParcelOrders
    {
        public void CalculateCosts(object parcels)
        {
            if (parcels == null)
                throw new ArgumentNullException(nameof(parcels));
        }
    }
}
