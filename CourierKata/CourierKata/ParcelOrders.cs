using System;
using System.Collections.Generic;

namespace CourierKata
{
    public class ParcelOrders
    {
        private List<ParcelConfig> _parcelConfigs;

        public ParcelOrders(IEnumerable<ParcelConfig> parcelsConfig)
        {
            if (parcelsConfig == null)
                throw new ArgumentNullException(nameof(parcelsConfig));

            _parcelConfigs = new List<ParcelConfig>(parcelsConfig);
        }

        public ItemisedSummary CalculateCosts(ICollection<Dimensions> parcelsDims)
        {
            if (parcelsDims == null)
                throw new ArgumentNullException(nameof(parcelsDims));

            // The logic to how the parcel orders are calculated are
            // contained within this class only
            var summary = new ItemisedSummary();

            if (parcelsDims.Count > 0)
            {
                foreach (Dimensions parcelDims in parcelsDims)
                {
                    // find the last parcel config where the parcel dims
                    // are more than or equal to the configured minimum
                    ParcelConfig parcelConfig = _parcelConfigs.FindLast(pc =>
                        parcelDims.Height >= pc.MinDim &&
                        parcelDims.Length >= pc.MinDim &&
                        parcelDims.Width >= pc.MinDim
                    );

                    summary.Items.Add(new ParcelItem(parcelConfig.Price, parcelConfig.Type));
                    summary.Total += parcelConfig.Price;
                }

                summary.SpeedyShipping = summary.Total * 2;
            }

            return summary;
        }
    }
}
