using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Study.BingMap.Query;

namespace Study.WPF.UnitTests.BingMapQueries
{
    [TestClass]
    public class Test_SpatialQuery
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ddd = new SpatialService("Ai6zQ5AwxFAZKY3DtRmKAPJHZVlK4h_e01jNbblWGbagsXzwH0nf5vYrTEr13kBd");

            //var result1 = ddd.ExampleFindByAreaRadius();
            //var result2 = ddd.ExampleFindByProperty();
            //var result = ddd.ExampleFindByBoundingBox();
            //var result4 = ddd.ExampleQueryByIdAtom();

            var result = ddd.ReverseGeoCode(33.75550, -84.3900);
            Assert.IsTrue(result.Count > 0);
            //Assert.IsTrue(result2.Count > 0);
            //Assert.IsTrue(result3.Count > 0);
            //Assert.IsTrue(result4 != null);
            
        }
    }
}
