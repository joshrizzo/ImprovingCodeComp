using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass()]
    public class PolygonTests
    {
        [TestMethod()]
        public void PolygonAreaTest1()
        {
            var polygon = new Polygon()
            {
                Points = new List<Point>()
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 10, Y = 10 },
                    new Point() { X = 0, Y = 10 },
                    new Point() { X = 10, Y = 0 },
                    new Point() { X = 5, Y = 5 },
                }
            };

            Assert.AreEqual(100.0, polygon.Area);
        }

        [TestMethod()]
        public void PolygonAreaTest2()
        {
            var polygon = new Polygon()
            {
                Points = new List<Point>()
                {
                    new Point() { X = 6, Y = 39 },
                    new Point() { X = 28, Y = 25 },
                    new Point() { X = 28, Y = 13 },
                    new Point() { X = 31, Y = 3 },
                    new Point() { X = 11, Y = 19 },
                    new Point() { X = 31, Y = 17 },
                    new Point() { X = 26, Y = 19 },
                    new Point() { X = 18, Y = 13 },
                    new Point() { X = 30, Y = 11 },
                    new Point() { X = 25, Y = 20 },
                }
            };

            Assert.AreEqual(406.0, polygon.Area);
        }
    }
}