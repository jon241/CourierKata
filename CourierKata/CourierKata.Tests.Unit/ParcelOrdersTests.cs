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
            _orders = new ParcelOrders();
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
    }
}
