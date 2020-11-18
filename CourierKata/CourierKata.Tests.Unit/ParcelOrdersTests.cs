using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CourierKata.Tests.Unit
{
    [TestClass]
    public class ParcelOrdersTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCalculateCostsHasNullObjectThenThrowArgumentNullException()
        {
            try 
            {
                new ParcelOrders().CalculateCosts(null);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'parcels')", exception.Message, "Message");
                throw;
            }
        }
    }
}
