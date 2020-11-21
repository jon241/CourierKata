using System;
using System.Collections.Generic;

namespace CourierKata
{
    /// <summary>
    /// Parcel orders
    /// </summary>
    public class ParcelOrders
    {
        private List<ParcelConfig> _parcelConfigs;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parcelsConfig">List of parcel configs</param>
        public ParcelOrders(IEnumerable<ParcelConfig> parcelsConfig)
        {
            if (parcelsConfig == null)
                throw new ArgumentNullException(nameof(parcelsConfig));

            _parcelConfigs = new List<ParcelConfig>(parcelsConfig);
        }

        /// <summary>
        /// Calculate the costs of a parcel order
        /// </summary>
        /// <param name="parcelsDims">Dimensions of the parcels to be delivered</param>
        /// <param name="isSpeedyShipping">Is speedy shipping required</param>
        /// <returns>Itemised summary of parcel delivery costs</returns>
        public ItemisedSummary CalculateCosts(ICollection<Dimensions> parcelsDims, bool isSpeedyShipping)
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

                // speedy shipping is double the order
                // so get the current total
                if (isSpeedyShipping)
                    summary.SpeedyShipping = summary.Total;

                // add the shipping cost to the total
                summary.Total += summary.SpeedyShipping;
            }

            return summary;
        }
    }
}
