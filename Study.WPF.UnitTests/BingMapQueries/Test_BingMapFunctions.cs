using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Maps.MapControl.WPF;
using Study.BingMap.Query;
using System.Linq;

namespace Study.WPF.UnitTests.BingMapQueries
{
    [TestClass]
    public class Test_BingMapFunctions
    {

        string _Key = "Ai6zQ5AwxFAZKY3DtRmKAPJHZVlK4h_e01jNbblWGbagsXzwH0nf5vYrTEr13kBd";

        [TestMethod]
        public void TestMethod1()
        {

            // add locations into the polygon
            var locCollection = new LocationCollection();
            locCollection.Add(new Location(47.6424, -122.3219));
            locCollection.Add(new Location(47.8424, -122.1747));
            locCollection.Add(new Location(47.5814, -122.1747));

            // define a polycy
            var p = new MapPolygon() { 
                Locations = locCollection,
            };

            var filterString = Helper.GetSpatialFilterString(p);
            Assert.AreEqual("spatialFilter=intersects('POLYGON ((47.6424 -122.3219,47.8424 -122.1747,47.5814 -122.1747))')", filterString);
        }


        [TestMethod]
        public void Test_A_Sample()
        {

            var mapQuery = new BingMapQuery(_Key);
            var result = mapQuery.SampleQuery();

            Assert.IsTrue(result.Count() > 0);

        }

    }
}
