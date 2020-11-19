using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierKata.Tests.Unit
{
    [TestClass]
    public class DimensionsTests
    {
        [TestMethod]
        [DataRow(9, 9, 9, true)]
        [DataRow(10, 10, 10, false)]
        public void WhenDimsAreInRangeThenReturn(int length, int width, int height, bool expectedResult)
        {
            var dims = new Dimensions(length, width, height);

            Assert.AreEqual(dims.IsInRange(0, 10), expectedResult, "Is in range");
        }

        [TestMethod]
        [DataRow(9, 9, 9, false)]
        [DataRow(10, 10, 10, true)]
        public void WhenDimsAreMinimumThenReturn(int length, int width, int height, bool expectedResult)
        {
            var dims = new Dimensions(length, width, height);

            Assert.AreEqual(dims.IsMinimum(10), expectedResult, "Is in range");
        }
    }
}
