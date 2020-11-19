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

        [TestInitialize]
        public void Initialise()
        {
            // Small - price 3, minDim 0, maxDim 10
            // Medium - price 8, minDim 10, maxDim 50
            // Large - price 15, minDim 50, maxDim 100
            // XL - price 25, minDim 100
            Dictionary<ParcelType, int> parcelCosts = new Dictionary<ParcelType, int>();
            parcelCosts.Add(ParcelType.Small, 3);
            parcelCosts.Add(ParcelType.Medium, 8);
            parcelCosts.Add(ParcelType.Large, 15);
            parcelCosts.Add(ParcelType.XL, 25);

            _orders = new ParcelOrders(parcelCosts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCalculateCostsHasNullObjectThenThrowArgumentNullException()
        {
            try 
            {
                _orders.CalculateCosts(null);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'parcels')", exception.Message, "Message");
                throw;
            }
        }

        [TestMethod]
        public void WhenCalucateCostsHasSmallParcelDimsThenSmallParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { new Dimensions(9,9,9) };
            
            var calculatedOrder = _orders.CalculateCosts(parcelDims);

            Assert.AreEqual(3, calculatedOrder.Total, "Total");
            Assert.AreEqual(1, calculatedOrder.Costs.Count, "Costs count");
            Assert.AreEqual(3, calculatedOrder.Costs[0].Price, "Cost Price");
            Assert.AreEqual(ParcelType.Small, calculatedOrder.Costs[0].TypeOfParcel, "TypeOfParcel");
        }

        [TestMethod]
        public void WhenCalucateCostsHasNoDimsThenZeroCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>();
            
            var calculatedOrder = _orders.CalculateCosts(parcelDims);

            Assert.AreEqual(0, calculatedOrder.Total, "Total");
            Assert.AreEqual(0, calculatedOrder.Costs.Count, "Costs count");
        }

        [TestMethod]
        public void WhenCalucateCostsHasMediumParcelDimsThenMediumParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { new Dimensions(10,10,10) };

            var calculatedOrder = _orders.CalculateCosts(parcelDims);

            Assert.AreEqual(8, calculatedOrder.Total, "Total");
            Assert.AreEqual(1, calculatedOrder.Costs.Count, "Costs count");
            Assert.AreEqual(8, calculatedOrder.Costs[0].Price, "Cost Price");
            Assert.AreEqual(ParcelType.Medium, calculatedOrder.Costs[0].TypeOfParcel, "TypeOfParcel");
        }

        [TestMethod]
        public void WhenCalucateCostsHasLargeParcelDimsThenLargeParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { new Dimensions(99, 99, 99) };

            var calculatedOrder = _orders.CalculateCosts(parcelDims);

            Assert.AreEqual(15, calculatedOrder.Total, "Total");
            Assert.AreEqual(1, calculatedOrder.Costs.Count, "Costs count");
            Assert.AreEqual(15, calculatedOrder.Costs[0].Price, "Cost Price");
            Assert.AreEqual(ParcelType.Large, calculatedOrder.Costs[0].TypeOfParcel, "TypeOfParcel");
        }

        [TestMethod]
        public void WhenCalucateCostsHasXLParcelDimsThenXLParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { new Dimensions(100, 100, 100) };

            var calculatedOrder = _orders.CalculateCosts(parcelDims);

            Assert.AreEqual(25, calculatedOrder.Total, "Total");
            Assert.AreEqual(1, calculatedOrder.Costs.Count, "Costs count");
            Assert.AreEqual(25, calculatedOrder.Costs[0].Price, "Cost Price");
            Assert.AreEqual(ParcelType.XL, calculatedOrder.Costs[0].TypeOfParcel, "TypeOfParcel");
        }

        [TestMethod]
        public void WhenCalucateCostsHasAllParcelDimsThenAllParcelCostsReturned()
        {
            List<Dimensions> parcelDims = new List<Dimensions>() { 
                new Dimensions(9, 9, 9),
                new Dimensions(10, 10, 10),
                new Dimensions(50, 50, 50),
                new Dimensions(100, 100, 100)
            };

            var calculatedOrder = _orders.CalculateCosts(parcelDims);

            Assert.AreEqual(51, calculatedOrder.Total, "Total");
            Assert.AreEqual(4, calculatedOrder.Costs.Count, "Costs count");
            Assert.AreEqual(3, calculatedOrder.Costs[0].Price, "0Cost Price");
            Assert.AreEqual(ParcelType.Small, calculatedOrder.Costs[0].TypeOfParcel, "0TypeOfParcel");
            Assert.AreEqual(8, calculatedOrder.Costs[1].Price, "1Cost Price");
            Assert.AreEqual(ParcelType.Medium, calculatedOrder.Costs[1].TypeOfParcel, "1TypeOfParcel");
            Assert.AreEqual(15, calculatedOrder.Costs[2].Price, "2Cost Price");
            Assert.AreEqual(ParcelType.Large, calculatedOrder.Costs[2].TypeOfParcel, "2TypeOfParcel");
            Assert.AreEqual(25, calculatedOrder.Costs[3].Price, "3Cost Price");
            Assert.AreEqual(ParcelType.XL, calculatedOrder.Costs[3].TypeOfParcel, "3TypeOfParcel");
        }
    }
}
