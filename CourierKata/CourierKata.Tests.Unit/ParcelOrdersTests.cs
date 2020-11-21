using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CourierKata.Tests.Unit
{
    [TestClass]
    [TestCategory("Unit")]
    public class ParcelOrdersTests
    {
        private ParcelOrders _orders;
        private List<ParcelConfig> _parcelsConfig;

        [TestInitialize]
        public void Initialise()
        {
            // The data source could be anything, ie. JSON, XML and then serialised into 
            // this list of config data
            _parcelsConfig = new List<ParcelConfig>();
            _parcelsConfig.Add(new ParcelConfig()
            {
                Type = ParcelType.Small,
                Price = 3,
                MinDim = 0
            });
            _parcelsConfig.Add(new ParcelConfig()
            {
                Type = ParcelType.Medium,
                Price = 8,
                MinDim = 10
            });
            _parcelsConfig.Add(new ParcelConfig()
            {
                Type = ParcelType.Large,
                Price = 15,
                MinDim = 50
            });
            _parcelsConfig.Add(new ParcelConfig()
            {
                Type = ParcelType.XL,
                Price = 25,
                MinDim = 100
            });

            _orders = new ParcelOrders(_parcelsConfig);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructorHasNullObjectThenThrowArgumentNullException()
        {
            try
            {
                new ParcelOrders(null);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'parcelsConfig')", exception.Message, "Message");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCalculateCostsHasNullObjectThenThrowArgumentNullException()
        {
            try 
            {
                _orders.CalculateCosts(null, false);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'parcelsDims')", exception.Message, "Message");
                throw;
            }
        }

        [TestMethod]
        public void WhenCalculateCostsHasSmallParcelDimsThenSmallParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { new Dimensions(9,9,9) };
            
            var summary = _orders.CalculateCosts(parcelDims, false);

            Assert.AreEqual(3, summary.Total, "Total");
            Assert.AreEqual(1, summary.Items.Count, "Costs count");
            AssertCost(summary.Items[0], _parcelsConfig[(int)ParcelType.Small]);
        }

        [TestMethod]
        public void WhenCalculateCostsHasNoDimsThenZeroCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>();
            
            var summary = _orders.CalculateCosts(parcelDims, false);

            Assert.AreEqual(0, summary.Total, "Total");
            Assert.AreEqual(0, summary.Items.Count, "Costs count");
        }

        [TestMethod]
        public void WhenCalculateCostsHasMediumParcelDimsThenMediumParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { new Dimensions(10,10,10) };

            var summary = _orders.CalculateCosts(parcelDims, false);

            Assert.AreEqual(8, summary.Total, "Total");
            Assert.AreEqual(1, summary.Items.Count, "Costs count");
            AssertCost(summary.Items[0], _parcelsConfig[(int)ParcelType.Medium]);
        }

        [TestMethod]
        public void WhenCalculateCostsHasLargeParcelDimsThenLargeParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { new Dimensions(99, 99, 99) };

            var summary = _orders.CalculateCosts(parcelDims, false);

            Assert.AreEqual(15, summary.Total, "Total");
            Assert.AreEqual(1, summary.Items.Count, "Costs count");
            AssertCost(summary.Items[0], _parcelsConfig[(int)ParcelType.Large]);
        }

        [TestMethod]
        public void WhenCalculateCostsHasXLParcelDimsThenXLParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { new Dimensions(100, 100, 100) };

            var summary = _orders.CalculateCosts(parcelDims, false);

            Assert.AreEqual(25, summary.Total, "Total");
            Assert.AreEqual(1, summary.Items.Count, "Costs count");
            AssertCost(summary.Items[0], _parcelsConfig[(int)ParcelType.XL]);
        }

        [TestMethod]
        public void WhenCalculateCostsHasAllParcelDimsThenAllParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { 
                new Dimensions(9, 9, 9),
                new Dimensions(10, 10, 10),
                new Dimensions(50, 50, 50),
                new Dimensions(100, 100, 100)
            };

            var summary = _orders.CalculateCosts(parcelDims, false);

            Assert.AreEqual(51, summary.Total, "Total");
            Assert.AreEqual(4, summary.Items.Count, "Costs count");

            for (int i=0; i<summary.Items.Count; i++)
            {
                AssertCost(summary.Items[i], _parcelsConfig[i]);
            }
        }

        [TestMethod]
        public void WhenCalculateCostsHasParcelDimsThenParcelCostsWithSpeedyShippingReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { new Dimensions(100, 100, 100) };

            var summary = _orders.CalculateCosts(parcelDims, true);

            Assert.AreEqual(50, summary.Total, "Total");
            Assert.AreEqual(1, summary.Items.Count, "Costs count");
            AssertCost(summary.Items[0], _parcelsConfig[(int)ParcelType.XL]);
            Assert.AreEqual(25, summary.SpeedyShipping, "SpeedyShipping");
        }

        private static void AssertCost(ParcelItem calculatedOrder, ParcelConfig parcelConfig)
        {
            Assert.AreEqual(parcelConfig.Price, calculatedOrder.Price, "Cost Price");
            Assert.AreEqual(parcelConfig.Type, calculatedOrder.TypeOfParcel, "TypeOfParcel");
        }
    }
}
